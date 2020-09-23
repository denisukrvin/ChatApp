using ChatApp.Service.Models.Profile;
using ChatApp.Service.Models.Common.Api;

namespace ChatApp.Service.Interfaces
{
    public interface IProfileService
    {
        ProfileModel Get(int userId);
        OperationResponse Edit(int userId, ProfileModel model);
    }
}