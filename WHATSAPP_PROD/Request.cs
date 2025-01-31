using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WHATSAPP_PROD
{
    public class Request
    {
        public string apiKey { get; set; }
        public string x_api_key { get; set; }
        public string TYPE { get; set; }
        public string SENDER { get; set; }
        public string NAMESPACE { get; set; }
        public string BOT_ID { get; set; }
        public string whatsAppNumber { get; set; }
        public string templateName { get; set; }
        public Parameter templateParams { get; set; }

        public string exceptionmsg { get; set; }

    }
    public class Parameter
    {
        [JsonProperty("1")]
        public string first { get; set; }
        [JsonProperty("2")]
        public string second { get; set; }
        [JsonProperty("3")]
        public string third { get; set; }
        [JsonProperty("4")]
        public string fourth { get; set; }
        [JsonProperty("5")]
        public string fifth { get; set; }
    }
    public class user_details
    {
        public string number { get; set; }
    }

    public class notifications
    {
        public string templateId { get; set; }
        public string type { get; set; }
        public string sender { get; set; }
        [JsonProperty("namespace")]
        public string namespaces { get; set; }
        [JsonProperty("params")]
        public Parameter paramss { get; set; }
    }


    public class input
    {
        public user_details userDetails { get; set; }
        public notifications notification { get; set; }
    }
}
