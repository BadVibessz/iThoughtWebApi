using DAL.Abstractions;
using Microsoft.Extensions.Caching.Memory;

namespace DAL.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly IMemoryCache _memoryCache;
    private readonly IDiaryRepository _diaryRepository;

    public NoteRepository(IDiaryRepository diaryRepository, IMemoryCache memoryCache)
    {
        _diaryRepository = diaryRepository;
        _memoryCache = memoryCache;
    }

    public List<Note> GetAllNotes()
    {
        _memoryCache.TryGetValue("notes", out List<Note>? notes);

        return notes ?? new List<Note>();
    }

    public Note? Get(int id) => GetAllNotes()?.Find(n => n.Id == id);

    public void Create(int diaryId, string text) // todo: test
    {
        var diaries = _diaryRepository.GetAllDiaries();

        var diary = diaries?.Find(d => d.Id == diaryId);
        if (diary is null) return;

        var notes = GetAllNotes();
        var note = new Note(text);
        
        _diaryRepository.AddNote(diaryId, note);

        // todo: remove while using database
        note.Id = notes.Count + 1;

        notes.Add(note);
        _memoryCache.Set("notes", notes);
    }

    public bool Update(int diaryId, int noteId, string newText) // todo: test
    {
        var notes = GetAllNotes();
        var note = notes?.Find(n => n.Id == noteId);
        if (note is null) return false;

        bool isUpdated = _diaryRepository.UpdateNote(diaryId, noteId, newText);
        _memoryCache.Set("notes", notes);

        return isUpdated;
    }

    public bool Delete(int diaryId, int noteId) // todo: test
    {
        bool isSuccess1 = _diaryRepository.DeleteNote(diaryId, noteId);

        var notes = GetAllNotes();
        var isSuccess2 = notes?.Remove(notes.Find(n => n.Id == noteId)) ?? false;
        _memoryCache.Set("notes", notes);

        return isSuccess1 && isSuccess2;
    }
}