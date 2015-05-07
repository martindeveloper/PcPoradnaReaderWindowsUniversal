using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PcPoradnaReaderWindowsUniversal.Model
{
    class JsonDataProvider : IDataProvider
    {
        private string Endpoint { get; }

        public JsonDataProvider (Uri endpoint)
        {
            Endpoint = endpoint.ToString();
        }

        private async Task<string> FetchUrl (string url)
        {
            string result = "";

            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(url))
            using (HttpContent content = response.Content)
            {
                string json = await content.ReadAsStringAsync();

                if (json?.Length >= 50)
                {
                    result = json;
                }
            }

            return result;
        }

        public async Task<IReadOnlyList<Reply>> FetchRepliesAsync(Question question)
        {
            List<Reply> replies = new List<Reply>();

            string result = await FetchUrl(question.RepliesApiUrl);

            JObject jsonObject = JObject.Parse(result);

            //System.Diagnostics.Debugger.Break();

            foreach (JToken item in jsonObject["replies"])
            {
                replies.Add(ReplyFactory.FromJsonToken(item));
            }

            IReadOnlyList<Reply> repliesReadOnlyList = new ReadOnlyCollection<Reply>(replies);
            question.Replies = repliesReadOnlyList;

            return repliesReadOnlyList;
        }

        public async Task<IReadOnlyList<Question>> FetchLatestQuestionsAsync ()
        {
            List<Question> questions = new List<Question>();

            string result = await FetchUrl(Endpoint);

            // Fill collection
            JObject jsonObject = JObject.Parse(result);

            foreach (JToken item in jsonObject["items"])
            {
                questions.Add(QuestionFactory.FromJsonToken(item));
            }

            return new ReadOnlyCollection<Question>(questions);
        }
    }
}
