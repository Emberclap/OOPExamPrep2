using NUnit.Framework;

namespace VendingRetail.Tests
{
    [TestFixture]
    public class Tests
    {
        private CoffeeMat coffeeMat;

        [SetUp]
        public void Setup()
        {
            int waterCapacity = 100;
            int buttonsCount = 5;
            coffeeMat = new CoffeeMat(waterCapacity, buttonsCount);
        }
        [Test]
        public void ConstructorShouldInitializeProperly()
        {
            Assert.That(100, Is.EqualTo(coffeeMat.WaterCapacity));
            Assert.AreEqual(5, coffeeMat.ButtonsCount);
            Assert.That(coffeeMat, Is.Not.Null);
        }
        [Test]
        public void FillWaterTankShould_ReturnStringWhenAlreadyFull()
        {
            coffeeMat.FillWaterTank();
            string expectedMessage = coffeeMat.FillWaterTank();
            string actualMessage = "Water tank is already full!";

            Assert.AreEqual(expectedMessage, actualMessage);

        }
        [Test]
        public void FillWaterTankShould_ReturnStatusStringWithAmountWatterFilled()
        {
            string expectedMessage = coffeeMat.FillWaterTank();
            string actualMessage = $"Water tank is filled with 100ml";

            Assert.AreEqual(expectedMessage, actualMessage);

        }
        [Test]
        public void AddDrinkShould_ReturnTrue_IfDrinkIsAdded()
        {
            Assert.True(coffeeMat.AddDrink("Pepsi", 5));
        }

        [Test]
        public void AddDrinkShould_ReturnFalse_IfThereAreNoMoreSlotsForDrinks()
        {

            coffeeMat.AddDrink("Pepsi1", 5);
            coffeeMat.AddDrink("Pepsi2", 5);
            coffeeMat.AddDrink("Pepsi3", 5);
            coffeeMat.AddDrink("Pepsi4", 5);
            coffeeMat.AddDrink("Pepsi5", 5);
            coffeeMat.AddDrink("Pepsi6", 5);
            Assert.False(coffeeMat.AddDrink("Pepsi7", 5));
        }
        [Test]
        public void BuyDrinkShould_ReturnErrorString_IfWaterTankLvlIsLowerThan80()
        {
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink("Pepsi", 5);
            coffeeMat.BuyDrink("Pepsi");
            string expectedMessage = "CoffeeMat is out of water!";

            Assert.AreEqual(expectedMessage, coffeeMat.BuyDrink("Pepsi"));

        }
        [TestCase("Cola")]
        public void BuyDrinkShould_ReturnString_IfDrinkIsNotAvailable(string drinkName)
        {
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink("Pepsi", 5);
            string expectedMessage = $"{drinkName} is not available!";

            Assert.AreEqual(expectedMessage, coffeeMat.BuyDrink(drinkName));
        }
        [Test]
        public void BuyDrinkShould_ReturnStatusString_WithPriceToPay()
        {
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink("Pepsi", 5);
            string expectedMessage = $"Your bill is 5,00$";

            Assert.AreEqual(expectedMessage, coffeeMat.BuyDrink("Pepsi"));
        }
        [Test]
        public void BuyDrinkShould_IncreaseIncome_WithPriceToPay()
        {
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink("Pepsi", 5);
            coffeeMat.BuyDrink("Pepsi");
            double expectedincome = 5d;

            Assert.AreEqual(expectedincome, coffeeMat.Income);
        }
        [Test]
        public void CollectIncomeShould_RestartCurrentIncomeTo0()
        {
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink("Pepsi", 15);
            coffeeMat.BuyDrink("Pepsi");
            coffeeMat.CollectIncome();
            Assert.AreEqual(0d, coffeeMat.Income);
        }
        [Test]
        public void CollectIncomeShould_ReturnCollectedIncome()
        {
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink("Pepsi", 25);
            coffeeMat.BuyDrink("Pepsi");
            double collectedIncome = coffeeMat.CollectIncome();

            Assert.AreEqual(25, collectedIncome);
        }
        [Test]
        public void CheckWaterConsuming()
        {
            CoffeeMat coffeeMat = new CoffeeMat(200, 6);

            coffeeMat.FillWaterTank();

            coffeeMat.AddDrink("Coffee", 0.80);
            coffeeMat.AddDrink("Macciato", 1.80);
            coffeeMat.AddDrink("Capuccino", 1.50);
            coffeeMat.AddDrink("Latte", 1.00);
            coffeeMat.BuyDrink("Coffee");
            coffeeMat.BuyDrink("Macciato");
            coffeeMat.BuyDrink("Capuccino");
            coffeeMat.BuyDrink("Latte");
            coffeeMat.BuyDrink("Milk");

            string actualResult = coffeeMat.BuyDrink("Macciato");

            string expectedResult = "CoffeeMat is out of water!";

            Assert.AreEqual(expectedResult, actualResult);
        }

    }
}