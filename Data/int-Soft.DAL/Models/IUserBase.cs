using System;
using System.Collections.Generic;

namespace IntSoft.DAL.Models
{
    public interface IUserBase<TUserRole, TUserClaim, TUserLogin> 
        where TUserRole: IUserRoleBase
        where TUserLogin : IUserLoginBase
        where TUserClaim : IUserClaimBase
    {
        string Email { get; set; }
        bool EmailConfirmed { get; set; }
        string PasswordHash { get; set; }
        string SecurityStamp { get; set; }
        string PhoneNumber { get; set; }
        string PreferredLanguage { get; set; }
        bool PhoneNumberConfirmed { get; set; }
        bool TwoFactorEnabled { get; set; }
        DateTime? LockoutEndDateUtc { get; set; }
        bool LockoutEnabled { get; set; }
        int AccessFailedCount { get; set; }
        ICollection<TUserClaim> UserClaims { get; set; }
        ICollection<TUserLogin> UserLogins { get; set; }
        ICollection<TUserRole> UserRoles { get; set; }
    }
}