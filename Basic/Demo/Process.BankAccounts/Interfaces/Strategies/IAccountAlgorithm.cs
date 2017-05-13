//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Process.BankAccounts
{
    /// <summary>
    /// An interface for calculations
    /// </summary>
    public interface IAccountAlgorithm
    {
        IAlgorithmResult Calculate(decimal balance);
    }
}
