using MauiApp1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Scripts
{
    public class TaskSuggester
    {
        public Dictionary<DateTime, List<EventModel>> existingTasks => EventService.Instance.EventsDict;

        public List<EventModel> SuggestTasks(int numberOfSuggestions = 5)
        {
            var suggestions = new List<EventModel>();
            if (existingTasks == null || existingTasks.Count == 0)
            {
                // no existing tasks, no suggestions
                return suggestions;
            }


            // calculate average of all tasks' frequency
            List<DateTime> taskDateTimes = existingTasks.Keys.ToList();
            TimeSpan averageTaskFrequency = CalculateAverageTaskFrequency(taskDateTimes);
            // can generate suggestions' next due date based on patterns and frequency
            DateTime lastTaskDate = taskDateTimes.Max();


            Random random = new Random();
            List<EventModel> existingTaskslist = new List<EventModel>();
            // get all tasks in one big list
            foreach (KeyValuePair<DateTime, List<EventModel>> items in existingTasks)
            {
                existingTaskslist.AddRange(items.Value);
            }


            for (int i = 0; i < numberOfSuggestions; i++)
            {
                // suggest a random task from the list of tasks
                EventModel randomTask = existingTaskslist[random.Next(existingTaskslist.Count)];

                string suggestedName = randomTask.Name;
                string suggestedDescription = randomTask.Description;

                DateTime suggestedDate = WordsTimeSpan(suggestedName + suggestedDescription, randomTask.DateTime);
                // check to see if the task contained any key words, changing its suggested date 
                // if not, assign a date based on the average frequency of tasks
                if (suggestedDate == randomTask.DateTime)
                    suggestedDate = lastTaskDate.Add(averageTaskFrequency.Multiply(i+1));

                EventModel suggestedTask = new EventModel(suggestedName, suggestedDescription, suggestedDate);
                if (!suggestions.Contains(suggestedTask))
                    suggestions.Add(suggestedTask);
            }

            return suggestions;
        }

        private TimeSpan CalculateAverageTaskFrequency(List<DateTime> taskDateTimes)
        {
            if (taskDateTimes == null || taskDateTimes.Count < 2)
            {
                return TimeSpan.FromDays(7); // default to weekly frequency if not enough data
            }

            List<TimeSpan> timeSpans = new List<TimeSpan>();
            for (int i = 1; i < taskDateTimes.Count; i++)
            {
                // avoid extreme date differences e.g. birthdays years from now
                if (taskDateTimes[i] - taskDateTimes[i-1] < TimeSpan.FromDays(367))
                    timeSpans.Add(taskDateTimes[i] - taskDateTimes[i - 1]);
            }

            return TimeSpan.FromTicks((long)timeSpans.Average(ts => ts.Ticks));
        }

        private DateTime WordsTimeSpan(string text, DateTime dateTime)
        {
            Dictionary<string, TimeSpan> timeSpans = new Dictionary<string, TimeSpan>
            {
                { "bins", TimeSpan.FromDays(7) },
                { "daily", TimeSpan.FromDays(1) },
                { "weekly", TimeSpan.FromDays(7) },
                { "groceries", TimeSpan.FromDays(7) },
                { "walk", TimeSpan.FromDays(2) },
                { "bi-weekly", TimeSpan.FromDays(14) },
                { "biweekly", TimeSpan.FromDays(14) },
                { "call", TimeSpan.FromDays(28) },
                { "assignment", TimeSpan.FromDays(7) },
            };
            foreach (KeyValuePair<string, TimeSpan> word in timeSpans)
            {
                if (text.ToLower().Contains(word.Key))
                    return dateTime.Add(word.Value);
            }

            List<string> strings = new List<string>() { "birthday", "christmas", "easter", "halloween", "hannukah", "independence"};
            foreach (string word in strings)
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
    }
}
