using System;

namespace IntSoft.DAL.Models
{
    public interface IOperationBase
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Category { get; set; }
    }
}
