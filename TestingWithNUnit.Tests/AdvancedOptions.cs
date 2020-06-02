using System;
using FluentAssertions;
using NUnit.Framework;

namespace TestingWithNUnit.Tests
{
    [TestFixture]
    public class AdvancedOptions
    {

        [Test]
        public void UsingWarnings()
        {
            var isProcessed = false;
            Warn.Unless(isProcessed, Is.EqualTo(true)
                .After(1).Minutes.PollEvery(10).Seconds);
            Console.WriteLine("still going");
        }

        [Test]
        public void Assumptions()
        {
            Assume.That("a value", Is.EqualTo("this value"));
        }

        [Test]
        public void AssumingThenAsserting()
        {
            var customSettingEnabled = false;
            Assume.That(customSettingEnabled, Is.True);

            // test actions here...

            Assert.That("actual", Is.EqualTo("expected"));

        }

        [Test]
        public void AssertPassThrowsException()
        {
            Assert.That(Assert.Pass, Throws.TypeOf<AssertionException>());
        }

        [Test]
        public void WillThisMakeItThroughCodeReview()
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, 2);
                Assert.AreEqual(3, 4);
            });
        }

        [Test]
        public void AssertingWithFluentAssertions()
        {
            "actual".Should().Be("expected");
        }








    }
}