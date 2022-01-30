using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace dialog_ai
{
    class ai_deserl
    {
        public class Root
        {
            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("responses")]
            public List<string> Responses { get; set; }

            [JsonProperty("sessionid")]
            public int Sessionid { get; set; }

            [JsonProperty("channel")]
            public int Channel { get; set; }

            [JsonProperty("ids")]
            public List<int> Ids { get; set; }
        }
    }
}
