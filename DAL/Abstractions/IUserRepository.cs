namespace DAL.Abstractions;

public interface IUserRepository
{
    List<User> GetAllUsers();
    User? Get(int id);
    void Create(string username, string password, string email);
    bool Update(int id, string? newName, string? newPassword, string? newEmail);
    bool Delete(int id);
}