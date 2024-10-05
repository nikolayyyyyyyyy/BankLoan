using BankLoan.Models.Contracts;
namespace BankLoan.Models
{
    public abstract class Loan : ILoan
    {
        private readonly int interesrRate;
        private readonly double amount;
        protected Loan(int interesrRate, double amount)
        {
            this.interesrRate = interesrRate;
            this.amount = amount;
        }
        public int InterestRate => interesrRate;
        public double Amount => amount;
    }
}
