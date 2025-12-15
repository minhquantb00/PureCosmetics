using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.Commons.Paginations
{
    /// <summary>
    /// Pagination metadata for paged results.
    /// User create: QuanTM
    /// Created date: 2025/12/11
    /// Last modified date: 2025/12/11
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// Constructor to initialize default pagination values.
        /// </summary>
        public Pagination()
        {
            Page = 1;
            ItemsPerPage = 25;
            SortBy = string.Empty;
            Descending = true;
            IncludeEntities = false;
        }

        /// <summary>
        /// Page number
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Items in each page
        /// </summary>
        public int ItemsPerPage { get; set; }

        /// <summary>
        /// Records in current page
        /// </summary>
        public int Records { get; set; }

        /// <summary>
        /// Total items
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Total pages
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Sort by field
        /// </summary>
        public string SortBy { get; set; }

        /// <summary>
        /// Descending order
        /// </summary>
        public bool Descending { get; set; }

        /// <summary>
        /// Include related entities
        /// </summary>
        public bool IncludeEntities { get; set; }
    }
}
