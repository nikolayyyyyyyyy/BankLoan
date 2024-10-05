using BankLoan.Core;
using BankLoan.Core.Contracts;
using System;

namespace BankLoan
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            IEngine engine = new Engine();
            engine.Run();
        }
    }
}
/*
AddBank BranchBank DSKBank
AddBank CentralBank Unicredit
AddBank CentralBank Fibank
AddLoan StudentLoan
AddLoan MortgageLoan
AddLoan MortgageLoan
ReturnLoan DSKBank StudentLoan
ReturnLoan Unicredit StudentLoan
ReturnLoan DSKBank MortgageLoan
ReturnLoan Fibank MortgageLoan
AddClient DSKBank Student Sarah 10A2AFBBAG 5421.5
AddClient DSKBank Student Tom 54AABAG75 2341.1
AddClient Fibank Adult Peter 6GSFAAZZ12 125054
FinalCalculation DSKBank
Statistics
End
*/