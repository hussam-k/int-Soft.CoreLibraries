using intSoft.MVC.Core.Helpers;

namespace intSoft.MVC.Core.Tasks
{
    public class AntiForgeryTokenTask : IRunOnEachRequest
    {
        public void Execute()
        {
            //AntiForgeryTokenHelper.SetAntiForgeryToken();
        }
    }
}