using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _repo;
        public VaultsService(VaultsRepository repo)
        {
            _repo = repo;
        }
///Getters
        public IEnumerable<Vault> Get(string userId)
        {
            return _repo.GetByUserId(userId);
        }
        public Vault GetById(string userId, int id)
        {
            Vault foundVault = _repo.GetById(userId, id);
            if (foundVault == null)
            {
                throw new Exception("not a valid vault id");
            }
            return foundVault;
        }
///Create, Edit, Delete
        public Vault Create(Vault newVault)
        {
          return _repo.Create(newVault);
        }
        public Vault Edit(Vault updatedVault, string userId)
        {
            Vault foundVault = GetById(updatedVault.UserId, updatedVault.Id);
            bool updated = _repo.Edit(updatedVault);
            if (!updated)
            {
                throw new Exception("Oops ALL BERRIES You are not the owner of this vault");
            }
            return updatedVault;
        }

        public string Delete(string userId, int id)
        {
            bool deleted = _repo.Delete(userId, id);
            if (!deleted)
            {
                throw new Exception("Oops ALL BERRIES You are not the owner of this vault");
            }
            return "Vault has been deleted";
        }
    }
}