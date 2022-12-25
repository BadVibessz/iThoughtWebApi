namespace DAL.Abstractions;

public interface INoteRepository
{
    List<Note> GetAllNotes();
    Note? Get(int id);
    void Create(int diaryId, string text);
    bool Update(int diaryId, int noteId, string newText);
    bool Delete(int diaryId, int noteId);
}