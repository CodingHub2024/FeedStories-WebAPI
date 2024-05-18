using FeedStories.Common.Utilities.Infrastructure;
using FeedStories.WebApi.Contracts.Request;
using FeedStories.WebApi.Contracts.Response;
using FeedStories.WebApi.RequestHandler;
using FeedStories.WebApi.RequestHandler.Handlers;
var builder = WebApplication.CreateBuilder(args);

#region Configure Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpHelper();
builder.Services.AddSingleton<IRequestHandlerFactory, RequestHandlerFactory>();
builder.Services.AddSingleton<IRequestHandler<EmptyRequest, StoryIdResponse>, GetStoryIdsRequestHandler>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
