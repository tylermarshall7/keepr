using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

///Getters
        internal IEnumerable<Keep> Get()
        {
            string sql = "SELECT * FROM keeps WHERE isPrivate = 0;";
            return _db.Query<Keep>(sql);
        }

        internal IEnumerable<Keep> GetByUserId(string userId)
        {
            string sql = "SELECT * FROM keeps WHERE userId = @userId";
            return _db.Query<Keep>(sql, new { userId });
        }

        internal Keep GetById(int id)
        {
            string sql = "SELECT * FROM keeps WHERE id = @Id";
      return _db.QueryFirstOrDefault<Keep>(sql, new { id });
        }

///Create, Edit, Delete
        internal Keep Create(Keep keepData)
        {
        string sql = @"
            INSERT INTO keeps 
            (userId, name, description, img, isPrivate, views, shares, keeps)
            VALUES
            (@userId, @name, @description, @img, @isPrivate, @views, @shares, @keeps);
            SELECT LAST_INSERT_ID()";
      keepData.Id = _db.ExecuteScalar<int>(sql, keepData);
      return keepData;
        }

        internal bool Edit(Keep updatedKeep)
        {
            string sql = @"
            UPDATE keeps
            SET
            name = @name,
            description = @description,
            img = @img
            WHERE id = @id AND userId = @userId LIMIT 1;";
            int rowsAffected = _db.Execute(sql, updatedKeep);
            return rowsAffected == 1;
        }

        internal bool Delete(string userId, int id)
        {
            string sql = @"
            DELETE FROM keeps WHERE id = @id AND userId = @userId LIMIT 1";
            int rowsAffected = _db.Execute(sql, new { userId, id} );
            return rowsAffected == 1;
        }



    }
}