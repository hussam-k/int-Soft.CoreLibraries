using System.Collections.Generic;
using Newtonsoft.Json;

namespace int_Soft.NetCore.Communication.SmsServices
{
    internal class NexmoSmsResult
    {
        [JsonProperty("message-count")]
        public int MessageCount { get; set; }
        public List<NexmoSmsMessage> Messages { get; set; }
    }

    internal class NexmoSmsMessage
    {
        public string To { get; set; }
        [JsonProperty("message-id")]
        public string MessageId { get; set; }
        public NexmoSmsStatus Status { get; set; }
        [JsonProperty("error-text")]
        public string ErrorText { get; set; }
        [JsonProperty("remaining-balance")]
        public double RemainingBalance { get; set; }
        [JsonProperty("message-price")]
        public double MessagePrice { get; set; }
        public string Network { get; set; }
    }
}