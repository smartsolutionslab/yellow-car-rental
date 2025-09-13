var builder = DistributedApplication.CreateBuilder(args);

var frontendPublic = builder.AddProject<Projects.YellowCarRental_Frontend_Public>("yellowcarrental-frontend-public");

builder.Build().Run();