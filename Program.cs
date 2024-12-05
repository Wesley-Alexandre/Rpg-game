var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5089");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseStaticFiles(); 

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.MapGet("/", () => Results.Redirect("/index.html"));

app.Run();
