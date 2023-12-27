using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddSession();

builder.Services.AddCors(options =>
{
    //options.AddPolicy("CorsPolicy", builder =>
    //{
    //    builder.AllowAnyOrigin()
    //        .AllowAnyHeader()
    //        .AllowAnyMethod()
    //      .AllowCredentials();
    //});
    options.AddPolicy("AllowHeaders", builder =>
    {
        builder.AllowAnyOrigin()
                .WithHeaders(HeaderNames.ContentType, HeaderNames.Server, HeaderNames.AccessControlAllowHeaders, HeaderNames.AccessControlExposeHeaders, "x-custom-header", "x-path", "x-record-in-use", HeaderNames.ContentDisposition);
    });
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseHsts();
}

app.UseCors("CorsPolicy");

app.UseSession();

app.UseHttpsRedirection();

app.MapControllers();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Token}/{action=Index}/{id?}");

app.Run();
