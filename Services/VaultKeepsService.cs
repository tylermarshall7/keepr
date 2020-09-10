using System;
using System.Collections;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    public VaultKeepsService(VaultKeepsRepository repo)
    {
      _repo = repo;
    }

///Getters
    public IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(int vaultId, string userId)
    {
      return _repo.GetKeepsByVaultId(vaultId, userId);
    }

    public VaultKeep GetById(string userId, int id)
    {
      VaultKeep found = _repo.GetById(id, userId);
      if (found == null)
      {
        throw new Exception("not a valid id");
      }
      return found;
    }

///Create, Edit, Delete
    public VaultKeep Create(VaultKeep newVaultKeep)
    {
      return _repo.Create(newVaultKeep);
    }
 
    public VaultKeep Delete(string userId, int id)
    {
      VaultKeep found = GetById(userId, id);
      if (found.UserId != userId)
      {
        throw new Exception("this is not yours!");
      }
      if (_repo.Delete(id, userId))
      {
        return found;
      }
      throw new Exception("something bad happened");
    }


  }
}