using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace intSoft.MVC.Core.Identity
{
    public interface ICurrentUser<out TUser>
        where TUser: IUser<Guid>
    {
        TUser User { get; }
        List<string> CurrentUserRoles { get; }
        List<string> CurrentUserOperations { get; }
    }
}