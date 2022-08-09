using CrwnClothing.BLL.DTOs;
using CrwnClothing.DAL.Entities;

namespace CrwnClothing.BLL.Mappers
{
    public static class UserMapper
    {
        public static UserDTO ToDTO(this User user) => new()
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            LastLoginAt = user.LastLoginAt,
            Verified = user.Verified
        };

        public static User ToEntity(this UserDTO user) => new()
        {
            Id = user.Id == null ? 0 : (int)user.Id,
            Username = user.Username,
            Email = user.Email,
            Password = user.Password,
            CreatedAt = user.CreatedAt == null ? DateTime.Now : (DateTime)user.CreatedAt,
            LastLoginAt = user.LastLoginAt,
            Verified = user.Verified ?? null
        };

    }
}
