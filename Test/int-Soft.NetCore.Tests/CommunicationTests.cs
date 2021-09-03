using System.Threading.Tasks;
using int_Soft.NetCore.Communication;
using int_Soft.NetCore.Communication.EmailServices;
using int_Soft.NetCore.Communication.SmsServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace int_Soft.NetCore.Tests
{
    [TestClass]
    public class CommunicationTests
    {
        [TestMethod]
        public async Task TestSendGridEmailService()
        {
            var sendGridService = new SendGridEmailService();
            var result = await sendGridService.SendAsync(new Message
            {
                Destination = "TEST@gmail.com",
                From = "test@mentoria.me",
                Body = "This is working!",
                Subject = "Test Email",
                FromDisplayText = "Testing SendGridEmailService"
            });

            Assert.IsTrue(result.Succeeded);
        }

        [TestMethod]
        public async Task TestNexmoSmsService()
        {
            var nexmoSmsService = new NexmoSmsService();
            var result = await nexmoSmsService.SendAsync(new Message
            {
                Destination = "+201099999999",
                From = "test@DOMAIN.COM",
                Body = "This is working!",
                Subject = "Test Email",
                FromDisplayText = "Testing SendGridEmailService"
            });

            Assert.IsTrue(result.Succeeded);
        }
    }
}
