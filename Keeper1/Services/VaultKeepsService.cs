using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        internal VaultKeep Create(VaultKeep vaultKeepData)
        {
            return _vkRepo.Create(vaultKeepData);
        }

        internal void Remove(int id, Account userinfo)
        {
            VaultKeep vaultKeep = GetVaultKeepById(id);
            if (vaultKeep.CreatorId != userinfo.Id)
            {
                throw new Exception("That is not yours to delete");
            }
            _vkRepo.Remove(id, vaultKeep.KeepId);
        }


    }
}