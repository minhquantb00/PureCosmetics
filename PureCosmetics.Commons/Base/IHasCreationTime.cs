using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.Commons.Base
{
    /// <summary>
    /// Defines properties for tracking the creation time and creator of an entity.
    /// User create: QuanTM
    /// Created date: 2025/12/11
    /// Last modified date: 2025/12/11
    /// </summary>
    /// <remarks>Implement this interface to provide standardized auditing information for entities, including
    /// when they were created and by which user. This is commonly used in systems that require tracking of entity
    /// lifecycle events for auditing or history purposes.</remarks>
    public interface IHasCreationTime
    {
         DateTime CreationTime { get;  set; }
         int? CreatorUserId { get; set; }
    }
}
