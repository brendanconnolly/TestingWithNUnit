using NUnit.Framework;

namespace TestingWithNUnit.Tests
{
    public class MyFirstNUnitTests
    {
        [Test]
        [Category("first")]
        public void IAmDoingGreat()
        {
            Assert.True(true);

        }

    }
}