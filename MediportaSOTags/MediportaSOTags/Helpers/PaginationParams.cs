namespace MediportaSOTags.Helpers
{
    public class PaginationParams
    {
        private const int MaxPageSize = 50;

        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        /// <summary>
        /// Max page size = 50.
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
