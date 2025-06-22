using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace MauiApp1.Services
{
    public class LlamaService
    {
        private readonly HttpClient _httpClient;

        public LlamaService()
        {
#if ANDROID
            _httpClient = new HttpClient { BaseAddress = new Uri("http://10.0.2.2:11434") };
#else
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:11434") };
#endif
        }
        public string FormatPrompt(List<EventModel> tasks)
        {
            string output = "";
            int count = 1;
            foreach (EventModel task in tasks)
            {
                output += $"Task {count}. Name: {task.Name}, Description: {task.Description}, Date: {task.DateTime}. ";
                count++;
            }
            return output;
        }

        public async Task<List<EventModel>> GenerateFollowUpsAsync(List<EventModel> tasks)
        {
            var prompt = $"Suggest a follow-up task each for: {FormatPrompt(tasks)}. Be as brief as possible. " +
                $"Your response should be with a list of numbered suggestions with descriptions and due date. Nothing else in the response. " +
                $"The response should be in the format Task name: Task description: Task suggested date (DD/MM/YYYY)." +
                $"The date right now is {DateTime.Now}. Suggest appopriate due dates from present onwards. Ensure name, description and date separated by :";

            var requestBody = new
            {
                model = "llama3",
                prompt = prompt,
                stream = false
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/generate", content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var llamaResponse = JsonSerializer.Deserialize<LlamaResponse>(result);

            return ParseTasks(llamaResponse?.Response);
        }

        private List<EventModel> ParseTasks(string rawOutput)
        {
            List<string> lines = SplitLines(rawOutput);

            List<EventModel> output = new List<EventModel>();
            foreach (string line in lines)
            {
                var parts = line.Split(':', 3);
                if (parts.Length == 3)
                {
                    string name = parts[0].Trim();
                    string description = parts[1].Trim();

                    //bool v = DateTime.TryParseExact(parts[2].Trim(, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, 
                      //  System.Globalization.DateTimeStyles.None, out DateTime date);
                    //output.Add(new EventModel(name, description, date));
                }
            }

            return output;
        }

        private List<String> SplitLines(string rawOutput)
        {
            var step1 = rawOutput.Substring(rawOutput.IndexOf('1'));
            // Assumes output is a list like: "1. Review the draft\n2. Get feedback\n..."
            var lines = step1.Split('\n')
                                 .Select(l => l.TrimStart('-', '*', ' ', '1', '2', '3', '4', '5', '.', ')'))
                                 .Where(l => !string.IsNullOrWhiteSpace(l))
                                 .ToList();
            return lines;
        }

        private class LlamaResponse
        {
            [JsonPropertyName("response")]
            public string Response { get; set; }
        }
    }
}
