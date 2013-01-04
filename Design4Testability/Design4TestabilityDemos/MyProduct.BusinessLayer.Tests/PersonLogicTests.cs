using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyProduct.BusinessLayer.Interfaces;


namespace MyProduct.BusinessLayer.Tests
{
    class MyLittleFakeValidator : IPersonValidator
    {
        public bool WillReturnIsValid;
        public bool IsValid(Person p)
        {
            return WillReturnIsValid;
        }
    }

    [TestClass]
    public class PersonLogicTests
    {
        [TestMethod]
        public void CanPurchase_PersonWitNoCreditPassesValidation_ReturnFalse()
        {
            MyLittleFakeValidator validator = new MyLittleFakeValidator();
            validator.WillReturnIsValid = true;

            PersonLogicBTestable pl = new PersonLogicBTestable(validator);
            
            bool canPurchase = pl.CanPurchase(new Person());
            Assert.IsFalse(canPurchase);
        }











        [TestMethod]
        public void CanPurchase_CreditLessThan1_ReturnsFalse()
        {
            FakeTestingValidator fakeValidator
                        = makeFakeValidator(true);

            PersonLogicBTestable logic =
                makeLogicWithValidator(fakeValidator);

            Person p = makePersonWithPurchasePossible();
            p.CreditOnFile = 0;

            //test
            bool canPurchase = logic.CanPurchase(p);
            Assert.IsFalse(canPurchase);
        }

        [TestMethod]
        public void CanPurchase_NullSubcriptionType_ReturnsFalse()
        {
            FakeTestingValidator fakeValidator
                        = makeFakeValidator(true);

            PersonLogicBTestable logicUnderTest =
                makeLogicWithValidator(fakeValidator);

            Person p = makePersonWithPurchasePossible();
            p.SubscriptionType = null;

            //test
            bool canPurchase = logicUnderTest.CanPurchase(p);
            Assert.IsFalse(canPurchase);
        }

        [TestMethod]
        public void CanPurchase_NullSSID_ReturnsFalse()
        {
            FakeTestingValidator fakeValidator 
                        = makeFakeValidator(true);

            PersonLogicBTestable logicUnderTest =
                makeLogicWithValidator(fakeValidator);

            //Person to pass in
            Person p = makePersonWithPurchasePossible();
            p.SSID = null;

            //test
            bool canPurchase = logicUnderTest.CanPurchase(p);
            Assert.IsFalse(canPurchase);
        }

        [TestMethod]
        public void CanPurchase_IsNotValid_ReturnsFalse()
        {
            FakeTestingValidator fakeValidator
                        = makeFakeValidator(false);

            PersonLogicBTestable logicUnderTest = 
                makeLogicWithValidator(fakeValidator);

            //Person to pass in
            Person p = makePersonWithPurchasePossible();


            //test
            bool canPurchase = logicUnderTest.CanPurchase(p);
            Assert.IsFalse(canPurchase);
        }

        [TestMethod]
        public void CanPurchase_AllIsWell_ReturnsTrue()
        {
            FakeTestingValidator fakeValidator
                        = makeFakeValidator(true);

            PersonLogicBTestable logicUnderTest = new PersonLogicBTestable(fakeValidator);
            //Person to pass in
            Person p = makePersonWithPurchasePossible();
            
            //test
            bool canPurchase = logicUnderTest.CanPurchase(p);
            Assert.IsTrue(canPurchase);
        }

        #region helper methods
        private PersonLogicBTestable makeLogicWithValidator(FakeTestingValidator fakeValidator)
        {
            PersonLogicBTestable logicUnderTest = new PersonLogicBTestable(fakeValidator);
            return logicUnderTest;
        }

        private Person makePersonWithPurchasePossible()
        {
            Person p = new Person();
            p.SSID = "abc";
            p.SubscriptionType = new MonthlySubscriptionType();
            p.CreditOnFile = 1;
            return p;
        }

        private FakeTestingValidator makeFakeValidator(bool validToReturn)
        {
            FakeTestingValidator fakeValidator =
                        new FakeTestingValidator();
            fakeValidator.ValidStatusToReturn = validToReturn;
            return fakeValidator;
        } 
        #endregion
    }

    class FakeTestingValidator :IPersonValidator
    {
        public bool ValidStatusToReturn;
        public bool IsValid(Person p)
        {
            return ValidStatusToReturn;
        }
    }
}
