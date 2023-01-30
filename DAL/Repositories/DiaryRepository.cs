using DAL.Abstractions;
using DAL.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace DAL.Repositories;

public class DiaryRepository : IDiaryRepository
{
    private readonly DatabaseContext _db;

    public DiaryRepository(DatabaseContext db)
    {
        _db = db;

        //todo: remove (for debugging)
        // Create("Personal Diary", "Private diary about my life", "12345678");
        // Create("Work Diary", "Public diary about my work");
        // Create("Abstract thoughts Diary", "Diary about my one-time thoughts");
        //
        // AddNote(1, new Note("Today I fucked Clara"));
        // AddNote(1, new Note("Today Clara cheated on me"));
        //
        // AddNote(2, new Note("Today I was hired"));
        // AddNote(2, new Note("Today I was fired"));
        //
        // AddNote(3, new Note("Why I always wanna sleep?"));
    }

    public List<Diary> GetAllDiaries() // todo: learn about lazy loading
        => _db.Diaries
            .Include(d => d.Notes)
            .Include(d => d.User)
            .ToList();


    public Diary? Get(int id) =>
        GetAllDiaries()?.Find(d => d.Id == id);


    public void Create(int userId, string name, string? desc = null, string? pass = null)
    {
        var user = _db.Users.FirstOrDefault(u => u.Id == userId);

        // todo: where to check whether or not user exists in DAL or BLL?
        if (user is null) return; // todo: throw an exception?

        if (pass is not null)
            pass = Enctyption.Sha256HashSumOf(pass);

        var diary = new Diary(user, name, desc, pass);

        _db.Diaries.Add(diary);
        _db.SaveChanges();
    }

    public bool Update(int id, string? newName = null, string? newDesc = null, string? newPass = null)
    {
        var diary = Get(id);
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
                diary.Password = Enctyption.Sha256HashSumOf(newPass);
                isUpdated = true;
            }

            if (isUpdated)
            {
                _db.Entry(diary).State = EntityState.Modified;
                _db.SaveChanges();
            }
        }

        return isUpdated;
    }

    public bool Delete(int id)
    {
        bool isSuccess = false;

        var diary = Get(id);
        if (diary is not null)
        {
            _db.Diaries.Remove(diary); // todo: where to check whether or not diary exists in DAL or BLL?
            _db.SaveChanges();
            isSuccess = true;
        }

        return isSuccess;
    }

    public void AddNote(int diaryId, Note note)
    {
        var diary = Get(diaryId);
        if (diary is not null)
        {
            diary.AddNote(note);
            _db.SaveChanges();
        }
    }

    public bool UpdateNote(int diaryId, int noteId, string newText)
    {
        // todo:
        var diary = Get(diaryId);
        if (diary is null) return false;

        bool isUpdated = diary.UpdateNote(noteId, newText);

        if (isUpdated)
        {
            _db.Entry(diary).State = EntityState.Modified;
            _db.SaveChanges();
        }

        return isUpdated;
    }

    public bool DeleteNote(int diaryId, int noteId)
    {
        var diary = Get(diaryId);
        if (diary is null) return false;

        bool isDeleted = diary.DeleteNote(noteId);

        if (isDeleted)
            _db.SaveChanges();

        return isDeleted;
    }
}