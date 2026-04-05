using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{

   /* public class BankTerminal
    {
        
        public Action<int> OnMoneyWithdraw;

        public void Withdraw(int amount)
        {
           
            OnMoneyWithdraw?.Invoke(amount);
        }
    }

    class Program
    {
        static void Main()
        {
            BankTerminal terminal = new BankTerminal();

            
            terminal.OnMoneyWithdraw += amount => Console.WriteLine($"Знято {amount} грн");

           
            terminal.OnMoneyWithdraw = null; 

            
            terminal.OnMoneyWithdraw += amount => Console.WriteLine($"Знято {amount} грн");
            terminal.Withdraw(50); 
        }
    }*/
}
