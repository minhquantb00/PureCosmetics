using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.Commons.Base
{
    /// <summary>
    /// Defines a contract for objects that can be activated or deactivated
    /// User create: QuanTM
    /// Created date: 2025/12/11
    /// Last modified date: 2025/12/11
    /// </summary>
    public interface IActivatable
    {
        /// <summary>
        /// Gets or sets a value indicating whether the current instance is active.
        /// </summary>
        bool IsActive { get; set; }
    }
}
