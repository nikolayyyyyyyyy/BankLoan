using BankLoan.Models.Contracts;
using BankLoan.Utilities.Messages;
using System;
namespace BankLoan.Models
{
    public abstract class Client : IClient
    {
        private string name;
        private string id;
        protected int interest;
        private double income;

        protected Client(string name, string id, int interest, double income)
        {
            Name = name;
            Id = id;
            this.interest = interest;
            Income = income;
        }
        public string Name
        {
            get => name;
            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.ClientNameNullOrWhitespace);
                }
                name = value;
            }
        }
        public string Id
        {
            get => id;
            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.ClientIdNullOrWhitespace);
                }
                id = value;
            }
        }
        public int Interest
        {
            get => interest;
            protected set
            {
                interest = value;
            }
        }
        public double Income
        {
            get => income;
            private set
            {
                if(value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.ClientIncomeBelowZero);
                }
                income = value;
            }
        }
        public abstract void IncreaseInterest();
    }
}
