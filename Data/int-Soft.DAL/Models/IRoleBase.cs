using System;

namespace IntSoft.DAL.Models
{
    public interface IRoleBase
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}