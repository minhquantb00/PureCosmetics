var builder = WebApplication.CreateBuilder(args);

// Đọc cấu hình Reverse Proxy từ appsettings.json
builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "swagger";

    c.SwaggerEndpoint("/auth/swagger/v1/swagger.json", "Auth Service");
    c.SwaggerEndpoint("/email/swagger/v1/swagger.json", "Email Service");
});

app.MapGet("/", () => "Gateway OK");


app.MapReverseProxy();

app.Run();