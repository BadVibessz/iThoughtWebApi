using BLL.Abstractions;
using BLL.DTO;
using DAL.Abstractions;
using DAL.Repositories;

namespace BLL.Services;

public class NoteService : INoteService
{
    private readonly INoteRepository _noteRepository;

    public NoteService(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public List<NoteDTO> GetAllNotes()
        => _noteRepository.GetAllNotes().Select(n => new NoteDTO(n)).ToList();


    public NoteDTO? Get(int id)
        => new(_noteRepository.Get(id));

    public void Create(int diaryId, string text)
        => _noteRepository.Create(diaryId, text);

    public bool Update(int noteId, string newText)
        => _noteRepository.Update(noteId, newText);

    public bool Delete(int noteId)
        => _noteRepository.Delete(noteId);
}