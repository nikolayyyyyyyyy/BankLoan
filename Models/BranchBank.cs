﻿namespace BankLoan.Models
{
    public class BranchBank : Bank
    {
        private const int BranchBankCapacity = 25;
        public BranchBank(string name)
            : base(name, BranchBankCapacity)
        {
        }
    }
}
