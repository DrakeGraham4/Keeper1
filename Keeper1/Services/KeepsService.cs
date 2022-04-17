using System;
using System.Collections.Generic;
using Keeper1.Models;
using Keeper1.Repositories;

namespace Keeper1.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _kRepo;

        public KeepsService(KeepsRepository kRepo)
        {
            _kRepo = kRepo;
        }

        internal List<Keep> GetAll()
        {
            return _kRepo.GetAll();
        }

        internal Keep GetById(int id)
        {
            return _kRepo.GetById(id);
        }

        internal Keep Create(Keep keepData)
        {
            return _kRepo.Create(keepData);
        }


        internal Keep Update(Keep keepData)
        {
            Keep original = GetById(keepData.Id);
            ValidateOwner(keepData.CreatorId, original);
            original.Name = keepData.Name ?? original.Name;
            original.Description = keepData.Description ?? original.Description;
            original.Img = keepData.Img ?? original.Img;
            original.Views = keepData.Views ?? original.Views;
            original.Kept = keepData.Kept ?? original.Kept;
            _kRepo.Update(original);
            return original;


        }

        private static void ValidateOwner(string userId, Keep keepData)
        {
            if (userId != keepData.CreatorId)
            {
                throw new Exception("You cannot edit a keep you did not make");
            }
        }


        internal void Remove(int id, string userId)
        {
            Keep original = GetById(id);
            ValidateOwner(userId, original);
            _kRepo.Remove(id);
        }


        internal List<Keep> GetProfileKeeps(string id)
        {
            return _kRepo.GetProfileKeeps(id);

        }

        internal List<VKViewModel> GetKeepsByVaultId(int keepId)
        {
            return _kRepo.GetKeepsByVaultId(keepId);
        }
    }
}