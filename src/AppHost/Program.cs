using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);
/*

var dbPassword = builder.AddParameter("oracle-password");

var oracle = builder.AddOracle("oracle", dbPassword)
    .WithDataVolume()
    .AddDatabase("carrentaldb");

// Keycloak
var adminUsername = builder.AddParameter("username");
var adminPassword = builder.AddParameter("password"); // TODO secret: true);

var keycloak = builder.AddKeycloak("keycloak", 8080, adminUsername, adminPassword)
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);
*/
// Projects
/*var api = builder.AddProject<Projects.Api>("api")
    .WithReference(oracle)
    .WaitFor(oracle)
    .WithReference(keycloak)
    .WaitFor(keycloak)
    // Inject Keycloak for API too (override appsettings if needed)
    .WithEnvironment("Keycloak__Authority", keycloak.GetEndpoint("http").Url + "/realms/orange")
    .WithEnvironment("Keycloak__ClientId", "orange-api")
    .WithExternalHttpEndpoints();
*/

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