using Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDocumentationApi();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseDocumentarionApi();

app.Run();
