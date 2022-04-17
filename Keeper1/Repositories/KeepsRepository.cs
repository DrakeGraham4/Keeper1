using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keeper1.Models;

namespace Keeper1.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Keep> GetAll()
        {
            string sql = @"
            SELECT
             k.*,
             a.*
            FROM keeps k
            JOIN accounts a ON k.creatorId = a.id;
            ";
            return _db.Query<Keep, Account, Keep>(sql, (k, a) =>
            {
                k.Creator = a;
                return k;
            }).ToList();
        }

        internal Keep GetById(int id)
        {
            string sql = @"
            SELECT
            k.*,
            a.*
            FROM keeps k
            JOIN accounts a ON k.creatorId = a.id
            WHERE k.id = @id;
            ";
            return _db.Query<Keep, Account, Keep>(sql, (k, a) =>
            {
                k.Creator = a;
                return k;
            }, new { id }).FirstOrDefault();
        }


        internal Keep Create(Keep keepData)
        {
            string sql = @"
            INSERT INTO keeps
            (name, description, img, views, kept, creatorId)
            VALUES
            (@Name, @Description, @Img, @Views, @Kept, @CreatorId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, keepData);
            keepData.Id = id;
            return keepData;
        }


        internal void Update(Keep original)
        {
            string sql = @"
            UPDATE keeps
            SET
            name = @Name,
            description = @Description,
            img = @Img,
            views = @Views,
            kept = @Kept
            WHERE id = @Id
            ";
            _db.Execute(sql, original);
        }

        internal List<Keep> GetProfileKeeps(string id)
        {
            string sql = @"
            SELECT 
            a.*,
            k.*
            FROM keeps k
            JOIN accounts a on k.creatorId = a.id
            WHERE k.creatorId = @id;
            ";
            return _db.Query<Account, Keep, Keep>(sql, (a, k) =>
            {
                k.Creator = a;
                return k;
            }, new { id }).ToList();
        }

        internal List<VKViewModel> GetKeepsByVaultId(int keepId)
        {
            string sql = @"
            SELECT
            a.*,
            k.*,
            vk.id AS VaultKeepId
            FROM vaultKeeps vk
            JOIN keeps k ON k.id = vk.keepId
            JOIN accounts a ON a.id = k.creatorId
            WHERE vk.keepId = @keepId;
            ";
            return _db.Query<Account, VKViewModel, VKViewModel>(sql, (a, vkvm) =>
            {
                vkvm.Creator = a;
                return vkvm;

            }, new { keepId }).ToList();
        }

        internal void Remove(int id)
        {
            string sql = @"
            DELETE
            FROM keeps
            WHERE id = @id LIMIT 1;
            ";
            _db.Execute(sql, new { id });
        }

    }
}