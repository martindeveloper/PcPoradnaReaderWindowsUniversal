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

        public async Task<IReadOnlyList<Question>> FetchLatestQuestionsAsync ()
        {
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(Endpoint))
            using (HttpContent content = response.Content)
            {
                List<Question> questions = new List<Question>();
                string result = await content.ReadAsStringAsync();

                if (result?.Length >= 50)
                {
                    // Fill collection
                    JObject jsonObject = JObject.Parse(result);
                    
                    foreach(JToken item in jsonObject["items"])
                    {
                        questions.Add(QuestionFactory.FromJsonToken(item));
                    }
                }

                return new ReadOnlyCollection<Question>(questions);
            }
        }
    }
}
