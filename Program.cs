using ApiCatalogo.AppServicesExtensions;
using CatalogMinimalApi.ApiEndpoints;
using CatalogMinimalApi.AppServicesExtensions;
using CatalogMinimalApi.Context;
using CatalogMinimalApi.Models;
using CatalogMinimalApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiSwagger();
builder.AddPersistence();
builder.Services.AddCors();
builder.AddAutenticationJwt();

var app = builder.Build();

//endpoints
app.MapAuthenticationEndpoints();
app.MapCategoriesEndpoints();
app.MapProductsEndpoints();

var environment = app.Environment;
app.UseExceptionHandling(environment)
    .UseSwaggerMiddleware()
    .UseAppCors();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

