using CoinSight.Core.Interfaces;
using CoinSight.Handlers;
using CoinSight.Services.Clients;

const string policyName = "CorsPolicy";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddTransient<IPriceFluctuationHandler, PriceFluctuationHandler>();
builder.Services.AddTransient<ITrendsHandler, TrendsHandler>();
builder.Services.AddTransient<IMarketDominanceHandler, MarketDominanceHandler>();
builder.Services.AddTransient<ICoinGeckoClient, CoinGeckoClient>();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: policyName, policyBuilder =>
    {
        policyBuilder.WithOrigins("https://localhost:44400")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(policyName);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();