using BankLoan.Models.Contracts;
using BankLoan.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;
namespace BankLoan.Repositories
{
    public class BankRepository : IRepository<IBank>
    {
        private readonly List<IBank> banks;
        public BankRepository()
        {
            banks = new List<IBank>();
        }
        public IReadOnlyCollection<IBank> Models => banks.AsReadOnly();
        public void AddModel(IBank model)
            => banks.Add(model);
        public IBank FirstModel(string name)
            => banks.FirstOrDefault(t => t.GetType().Name == name);
        public bool RemoveModel(IBank model)
        {
            if(banks.Contains(model))
            {
                banks.Remove(model);
                return true;
            }
            return false;
        }
    }
}
