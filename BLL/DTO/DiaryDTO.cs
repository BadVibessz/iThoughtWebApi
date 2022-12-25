using DAL;

namespace BLL.DTO;

public class DiaryDTO
{
    //public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Password { get; set; }
    public DateTime DateOfCreation { get; } = DateTime.Now;

    public DiaryDTO()
    {
        
    }
    public DiaryDTO(Diary diary)
    {
       // Id = diary.Id;
        Name = diary.Name;
        Description = diary.Description;
        Password = diary.Password;
        DateOfCreation = diary.DateOfCreation;
    }
}