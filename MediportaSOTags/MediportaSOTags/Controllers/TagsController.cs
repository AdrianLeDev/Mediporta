using System.ComponentModel.DataAnnotations;

namespace MediportaSOTags.Controllers
{
    [ApiController]
    [Route("v1/tags")]
    public class TagsController: ControllerBase
    {
        private readonly ITagsRepo _tagsRepo;
        private readonly ISOTagsService _sOTagsService;

        public TagsController(ITagsRepo tagsRepo, ISOTagsService sOTagsService)
        {
            _tagsRepo = tagsRepo;
            _sOTagsService = sOTagsService;
        }
        /// <summary>
        /// Returns the tags found on a StackOverflow page.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<TagsDto>> GetTags([FromQuery] TagsParams tagsParams)
        { 
            
            if((tagsParams.Sort != "desc" &&  tagsParams.Sort != "asc") | (tagsParams.OrderBy != "name" && tagsParams.OrderBy != "count" && tagsParams.OrderBy != "percentpart" && tagsParams.OrderBy != ""))
            {
                return BadRequest("Wrong parameter value.");
            }
            var tagsList = await _tagsRepo.GetTagsAsync(tagsParams);
            Response.AddPaginationHeader(new PaginationHeader(tagsList.CurrentPage, tagsList.PageSize, tagsList.TotalCount, tagsList.TotalPages));

            return Ok(tagsList);
        }
        /// <summary>
        /// Reloading tags from StackOverflow to local DB.
        /// </summary>
        [HttpGet]
        [Route("reloadTags")]
        public async Task ReloadTags()
        {
            await _tagsRepo.CleanDB();
            var tags = await _sOTagsService.GetSOTags();
            _tagsRepo.AddTags(tags);
        }
    }
}
