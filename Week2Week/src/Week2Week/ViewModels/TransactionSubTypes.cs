using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Week2Week.Models;
using Week2Week.Data;

namespace Week2Week.Models.TransactionViewModels
{
    //Create TransactionTypesViewModel that inherits from BaseViewModel 
    public class TransactionTypesViewModel
    {
        public IEnumerable<Transaction> Transactions { get; set; }
        public IEnumerable<TransactionType> TransactionTypes { get; set; }

        public IEnumerable<TransactionSubType> TransactionSubTypes { get; set; }

        //Create a custom constructor that accepts BangazonContext as an argument and passes that context (session with db) up to the methods on BaseViewModel
        public TransactionTypesViewModel(ApplicationDbContext ctx) { }
    }
}
