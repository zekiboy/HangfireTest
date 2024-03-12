﻿
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using HangfireApp.Jobs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//projenin çalışması için docker aktif olmalı
var hangfireConnectionString = "Server=localhost;Database=HangfireTest;User Id=SA;Password=reallyStrongPwd123;";

//var hangfireConnectionString = "Server=localhost; Database=HangfireTest; User Id=SA; Password=reallyStrongPwd123";

builder.Services.AddHangfire(x =>
{
    x.UseSqlServerStorage(hangfireConnectionString);

    //bu kısımda hata almamak için system.data.sqlClient veya microsoft.....SqlClient nugetleri şart
    //crontab.cronhub.io üzerinden süre belirle
    // RecurringJob.AddOrUpdate<Job>(j => j.DbControl(), "30 17  * * *");
    RecurringJob.AddOrUpdate<Job>(j => j.DbControl(), "* * * * *");
    //DbControl yerine direkt sendEmail metodunu kullan

});

builder.Services.AddHangfireServer();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseHangfireDashboard();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

