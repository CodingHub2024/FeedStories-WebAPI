using FeedStories.Common.Middlewares;
using FeedStories.Common.Utilities.Extension;
using FeedStories.WebApi.Contracts.Request;
using FeedStories.WebApi.Contracts.Response;
using FeedStories.WebApi.RequestHandler;
using FeedStories.WebApi.RequestHandler.Handlers;

#region Configuration
var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;
#endregion

#region Configure Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole(options =>
{
    options.LogToStandardErrorThreshold = LogLevel.Debug;
});
builder.Logging.AddDebug();
#endregion

#region Configure Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddStoryService(configuration["BaseURI"]);

#endregion

#region Configure Services of RequestHandler
builder.Services.AddSingleton<IRequestHandlerFactory, RequestHandlerFactory>();
builder.Services.AddSingleton<IRequestHandler<EmptyRequest, StoryIdResponse>, GetStoryIdsRequestHandler>();
builder.Services.AddSingleton<IRequestHandler<StoryIdRequest, StoryDetailResponse>, GetStoryDetailsRequestHandler>();
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

app.HandleExceptions();
    
app.MapControllers();

app.Run();

#endregion