using AgilSystemutveckling_Xamarin_Net5.Data;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using AgilSystemutveckling_Xamarin_Net5.TestModels;

using AgilSystemutveckling_Xamarin_Net5;


#region Test Data Shown in Console
MySqlConnection connection = new MySqlConnection(
    "Server=xamarindb.c6pefsvvniwb.eu-north-1.rds.amazonaws.com; Database=sys; UID=admin; Password=Xamarin321;"
    );

connection.Open();

string query = "Select * from Item";


//var query = AddProduct();
MySqlCommand command = new MySqlCommand(query, connection);
MySqlDataReader reader = command.ExecuteReader();

while (reader.Read())
{
    Console.WriteLine(reader["Id"]);
    Console.WriteLine(reader["Name"]);
    
}
#endregion

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
// 
builder.Services.AddDbContext<LibraryDbContext>(
    options => options.UseMySql(
        ServerVersion.AutoDetect("DefaultConnection")
        )
    );
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
