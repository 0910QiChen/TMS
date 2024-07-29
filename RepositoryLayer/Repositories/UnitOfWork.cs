using DomainLayer.Interfaces;
using RepositoryLayer.Contexts;
using System;

namespace RepositoryLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SystemContext _context;
        public IUserRepo UserRepo { get; }
        public IQuoteRepo QuoteRepo { get; }

        public UnitOfWork(SystemContext context)
        {
            _context = context;
            UserRepo = new UserRepo(_context);
            QuoteRepo = new QuoteRepo(_context);
        }

        public void commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}