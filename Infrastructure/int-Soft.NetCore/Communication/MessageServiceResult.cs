namespace int_Soft.NetCore.Communication
{
    public class MessageServiceResult
    {
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }

        public static MessageServiceResult Success()
        {
            return new MessageServiceResult
            {
                Succeeded = true
            };
        }

        public static MessageServiceResult Error(string message)
        {
            return new MessageServiceResult
            {
                ErrorMessage = message
            };
        }
    }
}