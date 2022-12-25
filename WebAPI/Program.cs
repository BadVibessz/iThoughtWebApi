using BLL.Abstractions;
using BLL.Services;
using DAL.Abstractions;
using DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// mine
builder.Services.AddMemoryCache();

builder.Services.AddScoped<IDiaryRepository, DiaryRepository>();
builder.Services.AddScoped<INoteRepository, NoteRepository>();

builder.Services.AddScoped<IDiaryService, DiaryService>();
builder.Services.AddScoped<INoteService, NoteService>();

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