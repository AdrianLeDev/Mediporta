namespace MediportaSOTags.Helpers
{
    public class TagsParams : PaginationParams
    {
        /// <summary>
        /// Default order by "name", you can use "percentpart" or "count"
        /// </summary>
        public string OrderBy { get; set; } = "";
        /// <summary>
        /// Default sort asc, you can use desc
        /// </summary>
        public string Sort { get; set; } = "asc";
    }
}
