using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Trivia
{
    class TriviaApi
    {
        private static string TOKEN;

        public static void SetToken()
        {
            using (var client = new HttpClient())
            {
                var method = new HttpMethod("GET");
                var request = new HttpRequestMessage(method, "https://opentdb.com/api_token.php?command=request");
                var response = client.SendAsync(request).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var obj = JObject.Parse(responseContent);
                TOKEN = (string)obj.SelectToken("token");
            }
        }

        public static List<TriviaObj> GetTriviaQuestions()
        {
            if (TOKEN == null || TOKEN == "") SetToken();
            List<TriviaObj> questions;
            using (var client = new HttpClient())
            {
                var method = new HttpMethod("GET");
                var request = new HttpRequestMessage(method, "https://opentdb.com/api.php?amount=10");
                var response = client.SendAsync(request).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var temp = JObject.Parse(responseContent);
                var temp2 = temp.SelectToken("results");
                questions = JsonConvert.DeserializeObject<List<TriviaObj>>(temp2.ToString());
            }
            return questions;
        }

        public static List<TriviaObj> GetTriviaQuestionsNoAnime()
        {
            if (TOKEN == null || TOKEN == "") SetToken();
            List<TriviaObj> questions;
            using (var client = new HttpClient())
            {
                var method = new HttpMethod("GET");
                var request = new HttpRequestMessage(method, "https://opentdb.com/api.php?amount=10");
                var response = client.SendAsync(request).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var temp = JObject.Parse(responseContent);
                var temp2 = temp.SelectToken("results");
                questions = JsonConvert.DeserializeObject<List<TriviaObj>>(temp2.ToString());
            }
            questions = questions.Where(x => !x.Category.ToLower().Contains("anime")).ToList();
            //this will prevent an error when 10 anime questions come up
            while (questions.Count < 1) questions = GetTriviaQuestionsNoAnime();
            return questions;
        }
    }
}
