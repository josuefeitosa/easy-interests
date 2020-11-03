using EasyInterests.API.Enums;

namespace EasyInterests.API.Application.DTOs
{
  public class UpdateUserDTO
  {
    public UpdateUserDTO(string name, string email, UserRolesEnum role, string phoneNumber)
    {
      this.Name = name;
      this.Email = email;
      this.Role = role;
      this.PhoneNumber = phoneNumber;

    }
    public string Name { get; set; }
    public string Email { get; set; }
    public UserRolesEnum Role { get; set; }
    public string PhoneNumber { get; set; }
  }
}
