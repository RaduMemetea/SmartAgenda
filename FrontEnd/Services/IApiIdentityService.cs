using FrontEnd.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Services
{
    public interface IApiIdentityService
    {
        Task<UserAgenda> GetUserAgendaAsync(UserAgenda userAgenda);
        Task<IEnumerable<UserOwnership>> GetUserOwnershipAsync(string UserID);
        
        Task<IEnumerable<UserAgenda>> GetUserAgendaAsync(string UserID);
        Task<UserOwnership> GetUserOwnershipAsync(UserOwnership userOwnership);

        Task<UserAgenda> AddUserAgendaAsync(UserAgenda userAgenda);
        Task<UserOwnership> AddUserOwnershipAsync(UserOwnership userOwnership);
        
        Task<UserAgenda> DeleteUserAgenda(string userId, int conferenceId, int sessionId, int talkId);
        Task<UserOwnership> DeleteUserOwnership(string userId, int conferenceId);
    }
}
