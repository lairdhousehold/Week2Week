using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Week2Week.Models
{
    public class TransactionSubType
    {
        [Key]
        public int TransactionSubTypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string SubType { get; set; }
        public int TransactionTypeId { get; set; }

        public TransactionType TransactionType { get; set; }

    }
}
