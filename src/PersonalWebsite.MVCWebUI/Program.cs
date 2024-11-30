using PersonalWebsite.Data.DapperSQLite.ASPNetExtensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSQLiteRepositories(new SQLiteDbConfiguration
{
    PostDbConnectionString = $@"data source={AppDomain.CurrentDomain.BaseDirectory}postdb.db;",
    UserDbConnectionString = $@"data source={AppDomain.CurrentDomain.BaseDirectory}uderdb.db;"
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // TODO: Add custom exception page.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute("default", "");

app.Run();
