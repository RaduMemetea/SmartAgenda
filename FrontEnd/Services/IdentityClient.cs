using FrontEnd.Data;
using FrontEnd.Models.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Services
{
    public class IdentityClient : IApiIdentityService
    {
        private readonly DbContextOptions<IdentityDBContext> _dbOptions;

        public IdentityClient(DbContextOptions<IdentityDBContext> dbContextOptions)
        {
            _dbOptions = dbContextOptions;
        }

        public async Task<IEnumerable<UserAgenda>> GetUserAgendaAsync(string UserID)
        {
            using (var _context = new IdentityDBContext(_dbOptions))
            {
                var task = await _context.UserAgenda.Where(u => u.UserId.Equals(UserID, StringComparison.InvariantCultureIgnoreCase)).AsNoTracking().ToListAsync();

                if (task == null || !task.Any()) return null;

                return task;
            }
        }

        public async Task<IEnumerable<UserOwnership>> GetUserOwnershipAsync(string UserID)
        {
            using (var _context = new IdentityDBContext(_dbOptions))
            {
                var task = await _context.UserOwnership.Where(u => u.UserId.Equals(UserID, StringComparison.InvariantCultureIgnoreCase)).AsNoTracking().ToListAsync();
                if (task == null || !task.Any()) return null;
                return task;
            }
        }



        public async Task<UserAgenda> AddUserAgendaAsync(UserAgenda userAgenda)
        {
            using (var _context = new IdentityDBContext(_dbOptions))
            {
                var exists = GetUserAgendaAsync(userAgenda).Result;
                if (exists != null) return exists;

                _context.UserAgenda.Add(userAgenda);
                await _context.SaveChangesAsync();

                return GetUserAgendaAsync(userAgenda).Result;
            }
        }

        public async Task<UserOwnership> AddUserOwnershipAsync(UserOwnership userOwnership)
        {
            using (var _context = new IdentityDBContext(_dbOptions))
            {
                var exists = GetUserOwnershipAsync(userOwnership).Result;
                if (exists != null) return exists;

                _context.UserOwnership.Add(userOwnership);
                await _context.SaveChangesAsync();

                return GetUserOwnershipAsync(userOwnership).Result;
            }
        }

        public async Task<UserAgenda> DeleteUserAgenda(string userId, int conferenceId, int sessionId, int talkId)
        {
            using (var _context = new IdentityDBContext(_dbOptions))
            {
                var task = await _context.UserAgenda.FindAsync(userId, conferenceId, sessionId, talkId);

                if (task == null) return null;

                _context.UserAgenda.Remove(task);
                await _context.SaveChangesAsync();

                return task;
            }
        }

        public async Task<UserOwnership> DeleteUserOwnership(string userId, int conferenceId)
        {
            using (var _context = new IdentityDBContext(_dbOptions))
            {
                var task = await _context.UserOwnership.FindAsync(userId, conferenceId);
                if (task == null) return null;

                _context.UserOwnership.Remove(task);
                await _context.SaveChangesAsync();

                return task;
            }
        }

        public async Task<UserAgenda> GetUserAgendaAsync(UserAgenda userAgenda)
        {
            using (var _context = new IdentityDBContext(_dbOptions))
            {
                var task = await _context.UserAgenda.FindAsync(userAgenda.UserId, userAgenda.ConferenceId, userAgenda.SessionId, userAgenda.TalkId);

                if (task == null) return null;

                return task;
            }
        }

        public async Task<UserOwnership> GetUserOwnershipAsync(UserOwnership userOwnership)
        {
            using (var _context = new IdentityDBContext(_dbOptions))
            {
                var task = await _context.UserOwnership.FindAsync(userOwnership.UserId, userOwnership.ConferenceId);

                if (task == null) return null;

                return task;
            }
        }
    }
}
