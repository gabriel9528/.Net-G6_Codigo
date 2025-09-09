using DemoEFCoreRelationship.Data;
using DemoEFCoreRelationship.Repo;
using DemoEFCoreRelationship.Repo.ManyToMany;
using DemoEFCoreRelationship.Repo.OneToMany;
using DemoEFCoreRelationship.Repo.OneToOne;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<RepositoryCarCompanyModel>();
builder.Services.AddScoped<RepositoryEmployeeEmployeeAddress>();
builder.Services.AddScoped<RepositoryDoctorPattient>();
builder.Services.AddScoped<RepositoryAuthorBook>();
builder.Services.AddScoped<RepositoryStudentSubject>();
builder.Services.AddScoped<RepositoryPersonBusiness>();
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
