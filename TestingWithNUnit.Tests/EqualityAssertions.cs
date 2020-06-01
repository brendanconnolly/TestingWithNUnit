using NUnit.Framework;

namespace TestingWithNUnit.Tests
{
    
    
    
    [TestFixture]
    [Parallelizable]
    public class EqualityAssertions
    {

        [Test]
        [NonParallelizable]
        public void AreEqual()
        {
            Assert.AreEqual("expected ", "actual");

            Assert.That("actual", Is.EqualTo("expected"));
        }









        [Test]
        public void AreNotEqual()
        {
            Assert.AreNotEqual("expected", "actual");

        }










        [Test]
        public void AreEqualWithDescription()
        {
            Assert.AreEqual("expected", "actual", "what does this even mean");

            Assert.That("actual", Is.EqualTo("expected"), "what does this even mean");
        }

        [Test]
        public void AreEqualWithDynamicDescription()
        {
            Assert.AreEqual("expected", "actual", "what does {0} mean to {1}", "testing", "you");

            Assert.That("actual", Is.EqualTo("expected"), "what does {0} mean to {1}"
                , "testing", "you");
        }

        [Test]
        public void AreEqualNumbers()
        {
            Assert.AreEqual(1, 2);
        }

        [Test]
        public void AreEqualNumbersWithADifferentType()
        {
            Assert.AreEqual(2, 2d);
        }

        [Test]
        public void AreEqualNumbersWithinTolerance()
        {
            Assert.AreEqual(2, 23, .5);
        }



        [Test]
        public void AreEqualArrays()
        {
            var expected = new int[] { 1, 2, 3 };
            var actual = new int[] { 1, 3, 4 };
            Assert.AreEqual(expected, actual);
        }



        [Test]
        public void AreObjectsEqual()
        {
            var obj1 = new object();
            var obj2 = new object();

            var areTheyEqual = obj1.Equals(obj2);
            Assert.IsFalse(areTheyEqual);
            Assert.AreEqual(obj1, obj2);

        }

        [Test]
        public void AreReferencesEqual()
        {
            var obj1 = new object();
            var obj2 = obj1;

            Assert.AreSame(obj1, obj2);
        }






        [Test]
        public void ComparingRelativeValues()
        {
            Assert.GreaterOrEqual(2, 3);
        }





        [Test]
        public void IsStringEmpty()
        {
            var myString = "";
            Assert.IsEmpty(myString);
        }




















        [Test]
        public void AreEqualArrays1()
        {
            var expected = new int[] { 1, 2, 3 };
            var actual = new int[] { 1, 3, 2 };
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void ContainsOneInstanceOfThree()
        {
            int[] arrayOfValues = new int[] { 1, 2, 3 };
            //Assert.AreEqual(1,arrayOfValues.Count(x => x.Equals(3)));
            Assert.That(arrayOfValues, Has.One.EqualTo(3));
        }


        [Test]
        public void IsMoreThanFiveAndLessThan100()
        {
            var testValue = 100;
            Assert.That(testValue,
                Is.GreaterThan(5).Or.LessThan(100));

        }

        [Test]
        public void BeCareful()
        {
            var yourValue = true;
            Assert.That(yourValue, Is.True.Or.False);
            Assert.That(yourValue, Is.True.And.False);
        }





    }
}