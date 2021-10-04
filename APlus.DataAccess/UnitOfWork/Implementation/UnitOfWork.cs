using APlus.Data.Context;
using APlus.DataAccess.Repository.Implementation;
using APlus.DataAccess.Repository.Interface;
using APlus.DataAccess.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APlus.DataAccess.UnitOfWork.Implementation
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly APlusDbContext _context;
        private IGenericRepository<T> _repository;
        private IDbContextTransaction Transaction;
        private IExecutionStrategy strategy;

        public UnitOfWork(APlusDbContext dbContext)
        {
            _context = dbContext;
        }

        public IGenericRepository<T> Repository => _repository = _repository ?? new GenericRepository<T>(_context);

        public async Task RollBack()
        {
            await Transaction.RollbackAsync();
        }

        public async Task<bool> SaveAsync()
        {
            bool result = false;
            strategy = _context.Database.CreateExecutionStrategy();

            await strategy.Execute(async () =>
            {
                using (Transaction = await _context.Database.BeginTransactionAsync())
                {
                    await Transaction.CreateSavepointAsync("Aplus_Save_Point");
                    result = await _context.SaveChangesAsync() >= 0;
                    Transaction.Commit();
                }
            });

            return result;
        }
    }
}
