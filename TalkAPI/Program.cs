using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TalkAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var mes = Console.ReadLine();
                if (mes != "")
                {
                    talk(mes);
                }
            }
        }
        static async void talk (string mes)
        {
            var APIkey = "APIKeyの入力";
            var client = new HttpClient();
            var values = new Dictionary<string, string>
            {
                { "apikey", APIkey },
                { "query", mes}
            };
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(@"https://api.a3rt.recruit-tech.co.jp/talk/v1/smalltalk", content);
            var responseString = await response.Content.ReadAsStringAsync();
         
            Welcome str = JsonConvert.DeserializeObject<Welcome>(responseString);

            Console.WriteLine("AI:"+str.Results[0].Reply);
        }
        public class Welcome
        {
            public long Status { get; set; }

            public string Message { get; set; }

            public List<Result> Results { get; set; }
            public class Result
            {
                public double Perplexity { get; set; }

                public string Reply { get; set; }
            }
        }

    }
}
