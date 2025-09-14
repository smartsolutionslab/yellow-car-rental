var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.YellowCarRental_Api>("yellowcarrental-api")
    .WithHttpHealthCheck("/health");

var frontendPublic = builder.AddProject<Projects.YellowCarRental_Frontend_Public>("yellowcarrental-frontend-public")
    .WithReference(api)
    .WaitFor(api)
    //.WithEnvironment("ApiBaseUrl", api.GetEndpoint("http").Url)
    .WithExternalHttpEndpoints();

var frontendCallCenter = builder.AddProject<Projects.YellowCarRental_Frontend_CallCenter>("yellowcarrental-frontend-call-center")
    .WithReference(api)
    .WaitFor(api)
    //.WithEnvironment("ApiBaseUrl", api.GetEndpoint("http").Url)
    .WithExternalHttpEndpoints();

builder.Build().Run();