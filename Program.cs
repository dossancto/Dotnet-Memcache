using Enyim.Caching;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEnyimMemcached(o => o.Servers = new() { new() { Address = "localhost", Port = 11211 } });

int TIME_TO_LIVE = 10;

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");

app.MapGet("/get/{userid}", async (string userid, IMemcachedClient cache) =>
{
    if (!cache.TryGet(userid, out People user))
    {
        user = People.GetPeople(userid);
        await cache.SetAsync(userid, user, TIME_TO_LIVE);
    }

    return user;
});

app.Run();
