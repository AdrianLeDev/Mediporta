namespace MediportaSOTags.Data
{
    public class DBSeed
    {



        public async static void Seed(AppDbContext appDbContext, ISOTagsService sOTagsService, ITagsRepo tagsRepo, ILogger logger)
        {
            logger.LogInformation("Checking database...");
            if (appDbContext.Database.CanConnect())
            {
                logger.LogInformation("DB can connect");
                if (appDbContext.Database.IsRelational())
                {
                    logger.LogInformation("DB IsRelational");
                    var pendingMigrations = appDbContext.Database.GetPendingMigrations();
                    if (pendingMigrations != null && pendingMigrations.Any())
                    {
                        logger.LogInformation("Adding migrations");
                        appDbContext.Database.Migrate();
                    }
                    if (!appDbContext.Tags.Any())
                    {
                        logger.LogInformation("Clean DB, starting seed...");
                        var tagsDict = await sOTagsService.GetSOTags();
                        tagsRepo.AddTags(tagsDict);
                    }

                }
                if (appDbContext.Database.IsInMemory())
                {
                    if (!appDbContext.Tags.Any())
                    {
                        Dictionary<string, int> tagsDictionary = new Dictionary<string, int>();
                        Response response;
                        using (StreamReader r = new StreamReader("TestDB.json"))
                        {
                            string json = r.ReadToEnd();
                            response = JsonSerializer.Deserialize<Response>(json);
                        }
                        foreach (var tag in response.items)
                        {
                            tagsDictionary.Add(tag.name, tag.count);
                        }
                        tagsRepo.AddTags(tagsDictionary);
                    }
                }

            }
        }
    }
}
