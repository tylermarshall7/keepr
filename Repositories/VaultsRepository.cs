using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;
    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

///Getters

  public IEnumerable<Vault> GetByUserId(string userId)
  {
    string sql = "SELECT * FROM vaults WHERE userId = @userId";
    return _db.Query<Vault>(sql, new {userId} );
  }

  public Vault GetById(string userId, int id)
  {
    string sql = "SELECT * FROM Vaults WHERE id = @Id AND userId = @userId";
      return _db.QueryFirstOrDefault<Vault>(sql, new { userId, id });
  }

/// Create, Edit, Delete
  public Vault Create(Vault newVault)
  {
    string sql = @"
    INSERT INTO vaults
    (name, description, userId)
    VALUES
    (@name, @description, @userId);
    SELECT LAST_INSERT_ID()";
    newVault.Id = _db.ExecuteScalar<int>(sql, newVault);

    return newVault;
  }

  public bool Edit(Vault updatedVault)
  {
    string sql = @"
      UPDATE vaults
      SET
        name =@name,
        description = @description,
    WHERE id = @id AND userId = @userId LIMIT 1";
    int rowsAffected = _db.Execute(sql, updatedVault);
      return rowsAffected == 1;
  }

  public bool Delete(string userId, int id)
  {
    string sql = "DELETE FROM Vaults WHERE id = @Id AND userid = @UserId LIMIT 1;";
      int rowsAffected = _db.Execute(sql, new { userId, id });
      return rowsAffected == 1;
  }

  }
}