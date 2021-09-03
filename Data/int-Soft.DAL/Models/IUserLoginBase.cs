using System;

namespace IntSoft.DAL.Models
{
    public interface IUserLoginBase
    {
        string LoginProvider { get; set; }
        string ProviderKey { get; set; }
        Guid UserId { get; set; }
         
    }
}