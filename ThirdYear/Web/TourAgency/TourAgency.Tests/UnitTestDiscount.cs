using Microsoft.VisualStudio.TestTools.UnitTesting;
using TourAgency.Bll.BusinessModels;

namespace TourAgency.Tests
{
    [TestClass]
    public class UnitTestDiscount
    {
        [TestMethod]
        public void DiscountPrice_0andAny_AnyReturned()
        {
            int discount = 0;
            int any = 5000;
            int actual = Discount.DiscountPrice(any, discount);
            Assert.AreEqual(any, actual);
        }
        [TestMethod]
        public void AddDiscount_0and2and14_2Returned()
        {
            int discount = 0;
            int step = 2;
            int maxdDiscount = 14;
            int actual = Discount.AddDiscount(discount, step, maxdDiscount);
            Assert.AreEqual(2, actual);
        }
        [TestMethod]
        public void ReduceDiscount_0_0Returned()
        {
            int discount = 0;
            int actual = Discount.ReduceDiscount(discount);
            Assert.AreEqual(discount, actual);
        }
        [TestMethod]
        public void ReduceDiscount_15_10Returned()
        {
            int discount = 15;
            int actual = Discount.ReduceDiscount(discount);
            int result = 10;
            Assert.AreEqual(result, actual);
        }
        [TestMethod]
        public void DiscountPrice_10andAny_AnyReturned()
        {
            int discount = 10;
            int any = 5000;
            int resultPrice = 4500;
            int actual = Discount.DiscountPrice(any, discount);
            Assert.AreEqual(resultPrice, actual);
        }
        [TestMethod]
        public void AddDiscount_0and20and15_15Returned()
        {
            int discount = 0;
            int step = 20;
            int maxDiscount = 15;
            int actual = Discount.AddDiscount(discount, step, maxDiscount);
            Assert.AreEqual(maxDiscount, actual);
        }
        [TestMethod]
        public void AddDiscount_15and2and15_15Returned()
        {
            int discount = 15;
            int step = 2;
            int maxDiscount = 15;
            int actual = Discount.AddDiscount(discount, step, maxDiscount);
            Assert.AreEqual(maxDiscount, actual);
        }
    }
}
