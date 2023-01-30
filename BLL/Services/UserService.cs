using BLL.Abstractions;
using BLL.DTO;
using DAL.Abstractions;

namespace BLL.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public List<UserDTO> GetAllUsers()
        => _userRepository.GetAllUsers().Select(u => new UserDTO(u)).ToList();

    public UserDTO? Get(int id)
        => new(_userRepository.Get(id));

    public void Create(string username, string password, string email)
        => _userRepository.Create(username, password, email);

    public bool Update(int id, string? newName, string? newPassword, string? newEmail)
        => _userRepository.Update(id, newName, newPassword, newEmail);

    public bool Delete(int id)
        => _userRepository.Delete(id);
}