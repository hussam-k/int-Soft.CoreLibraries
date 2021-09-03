using System;

namespace IntSoft.DAL.Common
{
    public interface IApprovableEntity : IEntity
    {
        Guid? ApprovedById { get; set; }
        DateTime? ApproveDate { get; set; }
    }
}