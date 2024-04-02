namespace MediportaSOTags.Data
{
    public class TagsRepo : ITagsRepo
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public TagsRepo(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public void AddTags(Dictionary<string, int> tagsDict) {

            var allTagsCount = tagsDict.Sum(x => x.Value);
            foreach (var tag in tagsDict)
            {
                Tags tags = new Tags
                {
                    Name = tag.Key,
                    Count = tag.Value,
                    PercentPart = Calculators.CalculatePercentPart(tag.Value, allTagsCount)
                };
                _appDbContext.Add(tags);
            }
            _appDbContext.SaveChanges();
        }

        public async Task<PagedList<TagsDto>> GetTagsAsync(TagsParams tagsParams)
        {
            var query = _appDbContext.Tags.AsQueryable();
            
            query = tagsParams.OrderBy switch
            {
                "name" => tagsParams.Sort == "desc" ? query.OrderByDescending(u => u.Name) : query.OrderBy(u => u.Name),
                "percentpart" => tagsParams.Sort == "desc" ? query.OrderByDescending(u => u.PercentPart) : query.OrderBy(u => u.PercentPart),
                "count" => tagsParams.Sort == "desc" ? query.OrderByDescending(u => u.Count) : query.OrderBy(u => u.Count),
                _ => tagsParams.Sort == "desc" ? query.OrderByDescending(u => u.Name) : query.OrderBy(u => u.Name)

            };

            return await PagedList<TagsDto>.CreateAsync(query.AsNoTracking().ProjectTo<TagsDto>(_mapper.ConfigurationProvider), tagsParams.PageNumber, tagsParams.PageSize);
        } 

        public async Task CleanDB()
        {
             await _appDbContext.Database.ExecuteSqlRawAsync("DELETE FROM [Tags]");
        }
    }
}
