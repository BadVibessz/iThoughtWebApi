using DAL.Repositories;
using Microsoft.Extensions.Caching.Memory;


// console app for api testing

var diaryCache = new MemoryCache(new MemoryCacheOptions());
var notesCache = new MemoryCache(new MemoryCacheOptions());

var diaryRepo = new DiaryRepository(diaryCache);
var notesRepo = new NoteRepository(diaryRepo, notesCache);

diaryRepo.Create("PersonalDiary", "My personal diary about life");
diaryRepo.Create("WorkDiary", "My work diary about job");

notesRepo.Create(1, "Today was the best day ever");
notesRepo.Create(1, "Today was the worst day ever");

notesRepo.Create(2, "Today I was hired");
notesRepo.Create(2, "Today I was fired");

foreach (var diary in diaryRepo.GetAllDiaries())
{
    Console.WriteLine($"Id: {diary.Id}\nName: {diary.Name}\nDescription: {diary.Description}");

    Console.WriteLine("Notes:");
    foreach (var note in diary.GetNotes())
        Console.WriteLine($"Id: {note.Id}\nDate: {note.DateOfCreation}\nText: {note.Text}");
    
    Console.WriteLine();
}