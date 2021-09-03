using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace int_Soft.NetCore.Communication.SmsServices
{
    public class NexmoSmsService: IMessageService
    {
        public async Task<MessageServiceResult> SendAsync(Message message)
        {
            var freeList = new List<string>
            {
                "+201093699196",
                "+201121934669",
                "+201001683081",
                "+963999054520"
            };
            if (!freeList.Contains(message.Destination))
                return await Task.FromResult(MessageServiceResult.Error("Number not allowed"));

            var smsKey = ConfigurationManager.AppSettings[CommunicationMagicStrings.NexmoSmsKey];
            var smsSecret = ConfigurationManager.AppSettings[CommunicationMagicStrings.NexmoSmsSecret];
            var smsUrl = ConfigurationManager.AppSettings[CommunicationMagicStrings.NexmoServiceUrl];
            
            if (string.IsNullOrEmpty(smsKey) || string.IsNullOrEmpty(smsSecret) || string.IsNullOrEmpty(smsUrl))
                throw new Exception("Nexmo service configuration is missed!");
            
            var myParameters = string.Format("api_key={0}&api_secret={1}&to={2}&from={3}&text={4}",
                smsKey, smsSecret, message.Destination, message.Subject, message.Body);

            using (var wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var result = JsonConvert.DeserializeObject<NexmoSmsResult>(wc.UploadString(smsUrl, myParameters));
                return result.Messages[0].Status == NexmoSmsStatus.Success
                    ? await Task.FromResult(MessageServiceResult.Success())
                    : await Task.FromResult(MessageServiceResult.Error(result.Messages[0].ErrorText));
            }
        }
    }
}