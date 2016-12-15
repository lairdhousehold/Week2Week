using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Week2Week.Data;


namespace Week2Week.Models.TransactionViewModels
{
   
    public class TransactionTypeViewModel
    {
        public IEnumerable<TransactionType> TransactionTypes { get; set; }
        
        public TransactionTypeViewModel(ApplicationDbContext ctx)
        { }
    }
}
