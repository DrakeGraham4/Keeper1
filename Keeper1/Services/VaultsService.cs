using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keeper1.Models;
using Keeper1.Repositories;

namespace Keeper1.Services
{
    public class VaultsService
    {

        private readonly VaultsRepository _vRepo;

        public VaultsService(VaultsRepository vRepo)
        {
            _vRepo = vRepo;
        }

        internal Vault GetById(int id)
        {
            return _vRepo.GetById(id);
        }
        internal Vault Create(Vault vaultData)
        {
            return _vRepo.Create(vaultData);
        }


        internal Vault Update(Vault vaultData)
        {
            Vault original = GetById(vaultData.Id);
            ValidateOwner(vaultData.CreatorId, original);
            original.Name = vaultData.Name ?? original.Name;
            original.Description = vaultData.Description ?? original.Description;
            _vRepo.Update(original);
            return original;


        }

        internal List<Vault> GetMyVaults(Account userInfo)
        {
            List<Vault> vaults = _vRepo.GetMyVaults(userInfo.Id);
            return vaults;
        }

        internal void Remove(int id, string userId)
        {
            Vault original = GetById(id);
            ValidateOwner(userId, original);
            _vRepo.Remove(id);
        }

        private static void ValidateOwner(string userId, Vault vaultData)
        {
            if (userId != vaultData.CreatorId)
            {
                throw new Exception("You cannot edit a vault you did not make");
            }
        }

        internal List<Vault> GetProfileVaults(string id, string userId)
        {
            List<Vault> vault = _vRepo.GetProfileVaults(id);
            if (id != userId)
            {
                return vault.FindAll(v => v.IsPrivate == false);
            }
            return vault;


        }
    }
}