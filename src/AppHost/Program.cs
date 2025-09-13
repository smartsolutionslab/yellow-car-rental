var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.YellowCarRental_Api>("yellowcarrental-api")
    .WithEndpoint();

var frontendPublic = builder.AddProject<Projects.YellowCarRental_Frontend_Public>("yellowcarrental-frontend-public")
    .WithReference(api)
    .WithEnvironment("ApiBaseUrl", api.GetEndpoint("http").Url)
    .WithEndpoint();

var frontendCallCenter = builder.AddProject<Projects.YellowCarRental_Frontend_CallCenter>("yellowcarrental-frontend-call-center")
    .WithReference(api)
    .WithEnvironment("ApiBaseUrl", api.GetEndpoint("http").Url)
    .WithEndpoint();

builder.Build().Run();