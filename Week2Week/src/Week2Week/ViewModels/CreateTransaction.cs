using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Week2Week.Models;
using Week2Week.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;


namespace Week2Week.Models.TransactionViewModels
{
    public class CreateTransaction
    {
        public Transaction Transaction { get; set; }
        public List<SelectListItem> TransactionTypeId { get; set; }
        public List<SelectListItem> TransactionSubTypeId { get; set; }
        public CreateTransaction(ApplicationDbContext ctx)
        {

            this.TransactionTypeId = ctx.TransactionType
                                    .OrderBy(l => l.Type)
                                    .AsEnumerable()
                                    .Select(li => new SelectListItem
                                    {
                                        Text = li.Type,
                                        Value = li.TransactionTypeId.ToString()
                                    }).ToList();

            this.TransactionTypeId.Insert(0, new SelectListItem
            {
                Text = "Choose category...",
                Value = "0"
            });
        }
    }
}