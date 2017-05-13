//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Process.BankAccounts
{
    /// <summary>
    /// An interface for calculation results
    /// </summary>
    public interface IAlgorithmResult
    {
        bool Result { get; set; }

        decimal Amount { get; set; }
    }
}
