using DAL.Abstractions;
using Microsoft.Extensions.Caching.Memory;

namespace DAL.Repositories;

public class DiaryRepository : IDiaryRepository
{
    private readonly IMemoryCache _memoryCache;

    public DiaryRepository(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public List<Diary> GetAllDiaries()
    {
        _memoryCache.TryGetValue("diaries", out List<Diary>? diaries);
        return diaries ?? new List<Diary>();
    }

    public Diary? Get(int id) =>
        GetAllDiaries()?.Find(d => d.Id == id);


    public void Create(string name, string? desc = null, string? pass = null)
    {
        var diaries = GetAllDiaries();

        var diary = new Diary(name, desc, pass);

        // todo: remove while using database
        diary.Id = diaries.Count + 1;

        diaries.Add(diary);
        _memoryCache.Set("diaries", diaries);
    }

    public bool Update(int id, string? newName, string? newDesc = null, string? newPass = null)
    {
        var diaries = GetAllDiaries();
        var diary = diaries?.Find(d => d.Id == id);
        bool isUpdated = false;

        if (diary is not null)
        {
            if (newName is not null)
            {
                diary.Name = newName;
                isUpdated = true;
            }

            if (newDesc is not null)
            {
                diary.Description = newDesc;
                isUpdated = true;
            }

            if (newPass is not null)
            {
                diary.Password = newPass;
                isUpdated = true;
            }

            _memoryCache.Set("diaries", diaries);
        }

        return isUpdated;
    }

    public bool Delete(int id)
    {
        var diaries = GetAllDiaries();
        var isSuccess = diaries?.Remove(diaries.Find(d => d.Id == id)) ?? false;

        _memoryCache.Set("diaries", diaries);
        return isSuccess;
    }

    public void AddNote(int diaryId, Note note)
    {
        var diaries = GetAllDiaries();
        var diary = diaries?.Find(d => d.Id == diaryId);

        if (diary is not null)
            diary.AddNote(note);

        _memoryCache.Set("diaries", diaries);
    }

    public bool UpdateNote(int diaryId, int noteId, string newText)
    {
        var diaries = GetAllDiaries();
        var diary = diaries?.Find(d => d.Id == diaryId);
        if (diary is null) return false;

        bool isUpdated = diary.UpdateNote(noteId, newText);
        _memoryCache.Set("diaries", diaries);

        return isUpdated;
    }

    public bool DeleteNote(int diaryId, int noteId)
    {
        var diaries = GetAllDiaries();
        var diary = diaries?.Find(d => d.Id == diaryId);
        if (diary is null) return false;

        bool isDeleted = diary.DeleteNote(noteId);
        _memoryCache.Set("diaries", diaries);

        return isDeleted;
    }
}