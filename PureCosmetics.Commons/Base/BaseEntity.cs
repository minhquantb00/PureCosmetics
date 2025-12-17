using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.Commons.Base
{
    /// <summary>
    /// Base entity class with generic identifier.
    /// User create: QuanTM
    /// Created date: 2025/12/11
    /// Last modified date: 2025/12/11
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseEntity<TEntity>
    {
        /// <summary>
        /// Primary key identifier.
        /// </summary>
        [Key]
        public TEntity Id { get; set; } = default!;

        /// <summary>
        /// Gets or sets the numerical order value used to determine the position or sequence of the item within a
        /// collection.
        /// </summary>
        public int NumericalOrder { get; set; }
    }
}
