using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.Commons.Paginations
{
    public class Pagination
    {
        public Pagination()
        {
            Page = 1;
            ItemsPerPage = 25;
            SortBy = null;
            Descending = true;
            IncludeEntities = false;
        }
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
        public int Records { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public string SortBy { get; set; }
        public bool Descending { get; set; }
        public bool IncludeEntities { get; set; }
    }
}
