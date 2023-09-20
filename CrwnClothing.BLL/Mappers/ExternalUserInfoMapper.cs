using CrwnClothing.BLL.DTOs;
using CrwnClothing.BLL.External.Contracts;
using CrwnClothing.BLL.Utils;

namespace CrwnClothing.BLL.Mappers
{
    public static class ExternalUserInfoMapper
    {
        public static ExternalUserInfoDTO ToDTO(this FacebokUserInfo facebokUserInfo) => new()
        {
            ExternalId = facebokUserInfo.Id,
            Username = facebokUserInfo.Name.FullNameToUserName(),
            Email = facebokUserInfo.Email,
            Picture = facebokUserInfo.Picture.Data.Url,
            Firstname = facebokUserInfo.FirstName,
            Lastname = facebokUserInfo.LastName,
        };

        public static ExternalUserInfoDTO ToDTO(this GoogleUserInfo googleUserInfo) => new()
        {
            ExternalId = googleUserInfo.Id,
            Username = googleUserInfo.Name.FullNameToUserName(),
            Email = googleUserInfo.Email,
            Firstname = googleUserInfo.GivenName,
            Lastname = googleUserInfo.FamilyName,
            Picture = googleUserInfo.GooglePicture,
        };
    }
}
