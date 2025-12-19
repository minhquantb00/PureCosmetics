var builder = WebApplication.CreateBuilder(args);

// Đọc cấu hình Reverse Proxy từ appsettings.json
builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.MapGet("/", () => "API Gateway running 🚀");

// Map gateway
app.MapReverseProxy();

app.Run();