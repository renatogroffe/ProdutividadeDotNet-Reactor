using Microsoft.AspNetCore.Mvc.ApiExplorer;
using APIContagem.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioningConfigurations();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    // Geracaoo de um endpoint do Swagger para cada versao descoberta
    foreach (var description in
        app.Services.GetRequiredService<IApiVersionDescriptionProvider>().ApiVersionDescriptions)
    {
        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
            description.GroupName.ToUpperInvariant());
    }
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();