namespace DAL.Abstractions;

public interface INoteRepository
{
    List<Note> GetAllNotes();
    Note? Get(int id);
    void Create(int diaryId, string text);
    bool Update(int id, string newText);
    bool Delete(int id);
}