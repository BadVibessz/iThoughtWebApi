using DAL;

namespace BLL.DTO;

public class DiaryDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Password { get; set; }
    public string DateOfCreation { get; } = DateTime.Now.ToString("f");
    public List<NoteDTO> Notes { get; set; }
    public int UserId { get; set; } = -1;

    public DiaryDTO()
    {
    }

    public DiaryDTO(Diary diary)
    {
        Id = diary.Id;
        Name = diary.Name;
        Description = diary.Description;
        Password = diary.Password;
        DateOfCreation = diary.DateOfCreation;
        Notes = diary.GetNotes().Select(n => new NoteDTO(n)).ToList();
        UserId = diary.User.Id; // todo: possible null
    }
}