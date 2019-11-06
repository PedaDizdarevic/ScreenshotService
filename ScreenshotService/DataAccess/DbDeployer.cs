using DbUp;
using System.Reflection;

namespace DataAccess
{
    public class DbDeployer
    {
        private readonly string _connectionString;

        public DbDeployer(string connectionStringArg)
        {
            _connectionString = connectionStringArg;
        }

        public bool Deploy()
        {
            var upgradeEngine = DeployChanges
                .To.SqlDatabase(_connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .Build();

            var upgrade = upgradeEngine.PerformUpgrade();

            return upgrade.Successful;
        }
    }
}