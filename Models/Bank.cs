using BankLoan.Models.Contracts;
using BankLoan.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace BankLoan.Models
{
    public abstract class Bank : IBank
    {
        private string name;
        private int capacity;
        private readonly List<ILoan> loans;
        private readonly List<IClient> clients;
        public Bank(string name, int capacity)
        {
            this.name = name;
            this.capacity = capacity;
            this.loans = new List<ILoan>();
            this.clients = new List<IClient>();
        }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.BankNameNullOrWhiteSpace);
                }
            }
        }
        public int Capacity => capacity;
        public IReadOnlyCollection<ILoan> Loans => loans.AsReadOnly();
        public IReadOnlyCollection<IClient> Clients => clients.AsReadOnly();
        public void AddClient(IClient Client)
        {
            if (clients.Count == this.capacity)
            {
                throw new ArgumentException(ExceptionMessages.NotEnoughCapacity);
            }
            clients.Add(Client);
        }
        public void AddLoan(ILoan loan)
            => loans.Add(loan);
        public string GetStatistics()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Name: {name}, Type: {this.GetType().Name}");
            if(clients.Count == 0)
            {
                stringBuilder.AppendLine("Clients: none");
            }
            else
            {
                stringBuilder.AppendLine($"Clients: {string.Join(", ",clients.Select(p=>p.Name))}");
            }
            stringBuilder.AppendLine($"Loans: {loans.Count}, Sum of Rates: {this.SumRates()}");
            return stringBuilder.ToString().TrimEnd();
        }
        public void RemoveClient(IClient Client)
            => clients.Remove(Client);
        public double SumRates()
            => loans.Sum(l => l.InterestRate);

    }
}
