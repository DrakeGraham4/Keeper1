using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keeper1.Models;

namespace Keeper1.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Vault Create(Vault vaultData)
        {
            string sql = @"
            INSERT INTO vaults
            (name, description, isPrivate, creatorId)
            VALUES
            (@Name, @Description, @IsPrivate, @CreatorId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, vaultData);
            vaultData.Id = id;
            return vaultData;

        }

        internal Vault GetById(int id)
        {
            string sql = @"
            SELECT
            v.*,
            a.*
            FROM vaults v
            JOIN accounts a ON v.CreatorId = a.id
            WHERE v.id = @id;
            ";
            return _db.Query<Vault, Account, Vault>(sql, (v, a) =>
            {
                v.Creator = a;
                return v;
            }, new { id }).FirstOrDefault();
        }

        internal List<Vault> GetMyVaults(string creatorId)
        {
            string sql = @"
            SELECT
            a.*,
            v.*
            FROM
            vaults v
            JOIN accounts a ON v.creatorId = a.id
            WHERE v.creatorId = @CreatorId
            ";
            return _db.Query<Account, Vault, Vault>(sql, (a, v) =>
            {
                v.Creator = a;
                return v;
            }, new { creatorId }).ToList();
        }

        internal void Update(Vault original)
        {
            string sql = @"
            UPDATE vaults
            SET
            name = @Name,
            description = @Description
            WHERE id = @Id;
            ";
            _db.Execute(sql, original);
        }
        internal void Remove(int id)
        {
            string sql = @"
            DELETE FROM vaults
            WHERE id = @id LIMIT 1;
            ";
            _db.Execute(sql, new { id });
        }

        internal List<Vault> GetProfileVaults(string id)
        {
            string sql = @"
            SELECT * FROM vaults 
            WHERE vaults.creatorId = @id
            ";
            return _db.Query<Vault>(sql, new { id }).ToList();
        }
    }
}