namespace MediportaSOTags.Services
{
    public class SOTagsService : ISOTagsService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SOTagsService> _logger;

        public SOTagsService(HttpClient httpClient, ILogger<SOTagsService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<Dictionary<string, int>> GetSOTags()
        {
            _logger.LogInformation("Downloading tags from SO");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
            Dictionary<string, int> tagsDictionary = new Dictionary<string, int>();
            try
            {
                for (var page = 1; page <= 25; page++)
                {
                    await Task.Delay(2000);
                    var response = await _httpClient.GetAsync($"https://api.stackexchange.com/2.3/tags?site=stackoverflow&pagesize=100&page={page}");
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var tags = JsonSerializer.Deserialize<Response>(responseBody);
                    foreach (var tag in tags.items)
                    {
                        tagsDictionary.Add(tag.name, tag.count);
                    }

                }
                return tagsDictionary;
            }
            catch (Exception ex) 
            {
                _logger.LogError("Cannot download tags from SO.");
                throw new Exception(ex.Message);
            }
        }
    }
}
