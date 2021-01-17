using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Syncfusion.Pages.Week
{
    public interface IEntryAccess
    {
        Task<List<Category>> GetCategories();
        Task<List<Entry>> GetEntries(DateTime start, DateTime end);
    }
}