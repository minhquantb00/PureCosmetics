using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.Commons.Paginations
{
    public class PagedResult<T>
    {
        public PagedResult(Pagination pagination, IEnumerable<T> results)
        {
            Pagination = pagination;
            Data = results;
        }
        public PagedResult() { }
        public Pagination Pagination { get; set; }
        public IEnumerable<T> Data { get; set; }
        public static async Task<PagedResult<T>> ToPagedResultAsync(Pagination pagination, IQueryable<T> query)
        {
            PagedResult<T> pagedResult = new PagedResult<T>();
            pagedResult.Pagination = pagination;
            if (pagedResult.Pagination == null)
                pagedResult.Pagination = new Pagination();

            var totalRecords = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pagination.ItemsPerPage);

            pagedResult.Pagination.Page = pagination.Page < 1 ? 0 : pagination.Page - 1;
            if (pagination.ItemsPerPage <= 0)
            {
                pagedResult.Data = query.ToList();
            }
            else
            {
                pagedResult.Data = query.Skip(pagination.ItemsPerPage * pagination.Page)
                    .Take(pagination.ItemsPerPage)
                    .ToList();
            }

            pagedResult.Pagination.Records = pagedResult.Data.Count();
            pagedResult.Pagination.TotalItems = totalRecords;
            pagedResult.Pagination.TotalPages = totalPages;

            return pagedResult;
        }
        public static PagedResult<T> ToPagedResult(Pagination pagination, IEnumerable<T> query)
        {
            PagedResult<T> pagedResult = new PagedResult<T>();
            pagedResult.Pagination = pagination;
            if (pagedResult.Pagination == null)
                pagedResult.Pagination = new Pagination();

            var totalRecords = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pagination.ItemsPerPage);

            pagedResult.Pagination.Page = pagination.Page < 1 ? 0 : pagination.Page - 1;
            if (pagination.ItemsPerPage <= 0)
            {
                pagedResult.Data = query.ToList();
            }
            else
            {
                pagedResult.Data = query.Skip(pagination.ItemsPerPage * pagination.Page)
                    .Take(pagination.ItemsPerPage)
                    .ToList();
            }

            pagedResult.Pagination.Records = pagedResult.Data.Count();
            pagedResult.Pagination.TotalItems = totalRecords;
            pagedResult.Pagination.TotalPages = totalPages;

            return pagedResult;
        }
        public static IQueryable<T> ToPagedQuery(Pagination pagination, IQueryable<T> query)
        {
            PagedResult<T> pagedResult = new PagedResult<T>();
            pagedResult.Pagination = pagination;
            if (pagedResult.Pagination == null)
                pagedResult.Pagination = new Pagination();

            pagedResult.Pagination.Page = pagination.Page < 1 ? 0 : pagination.Page - 1;
            if (pagination.ItemsPerPage > 0)
            {
                query = query.Skip(pagination.ItemsPerPage * pagination.Page)
                    .Take(pagination.ItemsPerPage);
            }

            return query;
        }
    }
}
