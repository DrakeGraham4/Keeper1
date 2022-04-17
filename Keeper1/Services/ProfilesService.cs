using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keeper1.Models;
using Keeper1.Repositories;

namespace Keeper1.Services
{
    public class ProfilesService
    {
        private readonly ProfilesRepository _pRepo;

        public ProfilesService(ProfilesRepository pRepo)
        {
            _pRepo = pRepo;
        }

        internal Profile GetUsersProfile(string id)
        {
            Profile profile = _pRepo.GetUsersProfile(id);
            if (profile == null)
            {
                throw new Exception("Profile does not exist");
            }
            return profile;
        }
    }
}