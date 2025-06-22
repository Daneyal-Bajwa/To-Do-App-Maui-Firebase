using MauiApp1.Services;
using Plugin.Maui.Calendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Scripts
{
    public class TaskSuggester
    {
        public static EventCollection Events => EventService.Instance.Events;
        private readonly LlamaService _llamaService = new LlamaService();

        public static List<EventModel> preloadedSuggestions = new List<EventModel>();
        private async void SendPrompt(List<EventModel> tasks)
        {
            List<EventModel> suggestions = await _llamaService.GenerateFollowUpsAsync(tasks);

            foreach (EventModel s in suggestions)
            {
                Console.WriteLine(s);
            }
            preloadedSuggestions.AddRange(suggestions);
        }

        private static readonly Dictionary<string, TimeSpan> timeSpans = new Dictionary<string, TimeSpan>
        {
            { "daily", TimeSpan.FromDays(1) },
            { "weekly", TimeSpan.FromDays(7) },
            { "bi-weekly", TimeSpan.FromDays(14) },
            { "biweekly", TimeSpan.FromDays(14) },

            { "bins", TimeSpan.FromDays(7) },
            { "groceries", TimeSpan.FromDays(7) },
            { "walk", TimeSpan.FromDays(1) },
            { "call", TimeSpan.FromDays(28) },
            { "assignment", TimeSpan.FromDays(7) },
            { "chore", TimeSpan.FromDays(7) },
            { "work", TimeSpan.FromDays(1) },
            { "visit", TimeSpan.FromDays(112) }, // 16 weeks/4 months
        };
        private static readonly List<string> annualHolidays = new List<string>()
                { "birthday", "christmas", "easter", "halloween", "independence" };

        public List<EventModel> SuggestTasks(int numberOfSuggestions = 5)
        {
            // essential that the events are sorted by their date-time before we start
            EventService.Instance.SortEvents();
            var existingTasks = Events;

            List<EventModel> suggestions = new List<EventModel>();
            if (existingTasks == null || existingTasks.Count == 0)
            {
                // no existing tasks, no suggestions
                suggestions.Add(new EventModel("Add some tasks!", "Get adding :)", DateTime.Now));
                return suggestions;
            }


            // calculate average of all tasks' frequency
            List<DateTime> taskDateTimes = existingTasks.Keys.ToList();
            TimeSpan averageTaskFrequency = CalculateAverageTaskFrequency(taskDateTimes);


            // get all tasks in one big list
            Random random = new Random();
            List<EventModel> existingTaskslist = new List<EventModel>();
            foreach (var items in existingTasks)
            {
                existingTaskslist.AddRange((IEnumerable<EventModel>)items.Value);
            }

            // gets tasks which happen together often in a tuple as the key, with their average timespan as the value
            Dictionary<string, (string, TimeSpan)> taskPatterns = AnalyseTaskPatterns(existingTaskslist);

            // tasks for AI
            List<EventModel> promptList = new List<EventModel>();

            // get specified number of tasks
            for (int i = 0; i < numberOfSuggestions; i++)
            {
                // suggest a random task from the list of tasks, the new task's inspiration
                EventModel randomTask = existingTaskslist[random.Next(existingTaskslist.Count)];

                string suggestedName = randomTask.Name;
                string suggestedDescription = randomTask.Description;
                DateTime suggestedDate = TimeKeywords(suggestedName + suggestedDescription, randomTask.DateTime);

                // if the task occurs with another task often, suggest the other task instead with their average time difference
                if (taskPatterns.TryGetValue(randomTask.Name, out (string, TimeSpan) value))
                {
                    suggestedName = value.Item1;
                    suggestedDate = randomTask.DateTime.Add(value.Item2);
                }


                // check to see if the task contained any key words, changing its suggested date 
                // if not, use LLM to create a follow-up task
                // else add the task to the list of suggestive tasks
                if (suggestedDate == randomTask.DateTime)
                {
                    promptList.Add(randomTask);
                }
                else {

                    EventModel suggestedTask = new EventModel(suggestedName, suggestedDescription, suggestedDate);
                    bool found = false;
                    foreach (var item in suggestions)
                        if (item.Name.Equals(suggestedTask.Name) && item.DateTime.Equals(suggestedTask.DateTime))
                        {
                            found = true;
                            break;
                        }
                    // only suggest tasks set in the future and if they haven't already been suggested this time
                    if (!found && suggestedDate >= DateTime.Today)
                        suggestions.Add(suggestedTask);
                }
            }

            SendPrompt(promptList);

            suggestions.AddRange(preloadedSuggestions);
            return suggestions;
        }

        private static TimeSpan CalculateAverageTaskFrequency(List<DateTime> taskDateTimes)
        {
            if (taskDateTimes == null || taskDateTimes.Count < 2)
            {
                return TimeSpan.FromDays(7); // default to weekly frequency if not enough data
            }

            List<TimeSpan> timeSpans = new List<TimeSpan>();
            for (int i = 1; i < taskDateTimes.Count; i++)
            {
                // avoid extreme date differences e.g. birthdays years from now
                if (taskDateTimes[i] - taskDateTimes[i - 1] < TimeSpan.FromDays(365))
                    timeSpans.Add(taskDateTimes[i] - taskDateTimes[i - 1]);
            }

            return TimeSpan.FromTicks((long)timeSpans.Average(ts => ts.Ticks));
        }

        private static DateTime TimeKeywords(string text, DateTime dateTime)
        {
            foreach (KeyValuePair<string, TimeSpan> word in timeSpans)
            {
                if (text.ToLower().Contains(word.Key))
                    return dateTime.Add(word.Value);
            }

            foreach (string word in annualHolidays)
            {
                if (text.ToLower().Contains(word))
                    return dateTime.AddYears(1);
            }

            List<string> strings2 = new List<string>() { "monthly" };
            foreach (string word in strings2)
            {
                if (text.ToLower().Contains(word))
                    return dateTime.AddMonths(1);
            }
            return dateTime;
        }

        private static Dictionary<string, (string, TimeSpan)> AnalyseTaskPatterns(List<EventModel> tasks)
        {
            var taskPatterns = new Dictionary<string, (string, TimeSpan)>();
            var taskPairs = new Dictionary<(string, string), List<TimeSpan>>();

            // analyse task pairs
            for (int i = 1; i < tasks.Count; i++)
            {
                EventModel currentTask = tasks[i];
                EventModel previousTask = tasks[i - 1];

                (string, string) taskPair = (previousTask.Name.ToLower(), currentTask.Name.ToLower());
                TimeSpan timeDifference = currentTask.DateTime - previousTask.DateTime;

                if (!taskPairs.ContainsKey(taskPair))
                {
                    taskPairs[taskPair] = new List<TimeSpan>();
                }

                taskPairs[taskPair].Add(timeDifference);
            }

            // identify frequent task pairs
            foreach (KeyValuePair<(string, string), List<TimeSpan>> pair in taskPairs)
            {
                TimeSpan averageTimeDifference = TimeSpan.FromTicks((long)pair.Value.Average(ts => ts.Ticks));
                if (pair.Value.Count >= 2) // adjust the threshold as needed
                {
                    taskPatterns[pair.Key.Item1] = (pair.Key.Item2, averageTimeDifference);
                }
            }

            return taskPatterns;
        }
    }
}
