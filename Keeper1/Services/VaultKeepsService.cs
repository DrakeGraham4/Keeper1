using System;
using Keeper1.Models;
using Keeper1.Repositories;

namespace Keeper1.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _vkRepo;
        private readonly VaultsRepository _vRepo;

        public VaultKeepsService(VaultKeepsRepository vkRepo, VaultsRepository vRepo)
        {
            _vkRepo = vkRepo;
            _vRepo = vRepo;
        }

        internal VaultKeep GetVaultKeepById(int id)
        {
            VaultKeep found = _vkRepo.GetVaultKeepById(id);
            if (found == null)
            {
                throw new Exception("No Vaultkeep by that Id");
            }
            return found;
        }

        internal VaultKeep Create(VaultKeep vaultKeepData, Account userInfo)
        {
            Vault found = _vRepo.GetById(vaultKeepData.VaultId);
            if (found.CreatorId != userInfo.Id)
            {
                throw new Exception("Hey guy, create your own Keep in your own vault");
            }
            return _vkRepo.Create(vaultKeepData);

        }

        internal void Remove(int vaultKeepId, string userId)
        {
            VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
            if (vaultKeep.CreatorId != userId)
            {
                throw new Exception("That is not yours to delete");
            }
            _vkRepo.Remove(vaultKeepId);
        }


    }
}