using DapperTransactionWithUoW.Data.Repositories;
using DapperTransactionWithUoW.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace DapperTransactionWithUoW.Data
{
    public class UnitOfWork : CockpitContext, IUnitOfWork
    {
        private IDbConnection connection;
        private IDbTransaction transaction;
        private IRepository userRepository;
        private IConfiguration configuration { get; }

        public UnitOfWork(IConfiguration configuration, IDbTransaction transaction=null) : base(configuration,transaction)
        {
            this.connection = base.Connection; ;
        }

        public IRepository UserRepository => this.userRepository ?? (this.userRepository = new UserRepository(configuration,transaction));

        public void Commit()
        {
            try
            {
                this.transaction.Commit();
            }
            catch
            {
                this.transaction.Rollback();
                throw;
            }
            finally
            {
                this.transaction.Dispose();
                this.transaction = connection.BeginTransaction();
                this.resetRepositories();
                this.Dispose();
            }
        }

        public void BeginTransaction()
        {
            this.connection.Open();
            this.transaction = connection.BeginTransaction();
        }

        private void resetRepositories()
        {
            userRepository = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (transaction != null)
                    {
                        transaction.Dispose();
                        transaction = null;
                    }
                    if (connection != null)
                    {
                        connection.Dispose();
                        connection = null;
                    }
                }
                disposed = true;
            }
        }



        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}
