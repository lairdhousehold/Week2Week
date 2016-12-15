using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Week2Week.Data;
using Week2Week.Models;

namespace Week2Week.Models.TransactionViewModels
{
    public class TransactionList
    {
        public IEnumerable<Transaction> Transactions { get; set; }
        public TransactionList(ApplicationDbContext ctx)
        {
            var context = ctx;
        }
    }
}
