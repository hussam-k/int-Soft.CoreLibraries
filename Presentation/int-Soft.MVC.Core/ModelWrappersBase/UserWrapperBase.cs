namespace intSoft.MVC.Core.ModelWrappersBase
{
    public abstract class UserWrapperBase<TUser>: ModelWrapperBase<TUser> 
        where TUser : class, new()

    {
        protected UserWrapperBase(TUser user) : base(user)
        {
            
        }

        protected UserWrapperBase()
        {
            
        }

        public abstract string Email { get; set; }

        public abstract string ConfirmEmail { get; set; }

        public abstract string Password { get; set; }

        public abstract string ConfirmPassword { get; set; }

        public abstract string UserName { get; set; }
    }
}