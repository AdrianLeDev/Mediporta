namespace MediportaSOTags.IntegrationTests
{
    public class TagsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private HttpClient _client;
        private WebApplicationFactory<Program> _factory;
        public TagsControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var dbContextOptions = services.Single(service => service.ServiceType == typeof(DbContextOptions<AppDbContext>));
                        services.Remove(dbContextOptions);
                        services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMemoryDB"));
                    });
                });
            _client = _factory.CreateClient();
            
            
        }



        [Fact]
        public async Task CompareFirstTagsPage_WithoutAddedParameters_CompareOK()
        {
            List<TagsDto> testFileResponse;
            using (StreamReader r = new StreamReader("TagsControllerTestData.json"))
            {
                string json = r.ReadToEnd();
                testFileResponse = JsonConvert.DeserializeObject<List<TagsDto>>(json);
            }

            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
            var result = await _client.GetAsync("v1/tags");
            result.EnsureSuccessStatusCode();
            var responseBody = await result.Content.ReadAsStringAsync();
            var tags = JsonConvert.DeserializeObject<List<TagsDto>>(responseBody);
            tags.Should().BeEquivalentTo(testFileResponse);

            

        }

        [Fact]
        public async Task GetFirstPage_WithoutAddedParameters_ReturnsOkResult()
        {
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
            var result = await _client.GetAsync("v1/tags");
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetFirstPage_WithWrongSortParameter_ReturnsBadReqest()
        {
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
            var result = await _client.GetAsync("v1/tags/?OrderBy=percentpartt&Sort=desc");
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}