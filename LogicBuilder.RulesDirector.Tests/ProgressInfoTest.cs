using System;

namespace LogicBuilder.RulesDirector.Tests
{
    public class ProgressInfoTest
    {
        [Fact]
        public void Constructor_InitializesDescriptionAndDateTime()
        {
            //arrange
            string expectedDescription = "Test progress description";
            DateTime beforeCreation = DateTime.Now;

            //act
            var progressInfo = new ProgressInfo(expectedDescription);
            DateTime afterCreation = DateTime.Now;

            //assert
            Assert.NotNull(progressInfo);
            Assert.Equal(expectedDescription, progressInfo.Description);
            Assert.True(progressInfo.DateAndTime >= beforeCreation);
            Assert.True(progressInfo.DateAndTime <= afterCreation);
        }

        [Fact]
        public void Description_ReturnsCorrectValue()
        {
            //arrange
            string expectedDescription = "Sample description";
            var progressInfo = new ProgressInfo(expectedDescription);

            //act
            string actualDescription = progressInfo.Description;

            //assert
            Assert.Equal(expectedDescription, actualDescription);
        }

        [Fact]
        public void DateAndTime_ReturnsCorrectValue()
        {
            //arrange
            DateTime beforeCreation = DateTime.Now;
            var progressInfo = new ProgressInfo("Test");
            DateTime afterCreation = DateTime.Now;

            //act
            DateTime actualDateTime = progressInfo.DateAndTime;

            //assert
            Assert.True(actualDateTime >= beforeCreation);
            Assert.True(actualDateTime <= afterCreation);
        }

        [Fact]
        public void Equals_ReturnsTrueForSameDescription()
        {
            //arrange
            string description = "Same description";
            var progressInfo1 = new ProgressInfo(description);
            var progressInfo2 = new ProgressInfo(description);

            //act
            bool result = progressInfo1.Equals(progressInfo2);

            //assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_ReturnsFalseForDifferentDescription()
        {
            //arrange
            var progressInfo1 = new ProgressInfo("Description 1");
            var progressInfo2 = new ProgressInfo("Description 2");

            //act
            bool result = progressInfo1.Equals(progressInfo2);

            //assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_ThrowsInvalidOperationExceptionForNullArgument()
        {
            //arrange
            var progressInfo = new ProgressInfo("Test");

            //act & assert
            Assert.Throws<InvalidOperationException>(() => progressInfo.Equals(null));
        }

        [Fact]
        public void CompareTo_ReturnsZeroForSameDescription()
        {
            //arrange
            string description = "Same description";
            var progressInfo1 = new ProgressInfo(description);
            var progressInfo2 = new ProgressInfo(description);

            //act
            int result = progressInfo1.CompareTo(progressInfo2);

            //assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void CompareTo_ReturnsNegativeWhenFirstIsLessThanSecond()
        {
            //arrange
            var progressInfo1 = new ProgressInfo("AAA");
            var progressInfo2 = new ProgressInfo("ZZZ");

            //act
            int result = progressInfo1.CompareTo(progressInfo2);

            //assert
            Assert.True(result < 0);
        }

        [Fact]
        public void CompareTo_ReturnsPositiveWhenFirstIsGreaterThanSecond()
        {
            //arrange
            var progressInfo1 = new ProgressInfo("ZZZ");
            var progressInfo2 = new ProgressInfo("AAA");

            //act
            int result = progressInfo1.CompareTo(progressInfo2);

            //assert
            Assert.True(result > 0);
        }

        [Fact]
        public void CompareTo_ThrowsInvalidOperationExceptionForNullArgument()
        {
            //arrange
            var progressInfo = new ProgressInfo("Test");

            //act & assert
            Assert.Throws<InvalidOperationException>(() => progressInfo.CompareTo(null));
        }

        [Fact]
        public void ToString_ReturnsFormattedString()
        {
            //arrange
            string description = "Progress description";
            var progressInfo = new ProgressInfo(description);

            //act
            string result = progressInfo.ToString();

            //assert
            Assert.NotNull(result);
            Assert.Contains(description, result);
            Assert.Contains(":", result); // Time separator
        }

        [Fact]
        public void ToString_ContainsDescriptionAndTime()
        {
            //arrange
            string description = "Test item";
            var progressInfo = new ProgressInfo(description);
            string expectedTime = progressInfo.DateAndTime.ToString("T", System.Globalization.CultureInfo.CurrentCulture);

            //act
            string result = progressInfo.ToString();

            //assert
            Assert.Contains(description, result);
            Assert.Contains(expectedTime, result);
        }
    }
}