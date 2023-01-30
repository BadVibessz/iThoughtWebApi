using BLL.Abstractions;
using BLL.DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/notes")]
[ApiController]
public class NotesController
{
    private readonly INoteService _noteService;

    public NotesController(INoteService noteService)
    {
        _noteService = noteService;
    }
    
    [HttpGet("get-all")]
    public IActionResult GetAll()
    {
        List<NoteDTO> notes;
        try
        {
            notes = _noteService.GetAllNotes();
        }
        catch
        {
            notes = new List<NoteDTO>();
        }

        return new OkObjectResult(notes);
    }
    
    [HttpGet("get-{id}")]
    public IActionResult Get(int id)
    {
        NoteDTO? note = null;
        try
        {
            note = _noteService.Get(id);
        }
        catch
        {
            return new NotFoundResult();
        }

        return new OkObjectResult(note);
    }
    
    [HttpPost("add-to-diary-{diaryId}")]
    public IActionResult Create(int diaryId, string text)
    {
        try
        {
            _noteService.Create(diaryId, text);
        }
        catch
        {
            return new BadRequestResult();
        }

        return new OkResult();
    }
    
    [HttpPut("update-{noteId}")]
    public IActionResult Update(int noteId, string newText)
    {
        try
        {
            _noteService.Update(noteId,newText);
        }
        catch
        {
            return new BadRequestResult();
        }

        return new OkResult();
    }
    
    [HttpDelete("delete-{noteId}")]
    public IActionResult Delete(int noteId)
    {
        try
        {
            _noteService.Delete(noteId);
        }
        catch
        {
            return new BadRequestResult();
        }

        return new OkResult();
    }
}