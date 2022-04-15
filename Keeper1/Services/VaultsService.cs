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

        internal Vault Create(Vault vaultData)
        {
            return _vRepo.Create(vaultData);
        }
    }
}