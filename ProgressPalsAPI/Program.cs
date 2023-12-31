﻿using Microsoft.Extensions.Configuration;
using ProgressPalsAPI.Cognito.Interfaces;
using ProgressPalsAPI.Cognito.SessionTokenCache;
using ProgressPalsAPI.Domain.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var cognitoSettings = builder.Configuration.GetSection("CognitoClientSettings").Get<CognitoClientSettings>();
// Registering CognitoClientSettings instance
builder.Services.AddSingleton(cognitoSettings);
// Add CognitoClient as a Singleton service
builder.Services.AddSingleton<ICognitoClient, CognitoClient>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add InMemoryAuthenticationResultCache as a Singleton service
builder.Services.AddSingleton<IAuthenticationResultCache, InMemoryAuthenticationResultCache>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

