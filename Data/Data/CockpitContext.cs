using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DapperTransactionWithUoW.Data
{
    public abstract class CockpitContext : IDisposable
    {
        private string connectionString;

        public CockpitContext(IConfiguration configuration, IDbTransaction transaction)
        {
            connectionString = configuration.GetConnectionString("TESTE");
            this.Transaction = transaction;
        }

        protected IDbTransaction Transaction { get; private set; }

        protected IDbConnection Connection
        {
            get
            {
                return Transaction?.Connection ?? new SqlConnection(connectionString); ;
            }
        }

        protected bool disposed;

        public void Dispose()
        {
            if (Transaction != null)
            {
                Transaction.Dispose();
                Transaction = null;
            }
            if (Connection != null)
            {
                Connection.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
