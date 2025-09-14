var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.YellowCarRental_Api>("api")
    .WithHttpHealthCheck("/health");

var frontendPublic = builder.AddProject<Projects.YellowCarRental_Frontend_Public>("frontend-public")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(api)
    .WaitFor(api);
    //.WithEnvironment("ApiBaseUrl", api.GetEndpoint("http").Url)


var frontendCallCenter = builder.AddProject<Projects.YellowCarRental_Frontend_CallCenter>("frontend-call-center")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(api)
    .WaitFor(api);
//.WithEnvironment("ApiBaseUrl", api.GetEndpoint("http").Url)
    

builder.Build().Run();