using BLL.DTO;

namespace BLL.Abstractions;

public interface INoteService
{
    List<NoteDTO> GetAllNotes();
    NoteDTO? Get(int id);
    void Create(int diaryId, string text);
    bool Update(int noteId, string newText);
    bool Delete(int noteId);
}