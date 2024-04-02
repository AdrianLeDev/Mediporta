namespace MediportaSOTags.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() {

            CreateMap<Tags, TagsDto>();
        }
    }
}
