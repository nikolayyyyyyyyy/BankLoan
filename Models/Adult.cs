namespace BankLoan.Models
{
    public class Adult : Client
    {
        private const int InitialInterestAdult = 4;
        public Adult(string name, string id, double income)
            : base(name, id, InitialInterestAdult, income)
        {
        }
        public override void IncreaseInterest()
        {
            this.interest += 2;
        }
    }
}
