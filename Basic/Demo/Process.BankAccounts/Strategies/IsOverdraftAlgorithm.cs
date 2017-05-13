//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Process.BankAccounts
{
    /// <summary>
    /// An concrete for calculation algorithm
    /// </summary>
    public class IsOverdraftAlgorithm : IAccountAlgorithm
    {
        public IAlgorithmResult Calculate(decimal balance)
        {
            IAlgorithmResult result = new AlgorithmResult();

            // -----
            // NOTE: Obviously, in the real world, this example wouldn't warrant an Strategy.  This is for "semantic" demostration ONLY !!!
            // -----

            result.Result = (balance < 0.0m);
            result.Amount = balance;

            return result;
        }
    }
}
