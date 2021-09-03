using System;
using IntSoft.DAL.Common;

namespace IntSoft.DAL.Models
{
    public interface IUserClaimBase: IEntity
    {
        Guid UserId { get; set; }
        string ClaimType { get; set; }
        string ClaimValue { get; set; }
    }
}