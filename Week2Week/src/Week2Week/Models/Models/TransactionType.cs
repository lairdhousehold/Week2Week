using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Week2Week.Models
{
    public class TransactionType
    {
        [Key]
        public int TransactionTypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string Label { get; set; }
        public ICollection<Transaction> Transactions;
    }
}
