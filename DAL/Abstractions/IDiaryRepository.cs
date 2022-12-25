namespace DAL.Abstractions;

public interface IDiaryRepository // todo: пока что реализовать через IMemoryCache, потом заменить на бд
{
    List<Diary> GetAllDiaries();
    Diary? Get(int id);
    void Create(string name, string? desc = null, string? pass = null);
    bool Update(int id, string? newName, string? newDesc = null, string? newPass = null);
    bool Delete(int id);

    void AddNote(int diaryId, Note note);
    bool UpdateNote(int diaryId, int noteId, string newText);
    bool DeleteNote(int diaryId, int noteId);
}