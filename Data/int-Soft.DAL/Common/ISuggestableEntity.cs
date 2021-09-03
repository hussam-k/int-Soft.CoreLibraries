using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntSoft.DAL.Common
{
    public interface ISuggestableEntity
    {
        Guid? SuggestedById { get; set; }
    }
}
