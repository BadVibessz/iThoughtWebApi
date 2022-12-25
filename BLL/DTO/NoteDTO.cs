using DAL;

namespace BLL.DTO;

public class NoteDTO
{
   // public int Id { get; set; }
    public string Text { get; set; }
    public DateTime DateOfCreation { get; } = DateTime.Now;
    public int DiaryId { get; set; } = -1;

    public NoteDTO()
    {
        
    }
    public NoteDTO(Note note)
    {
       // Id = note.Id;
        Text = note.Text;
        DateOfCreation = note.DateOfCreation;
        DiaryId = note.Diary.Id; // todo:
    }
}