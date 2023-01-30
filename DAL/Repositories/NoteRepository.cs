using DAL.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace DAL.Repositories;

public class NoteRepository : INoteRepository
    // TODO: много повторяющегося функционала, разбить репозитории на два непересекающихся функционала
{
    private readonly IDiaryRepository _diaryRepository;
    private readonly DatabaseContext _db;
    public NoteRepository(IDiaryRepository diaryRepository, DatabaseContext db)
    {
        _diaryRepository = diaryRepository;
        // _memoryCache = memoryCache;
        _db = db;
    }

    public List<Note> GetAllNotes() => _db.Notes.Include(n => n.Diary).ToList();

    public Note? Get(int id) => GetAllNotes().Find(n => n.Id == id);

    public void Create(int diaryId, string text)
        => _diaryRepository.AddNote(diaryId, new Note(text));

    public bool Update(int id, string newText)
    {
        var note = Get(id);
        if (note is not null)
        {
            note.Text = newText;

            _db.Entry(note).State = EntityState.Modified;
            _db.SaveChanges();
        }

        return true;
    }


    public bool Delete(int id)
    {
        bool isSuccess = false;
        var note = Get(id);
        if (note is not null)
        {
            _db.Notes.Remove(note); // todo: where to check whether or not note exists in DAL or BLL?
            _db.SaveChanges();
            isSuccess = true;
        }

        return isSuccess;
    }
}