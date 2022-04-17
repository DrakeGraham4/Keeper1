using System.Data;
using Dapper;
using Keeper1.Models;

namespace Keeper1.Repositories
{
    public class ProfilesRepository
    {
        private readonly IDbConnection _db;

        public ProfilesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Profile GetUsersProfile(string id)
        {
            string sql = @"
            SELECT * FROM accounts
            WHERE id = @id;
            ";
            return _db.QueryFirstOrDefault<Profile>(sql, new { id });
        }
    }
}