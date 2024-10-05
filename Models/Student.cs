namespace BankLoan.Models
{
    public class Student : Client
    {
        private const int InitialInterestStudent = 2;
        public Student(string name, string id, double income)
            : base(name, id, InitialInterestStudent, income)
        {
        }
        public override void IncreaseInterest()
        {
            this.interest++;
        }
    }
}
