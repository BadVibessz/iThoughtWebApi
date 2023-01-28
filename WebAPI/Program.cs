using BLL.Abstractions;
using BLL.Services;
using DAL;
using DAL.Abstractions;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// mine
builder.Services.AddMemoryCache();

//TODO:
// Since Singletons are instantiated “only once” in the entire life cycle,
// they can contribute to overall application performance.
// Since Singletons stay forever in the application untill it is terminated,
// any object created inside the service if not disposed properly can create
// potential Memory leaks. Hence it is responsiblity of the developers
// to ensure the objects created inside Singletons are disposed properly.
// A type should be registered as a “Singleton” only when it
// is fully thread-safe and is not dependent on other services or types.

builder.Services.AddTransient<IDiaryRepository, DiaryRepository>();
builder.Services.AddTransient<INoteRepository, NoteRepository>();

builder.Services.AddTransient<IDiaryService, DiaryService>();
builder.Services.AddTransient<INoteService, NoteService>();

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    // todo: form config
    options.UseNpgsql(@"host=localhost;port=5432;database=iThought;user id=postgres;password=gunna;");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(opt => 
    opt.AllowAnyOrigin().
        AllowAnyMethod().
        AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();