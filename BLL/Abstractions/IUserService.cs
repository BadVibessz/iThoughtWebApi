using BLL.DTO;

namespace BLL.Abstractions;

public interface IUserService
{
    List<UserDTO> GetAllUsers();
    UserDTO? Get(int id);
    void Create(string username, string password, string email);
    bool Update(int id, string? newName, string? newPassword, string? newEmail);
    bool Delete(int id);
}