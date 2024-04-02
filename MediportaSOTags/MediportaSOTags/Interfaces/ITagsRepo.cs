namespace MediportaSOTags.Interfaces
{
    public interface ITagsRepo
    {
        public void AddTags(Dictionary<string, int> tagsDict);

        public Task<PagedList<TagsDto>> GetTagsAsync(TagsParams tagsParams);

        public Task CleanDB();
    }
}
