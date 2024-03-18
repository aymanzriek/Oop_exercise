namespace exercise_106
{
    public class PaymentCard
    {
        private double balance;
        private const double MaxBalance = 150;

        public PaymentCard(double openingBalance)
        {
            balance = openingBalance;
        }

        public override string ToString()
        {
            return $"The card has a balance of {balance} euros";
        }

        public void EatLunch()
        {
            if (balance >= 10.60)
            {
                balance -= 10.60;
            }
        }

        public void DrinkCoffee()
        {
            if (balance >= 2.0)
            {
                balance -= 2.0;
            }
        }

        public void AddMoney(double amount)
        {
            if (amount < 0)
            {
                // If the amount is negative, do not add it to the balance.
                return;
            }

            balance += amount;
            if (balance > MaxBalance)
            {
                balance = MaxBalance;
            }
        }
    }
}
