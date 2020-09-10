using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _repo;
        public KeepsService(KeepsRepository repo)
        {
            _repo = repo;
        }

///Getters
        public IEnumerable<Keep> Get()
        {
            return _repo.Get();
        }

        public IEnumerable<Keep> Get(string userId)
        {
            return _repo.GetByUserId(userId);
        }

        public Keep GetById(int id)
        {
            Keep foundKeep = _repo.GetById(id);
            if (foundKeep == null)
            {
                throw new Exception("not a valid keep id");
            }
            return foundKeep;
        }



/// Create, Edit, Delete
        public Keep Create(Keep newKeep)
        {
            return _repo.Create(newKeep);
        }

        public Keep Edit(Keep updatedKeep)
        {
            Keep foundKeep = GetById(updatedKeep.Id);

            updatedKeep.Name = updatedKeep.Name == null 
            ?
            foundKeep.Name : updatedKeep.Name;

            updatedKeep.Description = updatedKeep.Description == null 
            ?
            foundKeep.Description : updatedKeep.Description;

            updatedKeep.Img = updatedKeep.Img == null 
            ?
            foundKeep.Img : updatedKeep.Img;

            bool updated = _repo.Edit(updatedKeep);
            if (!updated)
            {
                throw new Exception("Oops ALL BERRIES You are not the owner of this keep");
            }
            return updatedKeep;
        }

        public string Delete(string userId, int id)
        {
            GetById(id);
            bool deleted = _repo.Delete(userId, id);
            if (!deleted)
            {
                throw new Exception("Oops ALL BERRIES You are not the owner of this keep");
            }
            return "Keep has been deleted!";
        }
    }
}