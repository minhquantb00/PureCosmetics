using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.Commons.Base
{
    public abstract class BaseEntity<TEntity>
    {
        [Key]
        public TEntity Id { get; set; }
        public int NumericalOrder { get; set; }
    }
}
