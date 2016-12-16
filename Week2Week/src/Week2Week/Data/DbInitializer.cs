using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Week2Week.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Week2Week.Data
{
    //Class: DbInitializer
    //Purpose: Checks to see if the database has Transaction types and Transaction subtypes; if it doesn't, the database will be seeded.
    public class DbInitializer
    {
        //Method: The initialize method creates a scoped variable "context," which represents a session with the database. If there are any Transaction types currently in the database, then it will not be seeded.
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any Transactions types.
                if (context.TransactionType.Any())
                {
                    return;
                }

                var TransactionTypes = new TransactionType[]
                {
                  new TransactionType {
                      Label = "Payment"
                  },
                  new TransactionType {
                      Label = "Income"
                  },
                  
                  
                };

                foreach (TransactionType i in TransactionTypes)
                {
                    context.TransactionType.Add(i);
                }
                context.SaveChanges();

                var TransactionSubType = new TransactionSubType[]
                {
                    new TransactionSubType {
                      SubType = "Living Expense",
                      TransactionTypeId = 1
                    },

                    new TransactionSubType {
                      SubType = "Food",
                      TransactionTypeId = 1
                    },

                    new TransactionSubType {
                      SubType = "Transportation Expense",
                      TransactionTypeId = 1
                    },

                    new TransactionSubType {
                      SubType = "Entertainment",
                      TransactionTypeId = 1
                    },

                    new TransactionSubType {
                      SubType = "PayCheck",
                      TransactionTypeId = 2
                    },
                    new TransactionSubType {
                      SubType = "Business Income",
                      TransactionTypeId = 2
                    },
                    new TransactionSubType {
                      SubType = "Gift",
                      TransactionTypeId = 2
                    },


                };

                foreach (TransactionSubType i in TransactionSubType)
                {
                    context.TransactionSubType.Add(i);
                }
                context.SaveChanges();
            }
        }
    }
}
