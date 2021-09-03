namespace int_Soft.NetCore.Communication
{
    public class Message
    {
        public virtual string From { get; set; }
        public virtual string FromDisplayText { get; set; }
        public virtual string Destination { get; set; }
        public virtual string Subject { get; set; }
        public virtual string Body { get; set; } 
    }
}