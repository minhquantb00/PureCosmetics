using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.Commons.Base
{
    public interface IHasModificationTime
    {
        DateTime? LastModificationTime { get; set; }
        int? LastModifierUserId { get; set; }
    }
}
