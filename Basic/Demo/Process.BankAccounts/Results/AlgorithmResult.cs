//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Process.BankAccounts
{
    /// <summary>
    /// A concrete calculation result
    /// </summary>
    public class AlgorithmResult : IAlgorithmResult
    {
        public bool Result { get; set; }
        public decimal Amount { get; set; }
    }
}
