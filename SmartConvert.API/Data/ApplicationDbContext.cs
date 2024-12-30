using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SmartConvert.API.Models;

namespace SmartConvert.API.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Define DbSets for your entities
        public DbSet<UserModel> Users { get; set; }

        public void ExecuteStoredProcedure(string procedureName, params SqlParameter[] parameters)
        {
            var sqlParameters = string.Join(", ", parameters.Select(p => p.ParameterName));
            Database.ExecuteSqlRaw($"EXEC {procedureName} {sqlParameters}", parameters);
        }

    }
}
