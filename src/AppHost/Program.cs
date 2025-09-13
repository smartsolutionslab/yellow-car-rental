var builder = DistributedApplication.CreateBuilder(args);

var frontendPublic = builder.AddProject<Projects.YellowCarRental_Frontend_Public>("yellowcarrental-frontend-public");

var frontendCallCenter = builder.AddProject<Projects.YellowCarRental_Frontend_CallCenter>("yellowcarrental-frontend-call-center");

builder.Build().Run();