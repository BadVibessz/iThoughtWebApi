using DAL;

namespace BLL.DTO;

public class NoteDTO
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string DateOfCreation { get; } = DateTime.Now.ToString("f");
    public int DiaryId { get; set; } = -1;

    public NoteDTO()
    {
    }

    public NoteDTO(Note note)
    {
        Id = note.Id;
        Text = note.Text;
        DateOfCreation = note.DateOfCreation;
        DiaryId = note.Diary.Id; // todo:
    }
}