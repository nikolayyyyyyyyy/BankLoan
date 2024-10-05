using BankLoan.Core.Contracts;
using BankLoan.Models;
using BankLoan.Models.Contracts;
using BankLoan.Repositories;
using BankLoan.Repositories.Contracts;
using BankLoan.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace BankLoan.Core
{
    public class Controller : IController
    {
        private IRepository<ILoan> loans;
        private IRepository<IBank> banks;
        public Controller()
        {
            loans = new LoanRepository();
            banks = new BankRepository();
        }
        public IReadOnlyCollection<ILoan> Loans => (IReadOnlyCollection<ILoan>)loans;
        public IReadOnlyCollection<IBank> Banks => (IReadOnlyCollection<IBank>)banks;
        public string AddBank(string bankTypeName, string name)
        {
            if (bankTypeName != nameof(CentralBank) && bankTypeName != nameof(BranchBank))
            {
                throw new ArgumentException(ExceptionMessages.BankTypeInvalid);
            }
            IBank bank;
            if (bankTypeName == nameof(CentralBank))
            {
                bank = new CentralBank(name);
            }
            else
            {
                bank = new BranchBank(name);
            }
            banks.AddModel(bank);
            return string.Format(OutputMessages.BankSuccessfullyAdded, bankTypeName);
        }
        public string AddLoan(string loanTypeName)
        {
            if (loanTypeName != nameof(StudentLoan) && loanTypeName != nameof(MortgageLoan))
            {
                throw new ArgumentException(ExceptionMessages.LoanTypeInvalid);
            }
            ILoan loan;
            if (loanTypeName == nameof(StudentLoan))
            {
                loan = new StudentLoan();
            }
            else
            {
                loan = new MortgageLoan();
            }
            loans.AddModel(loan);
            return string.Format(OutputMessages.LoanSuccessfullyAdded, loanTypeName);
        }
        public string ReturnLoan(string bankName, string loanTypeName)
        {
            ILoan loan = loans.FirstModel(loanTypeName);
            if (loan is null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.MissingLoanFromType, loanTypeName));
            }
            IBank bank = banks.Models.FirstOrDefault(t=>t.Name == bankName);
            bank.AddLoan(loan);
            loans.RemoveModel(loan);
            return string.Format(OutputMessages.LoanReturnedSuccessfully, loanTypeName, bankName);
        }
        public string AddClient(string bankName, string clientTypeName, string clientName, string id, double income)
        {
            if (clientTypeName is not nameof(Student) && clientTypeName is not nameof(Adult))
            {
                throw new ArgumentException(ExceptionMessages.ClientTypeInvalid);
            }
            IBank bank = banks.Models.FirstOrDefault(t=>t.Name == bankName);
            IClient client;
            if (clientTypeName is nameof(Student))
            {
                if (bank.GetType().Name is not nameof(BranchBank))
                {
                    return OutputMessages.UnsuitableBank;
                }
                client = new Student(clientName, id, income);
            }
            else
            {
                if (bank.GetType().Name is not nameof(CentralBank))
                {
                    return OutputMessages.UnsuitableBank;
                }
                client = new Adult(clientName, id, income);
            }
            bank.AddClient(client);
            return string.Format(OutputMessages.ClientAddedSuccessfully, clientTypeName, bankName);
        }
        public string FinalCalculation(string bankName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            IBank bank = banks.Models.FirstOrDefault(t=>t.Name == bankName);
            double sum = bank.Clients.Sum(s => s.Income) + bank.Loans.Sum(l => l.Amount);
            string formated = sum.ToString("0.00");
            stringBuilder.AppendLine($"The funds of bank {bankName} are {formated}.");
            return stringBuilder.ToString().TrimEnd();
        }
        public string Statistics()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach(var bank in banks.Models)
            {
                stringBuilder.AppendLine(bank.GetStatistics());
            }
            return stringBuilder.ToString().TrimEnd();
        }
    }
}
