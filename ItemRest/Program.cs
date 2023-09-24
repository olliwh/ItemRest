using ItemRest;
using ItemRest.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Cors
string allowAll = "AllowAll";
string withOrigin = "WithOrigin";
string onlyGet = "OnlyGet";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowAll,
                              policy =>
                              {
                                  policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();

                              });
    options.AddPolicy(name: withOrigin,
                              policy =>
                              {
                                  //policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                                  policy.WithOrigins("http://zealand.dk").WithMethods("Post", "Put").SetPreflightMaxAge(TimeSpan.FromSeconds(1440)).AllowAnyHeader();

                              });
    options.AddPolicy(name: onlyGet,
                              policy =>
                              {
                                  policy.AllowAnyOrigin()
                                  .WithMethods("GET")
                                  .AllowAnyHeader();
                              });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ItemDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
bool useDB = false;
if (useDB)
{
    var optionsBuilder =
        new DbContextOptionsBuilder<ItemContext>();
    optionsBuilder.UseSqlServer(connectionString);
    ItemContext context =
        new ItemContext(optionsBuilder.Options);
    builder.Services.AddSingleton<IItemsRepository>(
        new ItemsRepositoryDB(context));
}
else
{
    builder.Services.AddSingleton<IItemsRepository>(new ItemsRepository());

}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseCors(allowAll);

app.MapControllers();

app.Run();
