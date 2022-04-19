using System;
using System.Collections.Generic;
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

        internal Vault GetById(int id, Account userInfo)
        {
            Vault found = _vRepo.GetById(id);
            if (found.CreatorId != userInfo?.Id && found.IsPrivate)
            {
                throw new Exception("This Vault is private");
            }
            if (found == null)
            {
                throw new Exception("This Vault does not exist .com");
            }
            return found;
        }
        internal Vault Create(Vault vaultData)
        {
            return _vRepo.Create(vaultData);
        }


        internal Vault Update(Vault vaultData, Account userInfo)
        {
            Vault original = GetById(vaultData.Id, userInfo);
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
            Vault deleted = _vRepo.GetById(id);
            ValidateOwner(userId, deleted);
            _vRepo.Remove(id);
        }

        private static void ValidateOwner(string userId, Vault vaultData)
        {
            if (userId != vaultData.CreatorId)
            {
                throw new Exception("You cannot edit/delete a vault you did not make");
            }
        }

        internal List<Vault> GetProfileVaults(string id, string userId)
        {
            List<Vault> v = _vRepo.GetProfileVaults(id);
            if (id != userId)
            {
                return v.FindAll(v => v.IsPrivate == false);
            }
            return v;


        }

        internal object GetKeepsByVaultId(int id, Account userInfo)
        {
            Vault vault = GetById(id, userInfo);
            return _vRepo.GetKeepsByVaultId(id);
        }
    }
}