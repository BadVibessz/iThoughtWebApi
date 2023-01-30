using System.ComponentModel.DataAnnotations;
using DAL;

namespace BLL.DTO;

public class UserDTO
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; } // todo: validate
    public string DateOfCreation { get; set; }
    public List<DiaryDTO> Diaries { get; set; } = new();
    public string Salt { get; }

    public UserDTO()
    {
    }

    public UserDTO(User user)
    {
        Id = user.Id;
        Username = user.Username;
        Password = user.Password;
        Email = user.Email;
        DateOfCreation = user.DateOfCreation;
        Diaries = user.Diaries.Select(d => new DiaryDTO(d)).ToList();
        Salt = user.Salt;
    }
}