using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.Commons.Base
{
    /// <summary>
    /// Defines members for entities that support soft deletion, allowing them to be marked as deleted without being
    /// permanently removed from storage.
    /// User create: QuanTM
    /// Created date: 2025/12/11
    /// Last modified date: 2025/12/11
    /// </summary>
    /// <remarks>Implementing this interface enables tracking of deletion status, deletion time, and the user
    /// responsible for deletion. This is commonly used in scenarios where entities should remain accessible for
    /// auditing or recovery purposes after deletion.</remarks>
    public interface IDeletable
    {
        /// <summary>
        /// Gets or sets a value indicating whether the entity has been marked as deleted.
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the item was deleted, if applicable.
        /// </summary>
        DateTime? DeletionTime { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who deleted the entity, if applicable.
        /// </summary>
        int? DeleterUserId { get; set; }
    }
}
