using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using intSoft.MVC.Core.HTTPApplication;
using Microsoft.AspNet.Identity;

namespace intSoft.MVC.Core.Utilities
{
    public class SmsService : IIdentityMessageService
    {
        public virtual async Task SendAsync(IdentityMessage message)
        {
            var freeList = new List<string>
            {
                "+201093699196",
                "+201121934669",
                "+201001683081",
                "+963999054520"
            };
            if (!freeList.Contains(message.Destination))
                await Task.FromResult(0);
            var configuration = DependencyResolver.Current.GetService<ApplicationConfiguration>();
            var myParameters = string.Format("api_key={0}&api_secret={1}&to={2}&from={3}&text={4}",
                configuration.SMSKey, configuration.SMSSecret, message.Destination, message.Subject, message.Body);

            using (var wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var result = wc.UploadString(configuration.SMSServiceUrl, myParameters);
                await Task.FromResult(result);
            }
        }
    }
}