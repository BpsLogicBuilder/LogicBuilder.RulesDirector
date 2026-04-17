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
            DateTime beforeCreation = DateTime.UtcNow;

            //act
            var progressInfo = new ProgressInfo(expectedDescription);
            DateTime afterCreation = DateTime.UtcNow;

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
            DateTime beforeCreation = DateTime.UtcNow;
            var progressInfo = new ProgressInfo("Test");
            DateTime afterCreation = DateTime.UtcNow;

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
        public void Equals_ReturnsFalseForNullArgument()
        {
            //arrange
            var progressInfo = new ProgressInfo("Test");

            //act
            bool result = progressInfo.Equals(null);

            //assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_ReturnsTrueForSameDescription_WithObjectParameterOverride()
        {
            //arrange
            string description = "Same description";
            var progressInfo1 = new ProgressInfo(description);
            var progressInfo2 = new ProgressInfo(description);

            //act
            bool result = progressInfo1.Equals((object)progressInfo2);

            //assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_ReturnsFalseForDifferentDescription_WithObjectParameterOverride()
        {
            //arrange
            var progressInfo1 = new ProgressInfo("Description 1");
            var progressInfo2 = new ProgressInfo("Description 2");

            //act
            bool result = progressInfo1.Equals((object)progressInfo2);

            //assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_ReturnsFalseForNullArgument_WithObjectParameterOverride()
        {
            //arrange
            var progressInfo = new ProgressInfo("Test");

            //act
            bool result = progressInfo.Equals((object?)null);

            //assert
            Assert.False(result);
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
        public void CompareTo_ReturnsOneForNullArgument()
        {
            //arrange
            var progressInfo = new ProgressInfo("Test");

            //act
            int result = progressInfo.CompareTo(null);

            //assert
            Assert.Equal(1, result);
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

        [Fact]
        public void GetHashCode_ReturnsSameHashCodeForSameDescription()
        {
            //arrange
            string description = "Same description";
            var progressInfo1 = new ProgressInfo(description);
            var progressInfo2 = new ProgressInfo(description);

            //act
            int hashCode1 = progressInfo1.GetHashCode();
            int hashCode2 = progressInfo2.GetHashCode();

            //assert
            Assert.Equal(hashCode1, hashCode2);
        }

        [Fact]
        public void GetHashCode_ReturnsDifferentHashCodeForDifferentDescription()
        {
            //arrange
            var progressInfo1 = new ProgressInfo("Description 1");
            var progressInfo2 = new ProgressInfo("Description 2");

            //act
            int hashCode1 = progressInfo1.GetHashCode();
            int hashCode2 = progressInfo2.GetHashCode();

            //assert
            Assert.NotEqual(hashCode1, hashCode2);
        }

        [Fact]
        public void EqualityOperator_ReturnsTrueForSameDescription()
        {
            //arrange
            string description = "Same description";
            var progressInfo1 = new ProgressInfo(description);
            var progressInfo2 = new ProgressInfo(description);

            //act
            bool result = progressInfo1 == progressInfo2;

            //assert
            Assert.True(result);
        }

        [Fact]
        public void EqualityOperator_ReturnsTrueForSameReference()
        {
            //arrange
            var progressInfo1 = new ProgressInfo("Test");
            var progressInfo2 = new ProgressInfo("Test");

            //act
            bool result = progressInfo1 == progressInfo2;

            //assert
            Assert.True(result);
        }

        [Fact]
        public void EqualityOperator_ReturnsTrueForBothNull()
        {
            //arrange
            ProgressInfo? progressInfo1 = null;
            ProgressInfo? progressInfo2 = null;

            //act
            bool result = progressInfo1 == progressInfo2;

            //assert
            Assert.True(result);
        }

        [Fact]
        public void EqualityOperator_ReturnsFalseForDifferentDescription()
        {
            //arrange
            var progressInfo1 = new ProgressInfo("Description 1");
            var progressInfo2 = new ProgressInfo("Description 2");

            //act
            bool result = progressInfo1 == progressInfo2;

            //assert
            Assert.False(result);
        }

        [Fact]
        public void EqualityOperator_ReturnsFalseWhenLeftIsNull()
        {
            //arrange
            ProgressInfo? progressInfo1 = null;
            var progressInfo2 = new ProgressInfo("Test");

            //act
            bool result = progressInfo1 == progressInfo2;

            //assert
            Assert.False(result);
        }

        [Fact]
        public void EqualityOperator_ReturnsFalseWhenRightIsNull()
        {
            //arrange
            var progressInfo1 = new ProgressInfo("Test");
            ProgressInfo? progressInfo2 = null;

            //act
            bool result = progressInfo1 == progressInfo2;

            //assert
            Assert.False(result);
        }

        [Fact]
        public void InequalityOperator_ReturnsFalseForSameDescription()
        {
            //arrange
            string description = "Same description";
            var progressInfo1 = new ProgressInfo(description);
            var progressInfo2 = new ProgressInfo(description);

            //act
            bool result = progressInfo1 != progressInfo2;

            //assert
            Assert.False(result);
        }

        [Fact]
        public void InequalityOperator_ReturnsTrueForDifferentDescription()
        {
            //arrange
            var progressInfo1 = new ProgressInfo("Description 1");
            var progressInfo2 = new ProgressInfo("Description 2");

            //act
            bool result = progressInfo1 != progressInfo2;

            //assert
            Assert.True(result);
        }

        [Fact]
        public void InequalityOperator_ReturnsTrueWhenLeftIsNull()
        {
            //arrange
            ProgressInfo? progressInfo1 = null;
            var progressInfo2 = new ProgressInfo("Test");

            //act
            bool result = progressInfo1 != progressInfo2;

            //assert
            Assert.True(result);
        }

        [Fact]
        public void InequalityOperator_ReturnsTrueWhenRightIsNull()
        {
            //arrange
            var progressInfo1 = new ProgressInfo("Test");
            ProgressInfo? progressInfo2 = null;

            //act
            bool result = progressInfo1 != progressInfo2;

            //assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOperator_ReturnsTrueWhenLeftIsLessThanRight()
        {
            //arrange
            var progressInfo1 = new ProgressInfo("AAA");
            var progressInfo2 = new ProgressInfo("ZZZ");

            //act
            bool result = progressInfo1 < progressInfo2;

            //assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOperator_ReturnsFalseWhenLeftIsGreaterThanRight()
        {
            //arrange
            var progressInfo1 = new ProgressInfo("ZZZ");
            var progressInfo2 = new ProgressInfo("AAA");

            //act
            bool result = progressInfo1 < progressInfo2;

            //assert
            Assert.False(result);
        }

        [Fact]
        public void LessThanOperator_ReturnsFalseWhenEqual()
        {
            //arrange
            string description = "Same description";
            var progressInfo1 = new ProgressInfo(description);
            var progressInfo2 = new ProgressInfo(description);

            //act
            bool result = progressInfo1 < progressInfo2;

            //assert
            Assert.False(result);
        }

        [Fact]
        public void LessThanOperator_ReturnsTrueWhenLeftIsNull()
        {
            //arrange
            ProgressInfo? progressInfo1 = null;
            var progressInfo2 = new ProgressInfo("Test");

            //act
            bool result = progressInfo1 < progressInfo2;

            //assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOperator_ReturnsFalseWhenBothAreNull()
        {
            //arrange
            ProgressInfo? progressInfo1 = null;
            ProgressInfo? progressInfo2 = null;

            //act
            bool result = progressInfo1 < progressInfo2;

            //assert
            Assert.False(result);
        }

        [Fact]
        public void LessThanOperator_ReturnsFalseWhenRightIsNull()
        {
            //arrange
            var progressInfo1 = new ProgressInfo("Test");
            ProgressInfo? progressInfo2 = null;

            //act
            bool result = progressInfo1 < progressInfo2;

            //assert
            Assert.False(result);
        }

        [Fact]
        public void LessThanOrEqualOperator_ReturnsTrueWhenLeftIsLessThanRight()
        {
            //arrange
            var progressInfo1 = new ProgressInfo("AAA");
            var progressInfo2 = new ProgressInfo("ZZZ");

            //act
            bool result = progressInfo1 <= progressInfo2;

            //assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqualOperator_ReturnsTrueWhenEqual()
        {
            //arrange
            string description = "Same description";
            var progressInfo1 = new ProgressInfo(description);
            var progressInfo2 = new ProgressInfo(description);

            //act
            bool result = progressInfo1 <= progressInfo2;

            //assert
            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqualOperator_ReturnsFalseWhenLeftIsGreaterThanRight()
        {
            //arrange
            var progressInfo1 = new ProgressInfo("ZZZ");
            var progressInfo2 = new ProgressInfo("AAA");

            //act
            bool result = progressInfo1 <= progressInfo2;

            //assert
            Assert.False(result);
        }

        [Fact]
        public void LessThanOrEqualOperator_ReturnsTrueWhenLeftIsNull()
        {
            //arrange
            ProgressInfo? progressInfo1 = null;
            var progressInfo2 = new ProgressInfo("Test");

            //act
            bool result = progressInfo1 <= progressInfo2;

            //assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOperator_ReturnsTrueWhenLeftIsGreaterThanRight()
        {
            //arrange
            var progressInfo1 = new ProgressInfo("ZZZ");
            var progressInfo2 = new ProgressInfo("AAA");

            //act
            bool result = progressInfo1 > progressInfo2;

            //assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOperator_ReturnsFalseWhenLeftIsLessThanRight()
        {
            //arrange
            var progressInfo1 = new ProgressInfo("AAA");
            var progressInfo2 = new ProgressInfo("ZZZ");

            //act
            bool result = progressInfo1 > progressInfo2;

            //assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThanOperator_ReturnsFalseWhenEqual()
        {
            //arrange
            string description = "Same description";
            var progressInfo1 = new ProgressInfo(description);
            var progressInfo2 = new ProgressInfo(description);

            //act
            bool result = progressInfo1 > progressInfo2;

            //assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThanOperator_ReturnsFalseWhenLeftIsNull()
        {
            //arrange
            ProgressInfo? progressInfo1 = null;
            var progressInfo2 = new ProgressInfo("Test");

            //act
            bool result = progressInfo1 > progressInfo2;

            //assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThanOrEqualOperator_ReturnsTrueWhenLeftIsGreaterThanRight()
        {
            //arrange
            var progressInfo1 = new ProgressInfo("ZZZ");
            var progressInfo2 = new ProgressInfo("AAA");

            //act
            bool result = progressInfo1 >= progressInfo2;

            //assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqualOperator_ReturnsTrueWhenEqual()
        {
            //arrange
            string description = "Same description";
            var progressInfo1 = new ProgressInfo(description);
            var progressInfo2 = new ProgressInfo(description);

            //act
            bool result = progressInfo1 >= progressInfo2;

            //assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqualOperator_ReturnsFalseWhenLeftIsLessThanRight()
        {
            //arrange
            var progressInfo1 = new ProgressInfo("AAA");
            var progressInfo2 = new ProgressInfo("ZZZ");

            //act
            bool result = progressInfo1 >= progressInfo2;

            //assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThanOrEqualOperator_ReturnsFalseWhenLeftIsNull()
        {
            //arrange
            ProgressInfo? progressInfo1 = null;
            var progressInfo2 = new ProgressInfo("Test");

            //act
            bool result = progressInfo1 >= progressInfo2;

            //assert
            Assert.False(result);
        }

        [Fact]
        public void GreaterThanOrEqualOperator_ReturnsTrueWhenBothAreNull()
        {
            //arrange
            ProgressInfo? progressInfo1 = null;
            ProgressInfo? progressInfo2 = null;

            //act
            bool result = progressInfo1 >= progressInfo2;

            //assert
            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqualOperator_ReturnsTrueWhenRightIsNull()
        {
            //arrange
            var progressInfo1 = new ProgressInfo("Test");
            ProgressInfo? progressInfo2 = null;

            //act
            bool result = progressInfo1 >= progressInfo2;

            //assert
            Assert.True(result);
        }
    }
}