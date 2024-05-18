using FeedStories.Common.Middlewares;
using FeedStories.Common.Utilities.Infrastructure;
using FeedStories.WebApi.Contracts.Request;
using FeedStories.WebApi.Contracts.Response;
using FeedStories.WebApi.RequestHandler;
using FeedStories.WebApi.RequestHandler.Handlers;

#region Configuration
var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;
#endregion

#region Configure Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpHelper(configuration["BaseURI"]);
builder.Services.AddSingleton<IRequestHandlerFactory, RequestHandlerFactory>();
builder.Services.AddSingleton<IRequestHandler<EmptyRequest, StoryIdResponse>, GetStoryIdsRequestHandler>();
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