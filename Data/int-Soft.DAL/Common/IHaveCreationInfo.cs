using System;

namespace IntSoft.DAL.Common
{
    public interface IHaveCreationInfo
    {
        Guid CreatedBy { get; set; }
        DateTime CreationDate { get; set; }
    }
}