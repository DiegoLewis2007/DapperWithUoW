using System;

namespace DapperTransactionWithUoW.Domain.Interfaces
{
    public interface IUnitOfWork 
    {
        IRepository UserRepository { get; }

        void Commit();

        void BeginTransaction();
    }
}

