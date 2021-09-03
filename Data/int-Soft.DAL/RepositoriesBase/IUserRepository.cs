using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IntSoft.DAL.Models;

namespace IntSoft.DAL.RepositoriesBase
{
    public interface IUserRepository<TUser, TUserRole, TUserClaim, TUserLogin> : IRepository<TUser>
        where TUser : class, IUserBase<TUserRole, TUserClaim, TUserLogin>
        where TUserLogin : class, IUserLoginBase
        where TUserRole : class, IUserRoleBase
        where TUserClaim : class, IUserClaimBase
    {
        Task<IEnumerable<string>> GetRolesAsync(Guid userId);
        Task<IEnumerable<string>> GetOperationsAsync(Guid userId);
    }
}