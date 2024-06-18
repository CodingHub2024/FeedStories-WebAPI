using FeedStories.Common.Middlewares;
using FeedStories.Common.Utilities.Extension;
using FeedStories.WebApi.Contracts.Request;
using FeedStories.WebApi.Contracts.Response;
using FeedStories.WebApi.RequestHandler;
using FeedStories.WebApi.RequestHandler.Handlers;
using FeedStories.Common.Filters;

#region Configuration
var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;
#endregion

#region Configure Logging
builder.Host.AddSerilog(configuration);
#endregion

#region Configure Services
builder.Services.AddControllers();
builder.Services.ValidateModels();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddStoryService(configuration);
// Register IMemoryCache
builder.Services.AddMemoryCache();
#endregion

#region Configure CORS
// Configure CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder => builder
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader());
});
#endregion

#region Configure Services of RequestHandler
builder.Services.AddTransient<IRequestHandlerFactory, RequestHandlerFactory>();
builder.Services.AddTransient <IRequestHandler<StoryRequest, StoryResponse>, GetStoriesRequestHandler>();
#endregion

#region Configure Request Processing Pipeline
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.UseCors("AllowSpecificOrigins");

app.HandleExceptions();

app.MapControllers();

app.Run();

#endregion