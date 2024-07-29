using System;

namespace DomainLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepo UserRepo { get; }
        IQuoteRepo QuoteRepo { get; }
        void commit();
    }
}