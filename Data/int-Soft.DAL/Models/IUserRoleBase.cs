using System;
using IntSoft.DAL.Common;

namespace IntSoft.DAL.Models
{
    public interface IUserRoleBase : IEntity
    {
        Guid UserId { get; set; }
        Guid RoleId { get; set; } 
    }
}