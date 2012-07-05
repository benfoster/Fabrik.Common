using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Fabrik.Common
{
    public interface IPagedList : IEnumerable
    {
        int PageIndex { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }

    public interface IPagedList<T> : IPagedList, IList<T>
    {
    }

    /// <summary>
    /// A tried and tested PagedList implementation
    /// </summary>
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize) :
            this(source.GetPage(pageIndex, pageSize), pageIndex, pageSize, source.Count()) { }

        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
        {
            Ensure.Argument.NotNull(source, "source");
            
            this.TotalCount = totalCount;
            this.TotalPages = totalCount / pageSize;

            if (totalCount % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;

            this.AddRange(source.ToList());
        }

        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }

        public bool HasPreviousPage { get { return (PageIndex > 0); } }
        public bool HasNextPage { get { return (PageIndex + 1 < TotalPages); } }
    }
}
