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

    [Table("data.BankAccountHistory")]
    public partial class BankAccountHistory
    {
        [Key]
        [Column(Order = 0)]
        public int BankAccountHistoryId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BankAccountId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BankAccountTypeId { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string OwnerFullName { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "money")]
        public decimal Balance { get; set; }

        [Key]
        [Column(Order = 5)]
        public decimal AnnualPercentageRate { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(400)]
        public string ExecutedByName { get; set; }

        [Key]
        [Column(Order = 7)]
        public DateTime ExecutedDatetime { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(50)]
        public string TransactionTypeName { get; set; }
    }
}
