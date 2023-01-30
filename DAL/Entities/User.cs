using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using DAL.Utility;

namespace DAL;

public class User
{
    public int Id { get; set; }
    [MinLength(3)] [MaxLength(256)] public string Username { get; set; }
    public string Password { get; set; } // todo: hash
    public string Email { get; set; }
    public string DateOfCreation { get; set; } // todo: store as DateTime?
    public List<Diary> Diaries { get; set; } = new();
    public string Salt { get; set; }

    public User()
    {
    }

    public User(string username, string password, string email)
    {
        Username = username;
        Password = password;
        Email = email;
        DateOfCreation = DateTime.Now.ToString("f");

        Salt = Username + DateOfCreation + Enctyption.LocalSalt;
    }

    public User(User other)
    {
        Username = other.Username;
        Password = other.Password;
        Email = other.Email;
        Diaries = new(other.Diaries);
        DateOfCreation = DateTime.Now.ToString("f");
    }
}