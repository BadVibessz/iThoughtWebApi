using BLL.Abstractions;
using BLL.DTO;
using DAL;
using DAL.Abstractions;

namespace BLL.Services;

public class DiaryService : IDiaryService
{
    private readonly IDiaryRepository _diaryRepository;

    public DiaryService(IDiaryRepository diaryRepository)
    {
        _diaryRepository = diaryRepository;
    }

    public List<DiaryDTO> GetAllDiaries()
        => _diaryRepository.GetAllDiaries().Select(d => new DiaryDTO(d)).ToList();

    public DiaryDTO? Get(int id)
        => new(_diaryRepository.Get(id));

    public void Create(int userId, string name, string? desc = null, string? pass = null)
        => _diaryRepository.Create(userId, name, desc, pass);

    public bool Update(int id, string? newName, string? newDesc = null, string? newPass = null)
        => _diaryRepository.Update(id, newName, newDesc, newPass);

    public bool Delete(int id)
        => _diaryRepository.Delete(id);

    public void AddNote(int diaryId, NoteDTO note)
    {
        // var diaryDto = Get(diaryId);
        // var diary = new Diary(diaryDto.Name, diaryDto.Description, diaryDto.Password) { Id = diaryId }; // todo: possible null

        _diaryRepository.AddNote(diaryId, new Note(note.Text));
    }
    
    public bool UpdateNote(int diaryId, int noteId, string newText)
        => _diaryRepository.UpdateNote(diaryId, noteId, newText);

    public bool DeleteNote(int diaryId, int noteId)
        => _diaryRepository.DeleteNote(diaryId, noteId);
}