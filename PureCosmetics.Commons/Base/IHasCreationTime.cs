using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.Commons.Base
{
    public interface IHasCreationTime
    {
         DateTime CreationTime { get;  set; }
         int? CreatorUserId { get; set; }
    }
}
