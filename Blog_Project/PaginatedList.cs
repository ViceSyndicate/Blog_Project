using Microsoft.EntityFrameworkCore;

namespace Blog_Project
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }
        // True if we're not at PageIndex 1. 
        public bool HasPreviousPage => PageIndex > 1;
        // True if current Page is less than the total nr of pages.
        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
        public static async Task<PaginatedList<T>> Create(List<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count;
            // source.Skip((pageIndex - 1) * pageSize) should get the nr of page +1 * 5
            // to skip all the previous items in the list 
            // .Take(pageSize).ToList(); Takes the coming 5 items and turns them to a list. 
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
