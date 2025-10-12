using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.Commons.Base
{
    public interface IDeletable
    {
        bool IsDeleted { get; set; }
        DateTime? DeletionTime { get; set; }
        int? DeleterUserId { get; set; }
    }
}
