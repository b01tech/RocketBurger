using Api.Extensions;
using Application.Extensions;
using Infra.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddDocumentationApi()
    .AddCorsApi()
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

var app = builder.Build();

app.UseGlobalExceptionHandler();
app.UseCorsApi();
app.UseHttpsRedirection();
app.MapEndpoints();
app.UseDocumentarionApi();

app.Run();
