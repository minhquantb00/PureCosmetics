using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.Commons.Paginations
{
    /// <summary>
    /// Paged result set containing pagination metadata and the data items.
    /// User create: QuanTM
    /// Created date: 2025/12/11
    /// Last modified date: 2025/12/11
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResult<T>
    {
        /// <summary>
        /// Constructor to initialize a paged result with pagination info and data items.
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="results"></param>
        public PagedResult(Pagination pagination, IEnumerable<T> results)
        {
            Pagination = pagination;
            Data = results;
        }

        /// <summary>
        /// Constructor to initialize an empty paged result.
        /// </summary>
        public PagedResult() { }

        /// <summary>
        /// Pagination metadata.
        /// </summary>
        public Pagination Pagination { get; set; }

        /// <summary>
        /// Data items for the current page.
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// Pages the given queryable to a paged result asynchronously.
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="query"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Pages the given enumerable to a paged result.
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="query"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Pages the given queryable.
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="query"></param>
        /// <returns></returns>
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
