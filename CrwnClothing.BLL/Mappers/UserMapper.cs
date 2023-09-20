using CrwnClothing.BLL.DTOs;
using CrwnClothing.BLL.DTOs.UserDto;
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
            UpdatedAt = user.UpdatedAt,
            LastLoginAt = user.LastLoginAt,
            Verified = user.Verified,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PaymentId = user.PaymentId,
            PhoneNumber = user.PhoneNumber
        };

        public static User ToEntity(this UserDTO user) => new()
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            LastLoginAt = user.LastLoginAt,
            Verified = user.Verified ?? null,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PaymentId = user.PaymentId,
            PhoneNumber = user.PhoneNumber
        };

        public static User ToEntity(this CreateUserDTO createUserDTO) => new()
        {
            Username = createUserDTO.Username,
            Email = createUserDTO.Email,
            Password = createUserDTO.Password,
            FirstName = createUserDTO.FirstName,
            LastName = createUserDTO.LastName,
            PhoneNumber = createUserDTO.PhoneNumber
        };

        public static User ToEntity(this ExternalUserInfoDTO externalDTO) => new() 
        {
            Username = externalDTO.Username,
            Email = externalDTO.Email,
            Verified = true,
            FirstName = externalDTO.Firstname,
            LastName = externalDTO.Lastname
        };

    }
}
