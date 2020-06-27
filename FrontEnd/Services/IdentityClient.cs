﻿using FrontEnd.Data;
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

        private readonly IdentityDBContext _context;
        protected readonly IApiClientService _apiClient;

        public IdentityClient(IdentityDBContext context, IApiClientService apiClient)
        {
            _context = context;
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<UserAgenda>> GetUserAgendaAsync(string UserID)
        {
            var task = await _context.UserAgenda.Where(u => u.UserId.Equals(UserID, StringComparison.InvariantCultureIgnoreCase)).AsNoTracking().ToListAsync();

            if (task == null || !task.Any()) return null;

            List<UserAgenda> list = new List<UserAgenda>();
            foreach (var item in task)
                if (_apiClient.GetConferenceAsync(item.ConferenceId).Result != null && _apiClient.GetSessionAsync(item.SessionId).Result != null && _apiClient.GetTalkAsync(item.TalkId).Result != null)
                    list.Add(item);
                else
                    await DeleteUserAgenda(item.UserId, item.ConferenceId, item.SessionId, item.TalkId);

            if (list == null || !list.Any()) return null;

            return list;
        }

        public async Task<IEnumerable<UserOwnership>> GetUserOwnershipAsync(string UserID)
        {
            var task = await _context.UserOwnership.Where(u => u.UserId.Equals(UserID, StringComparison.InvariantCultureIgnoreCase)).AsNoTracking().ToListAsync();

            if (task == null || !task.Any()) return null;

            List<UserOwnership> list = new List<UserOwnership>();
            foreach (var item in task)
                if (_apiClient.GetConferenceAsync(item.ConferenceId).Result != null)
                    list.Add(item);
                else
                   await DeleteUserOwnership(item.UserId, item.ConferenceId);

            if (list == null || !list.Any()) return null;

            return list;
        }



        public async Task<UserAgenda> AddUserAgendaAsync(UserAgenda userAgenda)
        {
            var exists = GetUserAgendaAsync(userAgenda).Result;
            if (exists != null) return exists;

            _context.UserAgenda.Add(userAgenda);
            await _context.SaveChangesAsync();

            return GetUserAgendaAsync(userAgenda).Result;
        }


        public async Task<UserOwnership> AddUserOwnershipAsync(UserOwnership userOwnership)
        {
            var exists = GetUserOwnershipAsync(userOwnership).Result;
            if (exists != null) return exists;

            _context.UserOwnership.Add(userOwnership);
            await _context.SaveChangesAsync();

            return GetUserOwnershipAsync(userOwnership).Result;
        }

        public async Task<UserAgenda> DeleteUserAgenda(string userId, int conferenceId, int sessionId, int talkId)
        {
            var task = await _context.UserAgenda.FindAsync(userId, conferenceId, sessionId, talkId);

            if (task == null)
            {
                return null;
            }

            _context.UserAgenda.Remove(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<UserOwnership> DeleteUserOwnership(string userId, int conferenceId)
        {
            var task = await _context.UserOwnership.FindAsync(userId, conferenceId);
            if (task == null)
            {
                return null;
            }

            _context.UserOwnership.Remove(task);
            await _context.SaveChangesAsync();

            return task;
        }
        public async Task<UserAgenda> GetUserAgendaAsync(UserAgenda userAgenda)
        {
            var task = await _context.UserAgenda.FindAsync(userAgenda.UserId, userAgenda.ConferenceId, userAgenda.SessionId, userAgenda.TalkId);

            if (task == null)
                return null;

            return task;
        }

        public async Task<UserOwnership> GetUserOwnershipAsync(UserOwnership userOwnership)
        {
            var task = await _context.UserOwnership.FindAsync(userOwnership.UserId, userOwnership.ConferenceId);

            if (task == null)
                return null;

            return task;
        }
    }
}
