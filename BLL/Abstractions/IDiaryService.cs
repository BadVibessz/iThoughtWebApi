using BLL.DTO;

namespace BLL.Abstractions;

public interface IDiaryService
{
    List<DiaryDTO> GetAllDiaries();
    DiaryDTO? Get(int id);
    void Create(string name, string? desc = null, string? pass = null);
    bool Update(int id, string? newName, string? newDesc = null, string? newPass = null);
    bool Delete(int id);

    void AddNote(int diaryId, NoteDTO note);
    bool UpdateNote(int diaryId, int noteId, string newText);
    bool DeleteNote(int diaryId, int noteId);
}