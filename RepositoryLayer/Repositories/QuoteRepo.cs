using DomainLayer.DomainModels;
using DomainLayer.Interfaces;
using RepositoryLayer.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryLayer.Repositories
{
    public class QuoteRepo : GenericRepo<Quotes>, IQuoteRepo
    {
        public QuoteRepo(SystemContext context) : base(context)
        {
        }

        public IEnumerable<int> GetAllIDs()
        {
            var systemContext = _context as SystemContext;

            return systemContext.Quotes.Select(q => q.QuoteID).ToList();
        }

        public Quotes GetByQuoteID(int id)
        {
            var systemContext = _context as SystemContext;
            return systemContext.Quotes.FirstOrDefault(entity => entity.QuoteID == id);
        }
    }
}