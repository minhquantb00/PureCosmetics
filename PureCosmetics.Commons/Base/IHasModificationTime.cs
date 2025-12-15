using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.Commons.Base
{
    /// <summary>
    /// Defines properties for tracking the last modification time and the user who last modified an entity.
    /// User create: QuanTM
    /// Created date: 2025/12/11
    /// Last modified date: 2025/12/11
    /// </summary>
    /// <remarks>Implement this interface to provide auditing information about when an entity was last
    /// changed and by whom. This is commonly used in scenarios where change history or user accountability is
    /// required.</remarks>
    public interface IHasModificationTime
    {
        /// <summary>
        /// Gets or sets the date and time when the object was last modified.
        /// </summary>
        DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// Gets or sets the user ID of the last person who modified the entity.
        /// </summary>
        int? LastModifierUserId { get; set; }
    }
}
