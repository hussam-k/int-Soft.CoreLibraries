namespace intSoft.MVC.Core.ActionResults
{
    public class StandardJsonActionResult<T> : StandardJsonActionResult
    {
        public StandardJsonActionResult(bool isCamelCase = true)
            : base(isCamelCase)
        {
            
        }
        public new T Data
        {
            get { return (T)base.Data; }
            set { base.Data = value; }
        }
    }
}