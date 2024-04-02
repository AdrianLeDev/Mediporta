using MediportaSOTags.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "MediportaSOTags - V1",
            Description = "This is a interview task for MediPorta",
            Version = "v1"
        }
     );

    var filePath = Path.Combine(System.AppContext.BaseDirectory, "MediportaSOTags.xml");
    c.IncludeXmlComments(filePath);
});

builder.Services.AddDbContext<AppDbContext>(opt => { opt.UseSqlite("Data Source=MPSOT_DB.db"); });
builder.Services.AddHttpClient<ISOTagsService, SOTagsService>().ConfigurePrimaryHttpMessageHandler(config => new HttpClientHandler { AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate});
builder.Services.AddScoped<ITagsRepo, TagsRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Logger.LogInformation("Creating scope for services");
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
app.Logger.LogInformation("Scope for services have been created.");
try
{
    app.Logger.LogInformation("Getting services for seed.");
    var context = services.GetRequiredService<AppDbContext>();
    var tagsService = services.GetRequiredService<ISOTagsService>();
    var tagsRepo = services.GetRequiredService<ITagsRepo>();
    DBSeed.Seed(context, tagsService, tagsRepo);
}
catch (Exception ex)
{
    app.Logger.LogError(ex, "An error occurred during migration or DB seed");
}
app.Run();

public partial class Program { }