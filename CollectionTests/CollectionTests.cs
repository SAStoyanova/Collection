using Collections;
using NUnit.Framework;
using System;
using System.Linq;

namespace Collection.Unit.Tests
{
    public class CollectionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_Collections_EmptyConstructor()
        {
            var nums = new Collection<int>();

            Assert.That(nums.Count == 0, "Count property");
            Assert.That(nums.Capacity == 16, "Capacity property");
            Assert.That(nums.ToString() == "[]");
        }

        [Test]
        public void Test_Collections_ConstructorSingleItem()
        {
            var nums = new Collection<int>(5);

            Assert.That(nums.Count == 1, "Count property");
            Assert.That(nums.Capacity == 16, "Capacity property");
            Assert.That(nums.ToString() == "[5]");
        }

        [Test]
        public void Test_Collections_ConstructorMultipleItems()
        {
            var nums = new Collection<int>(5, 7);

            Assert.That(nums.Count == 2, "Count property");
            Assert.That(nums.Capacity == 16, "Capacity property");
            Assert.That(nums.ToString() == "[5, 7]");
        }

        [Test]
        public void Test_Collections_AddItem()
        {
            var nums = new Collection<int>();

            nums.Add(9);

            Assert.That(nums.Count == 1, "Count property");
            Assert.That(nums.Capacity == 16, "Capacity property");
            Assert.That(nums.ToString() == "[9]");
        }

        [Test]
        public void Test_Collections_AddItemString()
        {
            var nums = new Collection<string>();

            nums.Add("QA");

            Assert.That(nums.Count == 1, "Count property");
            Assert.That(nums.Capacity == 16, "Capacity property");
            Assert.That(nums.ToString() == "[QA]");
        }

        [Test]
        public void Test_Collections_AddRange()
        {
            var items = new int[] { 6, 7, 8 };
            var nums = new Collection<int>();

            nums.AddRange(items);

            Assert.That(nums.Count == 3, "Count property");
            Assert.That(nums.Capacity == 16, "Capacity property");
            Assert.That(nums.ToString() == "[6, 7, 8]");
        }

        [Test]
        public void Test_Collections_AddWithGrow()
        {
            var nums = new Collection<int>();
            int oldCapacity = nums.Capacity;
            var newNums = Enumerable.Range(1000, 2000).ToArray();
            nums.AddRange(newNums);
            string expectedNums = "[" + string.Join(", ", newNums) + "]";
            Assert.That(nums.ToString(), Is.EqualTo(expectedNums));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));

        }

        [Test]
        public void Test_Collections_GetByIndex()
        {
            var names = new Collection<string>("Misho", "Pesho");
            var item0 = names[0];
            var item1 = names[1];

            Assert.That(item0, Is.EqualTo("Misho"));
            Assert.That(item1, Is.EqualTo("Pesho"));

        }

        [Test]
        [Timeout(1000)]
        public void Test_Collection_1MillionItems()
        {
            const int itemsCount = 1000000;
            var nums = new Collection<int>();
            nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());
            Assert.That(nums.Count == itemsCount);
            Assert.That(nums.Capacity >= nums.Count);
            for (int i = itemsCount - 1; i >= 0; i--)
                nums.RemoveAt(i);
            Assert.That(nums.ToString() == "[]");
            Assert.That(nums.Capacity >= nums.Count);
        }

        [Test]
        public void Test_Collection_ExhangeWithOneInvalidIndex()
        {
            var items = new int[] { 3, 4, 5, 6, 7 };
            var nums = new Collection<int>();

            nums.AddRange(3, 4, 5, 6, 7);
            Assert.Throws<ArgumentOutOfRangeException>(() => nums.Exchange(2, 7));
        }

        [Test]
        public void Test_Collection_ExhangeWithTwoInvalidIndex()
        {
            var items = new int[] { 1, 2, 3, 4 };
            var nums = new Collection<int>();

            nums.AddRange(1, 2, 3, 4);
            Assert.Throws<ArgumentOutOfRangeException>(() => nums.Exchange(-2, 4));
        }

        [Test]
        public void Test_Collection_Clear()
        {
            var nums = new Collection<int>();

            nums.Clear();

            Assert.That(nums.Count == 0);
        }

        [TestCase("Peter", 0, "Peter")]
        public void Test_Collection_GetByValidIndex(
            string data, int index, string expectedData)
        {
            var nums = new Collection<string>(data);

            var actual = nums[index];

            Assert.AreEqual(expectedData, actual);
        }

        [TestCase("Peter", 0, "Peter")]
        public void Test_Collection_SetByValidIndex(
            string data, int index, string expectedData)
        {
            var nums = new Collection<string>(data);

            var actual = "Peter";

            nums[index] = actual;

            Assert.AreEqual(expectedData, actual);
        }

        [Test]
        public void Test_Collections_RemovedAt()
        {
            var names = new Collection<string>("Misho", "Pesho");

            var removedItem = names.RemoveAt(0);

            Assert.That(removedItem, Is.EqualTo("Misho"));
            Assert.That(names.Count == 1, "Count property");
            

        }

        [Test]
        public void Test_Collections_InsertAt()
        {
            var nums = new Collection<int>(3, 5);

            nums.InsertAt(0, 3);

            Assert.That(nums.Count == 3, "Count property");

        }

        [Test]
        public void Test_Collections_Exchange()
        {
            var nums = new Collection<int>(3, 5);
          
            nums.Exchange(0, 1);

            Assert.That(nums[1], Is.EqualTo(3));
            Assert.That(nums[0], Is.EqualTo(5));
        }
    }   
}