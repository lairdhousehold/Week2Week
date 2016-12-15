using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Week2Week.Data;

namespace Week2Week.Models.TransactionViewModels
{
    public class TransactionDetail
    {
        public Transaction Transaction { get; set; }
        public TransactionDetail(ApplicationDbContext ctx)
        {
            var context = ctx;
        }

    }
}
