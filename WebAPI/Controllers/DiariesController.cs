using BLL.Abstractions;
using BLL.DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/diaries")]
[ApiController]
public class DiariesController
{
    private readonly IDiaryService _diaryService;

    public DiariesController(IDiaryService diaryService)
    {
        _diaryService = diaryService;
    }

    // TODO: выводить Id дневника в апишке так же выводить список записок 
    
    [HttpGet("get-all")]
    public IActionResult GetAll()
    {
        List<DiaryDTO> diaries;
        try
        {
            diaries = _diaryService.GetAllDiaries();
            //diaries.Add(new DiaryDTO(){Id = 1,Name = "Diary",Description = "Description",Password = "Lomkinjub"});
        }
        catch
        {
            diaries = new List<DiaryDTO>();
        }

        return new OkObjectResult(diaries);
    }

    [HttpGet("get-{id}")]
    public IActionResult Get(int id)
    {
        DiaryDTO? diary = null;
        try
        {
            diary = _diaryService.Get(id);
        }
        catch
        {
            return new NotFoundResult();
        }

        return new OkObjectResult(diary);
    }

    [HttpPost("create")]
    public IActionResult Create(string name, string? desc = null, string? pass = null)
    {
        try
        {
            _diaryService.Create(name, desc, pass);
        }
        catch
        {
            return new BadRequestResult();
        }

        return new OkResult();
    }

    [HttpPut("update-{id}")]
    public IActionResult Update(int id, string? newName, string? newDesc = null, string? newPass = null)
    {
        try
        {
            _diaryService.Update(id, newName, newDesc, newPass);
        }
        catch
        {
            // todo: в репозитории выбрасывать правильные исключения в зависимости от ситуации
            // и обрабатыввать тут каждый экспешн, например, если реально в бд нет такого дневника,
            // то выдавать NotFoundResult, иначе BadRequestResult

            return new NotFoundResult();
        }

        return new OkResult();
    }

    [HttpDelete("delete-{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            _diaryService.Delete(id);
        }
        catch
        {
            return new NotFoundResult();
        }

        return new OkResult();
    }

    [HttpPost("add-note-{diaryId}")]
    public IActionResult AddNote(int diaryId, string text)
    {
        try
        {
            _diaryService.AddNote(diaryId, new NoteDTO { Text = text, DiaryId = diaryId });
        }
        catch
        {
            return new BadRequestResult();
        }

        return new OkResult();
    }
    
    [HttpPut("update-note-{noteId}-in-diary-{diaryId}")]
    public IActionResult UpdateNote(int diaryId, int noteId, string newText)
    {
        try
        {
            _diaryService.UpdateNote(diaryId,noteId,newText);
        }
        catch
        {
            return new BadRequestResult();
        }

        return new OkResult();
    }
    
    [HttpDelete("delete-note-{noteId}-from-diary-{diaryId}")]
    public IActionResult DeleteNote(int diaryId, int noteId)
    {
        try
        {
            _diaryService.DeleteNote(diaryId,noteId);
        }
        catch
        {
            return new NotFoundResult();
        }

        return new OkResult();
    }
}