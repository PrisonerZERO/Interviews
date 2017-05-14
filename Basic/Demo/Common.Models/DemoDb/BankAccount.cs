//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Common.Models.DemoDb
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("dbo.BankAccount")]
    public partial class BankAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BankAccountId { get; set; }

        [Required]
        public int BankAccountTypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string OwnerFullName { get; set; }

        [Column(TypeName = "money")]
        public decimal Balance { get; set; }

        public decimal AnnualPercentageRate { get; set; }

        [Required]
        [StringLength(400)]
        public string ExecutedByName { get; set; }

        public DateTime ExecutedDatetime { get; set; }

        public virtual BankAccountType BankAccountType { get; set; }
    }
}
