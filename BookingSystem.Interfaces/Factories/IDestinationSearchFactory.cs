using BookingSystem.Models.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Interfaces.Factories
{
    public interface IDestinationSearchFactory
    {
        IDestinationSearch GetDestinationSearch(SearchModel searchModel);
    }
}
