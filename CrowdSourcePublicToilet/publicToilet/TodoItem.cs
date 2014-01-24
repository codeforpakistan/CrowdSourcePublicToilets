using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace publicToilet
{
    public class TodoItem
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }


        [JsonProperty(PropertyName = "lat")]
        public string Latitude { get; set; }


        [JsonProperty(PropertyName = "lon")]
        public string Longitude { get; set; }


        [JsonProperty(PropertyName = "town")]
        public string Town { get; set; }


        [JsonProperty(PropertyName = "fam")]
        public string Near_Famous { get; set; }



        [JsonProperty(PropertyName = "complete")]
        public bool Complete { get; set; }

        [JsonProperty(PropertyName = "unisex")]
        public bool Unisex { get; set; }

        [JsonProperty(PropertyName = "wheelchair")]
        public bool WheelChair { get; set; }

        [JsonProperty(PropertyName = "payment")]
        public bool Payment { get; set; }

        [JsonProperty(PropertyName = "babychange")]
        public bool BabyChange { get; set; }

        [JsonProperty(PropertyName = "disables")]
        public bool Disables { get; set; }

        [JsonProperty(PropertyName = "male")]
        public bool Male { get; set; }

        [JsonProperty(PropertyName = "female")]
        public bool Female { get; set; }
    }
}
