using System;
using System.Data;
using Dapper;
using Keeper1.Models;

namespace Keeper1.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal VaultKeep Create(VaultKeep vaultKeepData)
        {
            string sql = @"
            INSERT INTO vaultKeeps
            (keepId, vaultId, creatorId)
            VALUES
            (@KeepId, @VaultId, @CreatorId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, vaultKeepData);
            vaultKeepData.Id = id;
            return vaultKeepData;
        }

        internal void Remove(int id, int keepId)
        {
            string sql = @"
            DELETE FROM vaultKeeps
            WHERE id = @id LIMIT 1;
            ";
            int rowsAffected = _db.Execute(sql, new { id, keepId });
            if (rowsAffected < 0)
            {
                throw new Exception("Invalid Id");
            }
        }

        internal VaultKeep GetVaultKeepById(int id)
        {
            string sql = @"
            SELECT * FROM vaultKeeps
            WHERE id = @id;
            ";
            return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
        }
    }
}