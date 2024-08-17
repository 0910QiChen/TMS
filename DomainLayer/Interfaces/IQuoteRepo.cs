using DomainLayer.DomainModels;
using System.Collections.Generic;

namespace DomainLayer.Interfaces
{
    public interface IQuoteRepo : IGenericRepo<Quotes>
    {
        IEnumerable<int> GetAllIDs();
        Quotes GetByQuoteID(int id);
    }
}