namespace HRManagement.BusinessLogic.Helpers
{
    public class PageResult<T> where T : class
    {
        /// <summary>
        /// The size of the page
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// The total number of page in the database
        /// </summary>
        public int TotalNumberPage { get; set; }

        /// <summary>
        /// The current page
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// The page count
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// The data
        /// </summary>
        public List<T>? Data { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="PageResult{T}"/>
        /// </summary>
        /// <param name="numberPage"></param>
        /// <param name="size"></param>
        /// <param name="totalNumberPage"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageCount"></param>
        /// <param name="data"></param>
        public PageResult(int size, int currentPage, int pageCount, List<T>? data)
        {
            Size = size;
            TotalNumberPage = (int)Math.Ceiling(pageCount / (double)size);
            CurrentPage = currentPage;
            PageCount = pageCount;
            Data = data;
        }
    }
}
