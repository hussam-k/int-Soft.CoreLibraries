namespace int_Soft.NetCore.Communication
{
    public static class CommunicationMagicStrings
    {
        /****** NEXMO ******/
        public const string NexmoSmsKey = "SMSKey";
        public const string NexmoSmsSecret = "SMSSecret";
        public const string NexmoServiceUrl = "SMSServiceUrl";
        
        /****** SendGrid ******/
        public const string SendGridHost = "EmailProviderHostAddress";
        public const string SendGridEnableSsl = "EmailProviderUseSSL";
        public const string SendGridPort = "EmailProviderPort";
        public const string SendGridUsername = "EmailProviderUsername";
        public const string SendGridPassword = "EmailProviderPassword";

    }
}