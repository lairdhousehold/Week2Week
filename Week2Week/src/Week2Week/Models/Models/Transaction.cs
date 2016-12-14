using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Week2Week.Models;


namespace Week2Week.Models
{
    public class Transaction
    {
        [Key]
        public int TranscactionId { get; set; }

        [Required]
        [StringLength(55)]
        [DisplayAttribute(Name = "Name")]
        public string Title { get; set; }
        [Required]
        public double Amount { get; set; }

        [Required]
        public DateTime SelectedDate { get; set; }
        [Required]
        public virtual ApplicationUser User { get; set; }
        [Required]
        [DisplayAttribute(Name = "Type")]

        public int TransactionTypeId { get; set; }
        public TransactionType TransactionType { get; set; }
        [Required]
        [DisplayAttribute(Name = "SubType")]
        public int TransactionSubTypeId { get; set; }
        public TransactionSubType TransactionSubType { get; set; }

        public bool IsReoccurring { get; set; }

    }
}
