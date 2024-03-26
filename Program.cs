using System;

class Program
{

    static void Main(string[] args)
    {
        bool continueDisplay = true;
        BankAccount account = new SavingsAccount("Elijah Onuh", "023718356", 100000, 0.05m);

        Console.WriteLine("welcome to vale finance");
        while (continueDisplay)
        {
            account.PerformTransaction();
            continueDisplay = account.Continue();
        }


    }
    private class BankAccount
    {
        public string accountName { get; private set; }
        public string accountNumber { get; private set; }
        public decimal balance;

        public BankAccount(string AccountName, string AccountNumber, decimal Balance)
        {
            accountName = AccountName;
            accountNumber = AccountNumber;
            balance = Balance;
        }

        // create a virtual method for deposit which can be overriden
        public virtual void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("invalid amount. Try again");
                return;
            }

            else {
                balance += amount;
            }
        }

        // overloaded method with different parameters
        public void Deposit(double amount)
        {
            Deposit((decimal)amount);
        }

        public virtual void Withdraw(decimal amount)
        {
            if (amount < 0)
            {
                Console.WriteLine("invalid input. Try again");
                return;
            }
            else if (amount > balance)
            {
                Console.WriteLine("insufficient funds");
            }
            else
            {
                balance -= amount;
            }
        }

        // overloaded method
        public void Withdraw(double amount)
        {
            Withdraw((decimal)amount);
        }

        public virtual decimal DisplayBalance(decimal Balance)
        {
            return Balance;
        }

        public void PerformTransaction()
        {
            Console.WriteLine("what transaction would you love to perform today?");
            Console.WriteLine("1) Deposit funds");
            Console.WriteLine(  "2) Withdraw funds");
            Console.WriteLine("3) Check Balance");
            Console.WriteLine(  "4) Exit");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice) || choice != 1 && choice != 2 && choice != 3 && choice != 4)
            {
                Console.WriteLine("invalid input. Try again");
            }

            switch (choice)
            {
                case 1:
                    Console.WriteLine("enter amount to deposit");
                    if (decimal.TryParse(Console.ReadLine(), out decimal amountToDeposit))
                    {
                        if(amountToDeposit <= 0) {
                            Console.WriteLine("invalid input");
                            return;
                        }

                        Deposit(amountToDeposit);
                        Console.WriteLine($"you deposited {amountToDeposit:C}, your new balance is {balance:C}");
                    }

                    else
                    {
                        Console.WriteLine("invalid amount. Deposit aborted");

                    }
                    break;

                case 2:
                    Console.WriteLine("enter amount to withdraw");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal amountToWithdraw))
                    {
                        Console.WriteLine("invalid input. try again");
                    }
                    else
                    {
                        Withdraw(amountToWithdraw);
                        Console.WriteLine($"You withdrew {amountToWithdraw:C}, your account balance is {balance:C}");

                    }
                    break;

                case 3:
                    DisplayBalance(balance);
                    Console.WriteLine($"your account balance is {balance:C}");
                    break;


                case 4:
                    Console.WriteLine("are you sure you want to exit?");
                    Console.WriteLine("press 1 to perform another transaction");
                    Console.WriteLine("press 2 to confirm exit");
                    int exitChoice = int.Parse(Console.ReadLine());
                    if (exitChoice == 1)
                    {
                        Console.WriteLine("please wait, while you are redirected to perform another transaction...");
                        PerformTransaction();
                    }
                    else if (exitChoice == 2)
                    {
                        Console.WriteLine("Thank you for banking with us, goodbye...");
                        Environment.Exit(0);
                        break;
                    }

                    else if (choice != 1 && choice != 2)
                    {
                        Console.WriteLine("invalid selection");
                    }
                    break;

                default:
                    break;



            }
        }

        public bool Continue()
        {
            Console.WriteLine("do you want to perform another transaction?");
            Console.WriteLine("please enter 1 for yes and 2 to exit");
            int ContinueOrExit = int.Parse(Console.ReadLine());

            if (ContinueOrExit == 1)
            {
                Console.WriteLine("please wait while your request is processing...");
                return true;
            }

            else if (ContinueOrExit == 2)
            {
                Console.WriteLine("Thank you for banking with us");
                return false;
            }
            else
            {
                Console.WriteLine("invalid command");
                return true;
            }
        }

    }

    private class SavingsAccount : BankAccount
    {
        private decimal InterestRate;
        public SavingsAccount(string accountName, string accountNumber, decimal balance, decimal interestRate) : base(accountName, accountNumber, balance)
        {
            InterestRate = interestRate;
        }

        public override void Deposit(decimal amount)
        {
            base.Deposit(amount);
            balance += balance * InterestRate;
        }

        public override void Withdraw(decimal amount)
        {
            base.Withdraw(amount);
            balance -= balance * InterestRate;
        }

    }

}


/*

// Declare a class and it's variables, while showcasing encapsulation
public class Banks
{
    public string accName;
    public string accNo;
    public decimal bal;

    // now, we use a constructor to initialize the declared variables and assign temporary values

    public Banks(string AccName, string AccNo, decimal Bal)
    {
        this.accName = AccName;
        this.accNo = AccNo;
        this.bal = Bal;
    }

    // create a method for deposit while making it virtual so, it can be overriden
    public virtual void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("insufficient funds");
        }
        else
        {
            bal += amount;
        }
    }

    // create a method for withdrawal and set it to virtual, so, it can be overriden
    public virtual void Withdraw(decimal amount)
    {
        if (amount > bal)
        {
            Console.WriteLine("insufficient funds");
        }
        else
        {
            bal -= amount;
        }
    }

}

// create another class(child, derived or sub class) which inherits properties(constructor) of the base class
public class SavingAcc : Banks
{
    decimal intRate;
    public SavingAcc(string accName, string accNo, decimal bal, decimal IntRate) : base(accName, accNo, bal)
    {
        intRate = IntRate;
    }

    // let's override the Deposit method in our super class, which is a feature of polymorphism

    public override void Deposit(decimal amount)
    {
        base.Deposit(amount);
        bal += bal * intRate;
    }

    // override the withdrawal method in our super class(Banks)
    public override void Withdraw(decimal amount)
    {
        base.Withdraw(amount);
        bal -= bal * intRate;
    }

    // overloading the deposit method simply involves changing the parameters but keeping same method name

    public void Deposit(double amount)
    {
        Deposit((decimal)amount);
    }

    // overloading the withdraw method as well

    public void Withdraw(double amount)
    {
        Withdraw((decimal)amount);
    }
}




// now, we make the main method and create an instance of the SAVINGS class
class Program
{
    static void Main(string[] args)
    {
        Banks Account = new SavingAcc("John Okafor", "0238150407", 10000, 0.02m);
        Account.Deposit(500);
        Account.Withdraw(200);
        Console.WriteLine($"Account balance is {Account.bal}");
    }
}
*/
