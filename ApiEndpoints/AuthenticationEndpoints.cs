using CatalogMinimalApi.Models;
using CatalogMinimalApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace CatalogMinimalApi.ApiEndpoints
{
    public static class AuthenticationEndpoints
    {
        public static void MapAuthenticationEndpoints(this WebApplication app)
        {
            //enpoint login


            app.MapPost("/login", [AllowAnonymous] (UserModel userModel, ITokenService tokenService) =>
            {
                if (userModel == null)
                {
                    return Results.BadRequest("Login Inv�lido");
                }
                if (userModel.UserName == "admin" && userModel.Password == "admin")
                {
                    var tokenString = tokenService.GenerateToken(app.Configuration["Jwt:Key"],
                        app.Configuration["Jwt:Issuer"],
                        app.Configuration["Jwt:Audience"],
                        userModel);
                    return Results.Ok(new { token = tokenString });
                }
                else
                {
                    return Results.BadRequest("Login Inv�lido");
                }
            }).Produces(StatusCodes.Status400BadRequest)
                          .Produces(StatusCodes.Status200OK)
                          .WithName("Login")
                          .WithTags("Autenticacao");
        }
    }
}
