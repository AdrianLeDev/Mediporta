namespace MediportaSOTags.Interfaces
{
    public interface ISOTagsService
    {
        public Task<Dictionary<string, int>> GetSOTags();
    }
}
