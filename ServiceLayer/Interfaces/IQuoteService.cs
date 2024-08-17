using ServiceLayer.DTOs;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IQuoteService
    {
        QuoteDTO GetQuote(int id);
        IEnumerable<QuoteDTO> GetQuotes();
        void AddQuote(QuoteDTO quoteDTO);
        void EditQuote(int id, QuoteDTO quoteDTO);
        void RemoveQuote(int id);
        int GetNextAvailableID();
    }
}