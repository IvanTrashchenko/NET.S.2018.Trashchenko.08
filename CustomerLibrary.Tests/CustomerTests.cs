using System;
using System.Globalization;
using CustomerExtensionLibrary;
using NUnit.Framework;

namespace CustomerLibrary.Tests
{
    [TestFixture]
    public class CustomerTests
    {
        private readonly Customer customer =
            new Customer("Jeffrey Richter", "+1 (425) 555-0100", 1000000);

        [TestCase("NRP", ExpectedResult = "Customer record: Jeffrey Richter, 1,000,000.00, +1 (425) 555-0100")]
        [TestCase("NP", ExpectedResult = "Customer record: Jeffrey Richter, +1 (425) 555-0100")]
        public string Customer_ToString_Invariant_Test(string format) =>
            customer.ToString(format, CultureInfo.InvariantCulture);

        /*[TestCase("R", ExpectedResult = "Customer record: 1 000 000,00")]
        [TestCase("NPR", ExpectedResult = "Customer record: Jeffrey Richter, +1 (425) 555-0100, 1 000 000,00")]
        public string Customer_ToString_Region_Test(string format) =>
            customer.ToString(format, new CultureInfo("ru-ru"));*/ 

        // not working here but working with linqpad?

        [TestCase(ExpectedResult = "Customer record: Jeffrey Richter, 1,000,000.00, +1 (425) 555-0100")]
        public string Customer_ToString_Test() =>
            customer.ToString();

        [TestCase("jeffrey richter", "+1 (425) 555-0100", 1000000)]
        [TestCase("Jeffrey Richter", "5555555", 1000000)]
        public void Customer_ThrowArgumentException(string name, string contactPhone, decimal revenue)
        {
            Assert.Throws<ArgumentException>(() => new Customer(name, contactPhone, revenue));
        }

        [TestCase("np")]
        [TestCase("")]
        public void Customer_ToString_ThrowFormatException(string format)
        {
            Assert.Throws<FormatException>(() => customer.ToString(format, CultureInfo.InvariantCulture));
        }

        [TestCase("F", ExpectedResult = "Customer record: Jeffrey")]
        [TestCase("LRP", ExpectedResult = "Customer record: Richter, 1,000,000.00, +1 (425) 555-0100")]
        public string Customer_Format_Test(string format)
        {
            CustomerFormatProvider fp = new CustomerFormatProvider();
            return fp.Format(format, customer, CultureInfo.InvariantCulture);
        }

        [TestCase(null)]
        [TestCase(23)]
        public void Customer_Format_ThrowFormatException(object arg)
        {
            CustomerFormatProvider fp = new CustomerFormatProvider();
            Assert.Throws<FormatException>(() => fp.Format("LRP", arg, CultureInfo.InvariantCulture));
        }
    }
}

