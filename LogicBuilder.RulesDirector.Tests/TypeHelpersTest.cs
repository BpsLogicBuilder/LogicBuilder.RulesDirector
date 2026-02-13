using System;
using Xunit;

namespace LogicBuilder.RulesDirector.Tests
{
    public class TypeHelpersTest
    {
        #region TryParse Tests

        [Fact]
        public void TryParse_WithString_ReturnsTrueAndParsedValue()
        {
            //arrange
            string input = "test string";

            //act
            bool result = input.TryParse(typeof(string), out object parsed);

            //assert
            Assert.True(result);
            Assert.Equal("test string", parsed);
        }

        [Fact]
        public void TryParse_WithValidInt_ReturnsTrueAndParsedValue()
        {
            //arrange
            string input = "42";

            //act
            bool result = input.TryParse(typeof(int), out object parsed);

            //assert
            Assert.True(result);
            Assert.Equal(42, parsed);
        }

        [Fact]
        public void TryParse_WithInvalidInt_ReturnsFalseAndNull()
        {
            //arrange
            string input = "not a number";

            //act
            bool result = input.TryParse(typeof(int), out object parsed);

            //assert
            Assert.False(result);
            Assert.Null(parsed);
        }

        [Fact]
        public void TryParse_WithValidDecimal_ReturnsTrueAndParsedValue()
        {
            //arrange
            string input = "123.45";

            //act
            bool result = input.TryParse(typeof(decimal), out object parsed);

            //assert
            Assert.True(result);
            Assert.Equal(123.45m, parsed);
        }

        [Fact]
        public void TryParse_WithValidDouble_ReturnsTrueAndParsedValue()
        {
            //arrange
            string input = "123.45";

            //act
            bool result = input.TryParse(typeof(double), out object parsed);

            //assert
            Assert.True(result);
            Assert.Equal(123.45, parsed);
        }

        [Fact]
        public void TryParse_WithValidFloat_ReturnsTrueAndParsedValue()
        {
            //arrange
            string input = "123.45";

            //act
            bool result = input.TryParse(typeof(float), out object parsed);

            //assert
            Assert.True(result);
            Assert.Equal(123.45f, parsed);
        }

        [Fact]
        public void TryParse_WithValidBool_ReturnsTrueAndParsedValue()
        {
            //arrange
            string input = "true";

            //act
            bool result = input.TryParse(typeof(bool), out object parsed);

            //assert
            Assert.True(result);
            Assert.Equal(true, parsed);
        }

        [Fact]
        public void TryParse_WithValidDateTime_ReturnsTrueAndParsedValue()
        {
            //arrange
            string input = "2026-02-13";

            //act
            bool result = input.TryParse(typeof(DateTime), out object parsed);

            //assert
            Assert.True(result);
            Assert.IsType<DateTime>(parsed);
        }

        [Fact]
        public void TryParse_WithValidDateTimeOffset_ReturnsTrueAndParsedValue()
        {
            //arrange
            string input = "2026-02-13T10:30:00+00:00";

            //act
            bool result = input.TryParse(typeof(DateTimeOffset), out object parsed);

            //assert
            Assert.True(result);
            Assert.IsType<DateTimeOffset>(parsed);
        }

        [Fact]
        public void TryParse_WithValidTimeSpan_ReturnsTrueAndParsedValue()
        {
            //arrange
            string input = "01:30:00";

            //act
            bool result = input.TryParse(typeof(TimeSpan), out object parsed);

            //assert
            Assert.True(result);
            Assert.IsType<TimeSpan>(parsed);
        }

        [Fact]
        public void TryParse_WithValidGuid_ReturnsTrueAndParsedValue()
        {
            //arrange
            string input = "12345678-1234-1234-1234-123456789012";

            //act
            bool result = input.TryParse(typeof(Guid), out object parsed);

            //assert
            Assert.True(result);
            Assert.IsType<Guid>(parsed);
        }

        [Fact]
        public void TryParse_WithValidByte_ReturnsTrueAndParsedValue()
        {
            //arrange
            string input = "255";

            //act
            bool result = input.TryParse(typeof(byte), out object parsed);

            //assert
            Assert.True(result);
            Assert.Equal((byte)255, parsed);
        }

        [Fact]
        public void TryParse_WithValidShort_ReturnsTrueAndParsedValue()
        {
            //arrange
            string input = "1234";

            //act
            bool result = input.TryParse(typeof(short), out object parsed);

            //assert
            Assert.True(result);
            Assert.Equal((short)1234, parsed);
        }

        [Fact]
        public void TryParse_WithValidLong_ReturnsTrueAndParsedValue()
        {
            //arrange
            string input = "123456789";

            //act
            bool result = input.TryParse(typeof(long), out object parsed);

            //assert
            Assert.True(result);
            Assert.Equal(123456789L, parsed);
        }

        [Fact]
        public void TryParse_WithValidChar_ReturnsTrueAndParsedValue()
        {
            //arrange
            string input = "A";

            //act
            bool result = input.TryParse(typeof(char), out object parsed);

            //assert
            Assert.True(result);
            Assert.Equal('A', parsed);
        }

        [Fact]
        public void TryParse_WithValidSByte_ReturnsTrueAndParsedValue()
        {
            //arrange
            string input = "127";

            //act
            bool result = input.TryParse(typeof(sbyte), out object parsed);

            //assert
            Assert.True(result);
            Assert.Equal((sbyte)127, parsed);
        }

        [Fact]
        public void TryParse_WithValidUShort_ReturnsTrueAndParsedValue()
        {
            //arrange
            string input = "65535";

            //act
            bool result = input.TryParse(typeof(ushort), out object parsed);

            //assert
            Assert.True(result);
            Assert.Equal((ushort)65535, parsed);
        }

        [Fact]
        public void TryParse_WithValidUInt_ReturnsTrueAndParsedValue()
        {
            //arrange
            string input = "4294967295";

            //act
            bool result = input.TryParse(typeof(uint), out object parsed);

            //assert
            Assert.True(result);
            Assert.Equal(4294967295u, parsed);
        }

        [Fact]
        public void TryParse_WithValidULong_ReturnsTrueAndParsedValue()
        {
            //arrange
            string input = "18446744073709551615";

            //act
            bool result = input.TryParse(typeof(ulong), out object parsed);

            //assert
            Assert.True(result);
            Assert.Equal(18446744073709551615ul, parsed);
        }

        [Fact]
        public void TryParse_WithNullableInt_ReturnsTrueAndParsedValue()
        {
            //arrange
            string input = "42";

            //act
            bool result = input.TryParse(typeof(int?), out object parsed);

            //assert
            Assert.True(result);
            Assert.Equal(42, parsed);
        }

        [Fact]
        public void TryParse_WithNullableIntInvalidValue_ReturnsFalseAndNull()
        {
            //arrange
            string input = "invalid";

            //act
            bool result = input.TryParse(typeof(int?), out object parsed);

            //assert
            Assert.False(result);
            Assert.Null(parsed);
        }

        [Fact]
        public void TryParse_WithValidEnum_ReturnsTrueAndParsedValue()
        {
            //arrange
            string input = "Saturday";

            //act
            bool result = input.TryParse(typeof(DayOfWeek), out object parsed);

            //assert
            Assert.True(result);
            Assert.Equal(DayOfWeek.Saturday, parsed);
        }

        [Fact]
        public void TryParse_WithInvalidEnum_ReturnsFalseAndNull()
        {
            //arrange
            string input = "InvalidDay";

            //act
            bool result = input.TryParse(typeof(DayOfWeek), out object parsed);

            //assert
            Assert.False(result);
            Assert.Null(parsed);
        }

        [Fact]
        public void TryParse_WithNullType_ReturnsFalseAndNull()
        {
            //arrange
            string input = "test";

            //act & assert
            var exception = Assert.Throws<ArgumentException>(() =>
                input.TryParse(null, out _));

            Assert.Equal("type", exception.ParamName);
            Assert.Contains("Argument cannot be null", exception.Message);
        }

        [Fact]
        public void TryParse_WithNonLiteralType_ThrowsArgumentException()
        {
            //arrange
            string input = "test";

            //act & assert
            var exception = Assert.Throws<ArgumentException>(() => 
                input.TryParse(typeof(TypeHelpersTest), out _));
            
            Assert.Equal("type", exception.ParamName);
            Assert.Contains("Not a valid literal type", exception.Message);
        }

        #endregion

        #region IsNullable Tests

        [Fact]
        public void IsNullable_WithNullableInt_ReturnsTrue()
        {
            //arrange
            Type type = typeof(int?);

            //act
            bool result = type.IsNullable();

            //assert
            Assert.True(result);
        }

        [Fact]
        public void IsNullable_WithNullableDateTime_ReturnsTrue()
        {
            //arrange
            Type type = typeof(DateTime?);

            //act
            bool result = type.IsNullable();

            //assert
            Assert.True(result);
        }

        [Fact]
        public void IsNullable_WithNonNullableInt_ReturnsFalse()
        {
            //arrange
            Type type = typeof(int);

            //act
            bool result = type.IsNullable();

            //assert
            Assert.False(result);
        }

        [Fact]
        public void IsNullable_WithString_ReturnsFalse()
        {
            //arrange
            Type type = typeof(string);

            //act
            bool result = type.IsNullable();

            //assert
            Assert.False(result);
        }

        [Fact]
        public void IsNullable_WithReferenceType_ReturnsFalse()
        {
            //arrange
            Type type = typeof(object);

            //act
            bool result = type.IsNullable();

            //assert
            Assert.False(result);
        }

        #endregion

        #region CanBeAssignedNull Tests (Obsolete)

        [Fact]
        public void CanBeAssignedNull_WithString_ReturnsTrue()
        {
            //arrange
            Type type = typeof(string);

            //act
#pragma warning disable CS0618 // Type or member is obsolete
            bool result = type.CanBeAssignedNull();
#pragma warning restore CS0618 // Type or member is obsolete

            //assert
            Assert.True(result);
        }

        [Fact]
        public void CanBeAssignedNull_WithNullableInt_ReturnsTrue()
        {
            //arrange
            Type type = typeof(int?);

            //act
#pragma warning disable CS0618 // Type or member is obsolete
            bool result = type.CanBeAssignedNull();
#pragma warning restore CS0618 // Type or member is obsolete

            //assert
            Assert.True(result);
        }

        [Fact]
        public void CanBeAssignedNull_WithInt_ReturnsFalse()
        {
            //arrange
            Type type = typeof(int);

            //act
#pragma warning disable CS0618 // Type or member is obsolete
            bool result = type.CanBeAssignedNull();
#pragma warning restore CS0618 // Type or member is obsolete

            //assert
            Assert.False(result);
        }

        [Fact]
        public void CanBeAssignedNull_WithReferenceType_ReturnsTrue()
        {
            //arrange
            Type type = typeof(object);

            //act
#pragma warning disable CS0618 // Type or member is obsolete
            bool result = type.CanBeAssignedNull();
#pragma warning restore CS0618 // Type or member is obsolete

            //assert
            Assert.True(result);
        }

        #endregion

        #region AssignableFrom Tests (Obsolete)

        [Fact]
        public void AssignableFrom_WithSameType_ReturnsTrue()
        {
            //arrange
            Type to = typeof(int);
            Type from = typeof(int);

            //act
#pragma warning disable CS0618 // Type or member is obsolete
            bool result = to.AssignableFrom(from);
#pragma warning restore CS0618 // Type or member is obsolete

            //assert
            Assert.True(result);
        }

        [Fact]
        public void AssignableFrom_WithDerivedType_ReturnsTrue()
        {
            //arrange
            Type to = typeof(object);
            Type from = typeof(string);

            //act
#pragma warning disable CS0618 // Type or member is obsolete
            bool result = to.AssignableFrom(from);
#pragma warning restore CS0618 // Type or member is obsolete

            //assert
            Assert.True(result);
        }

        [Fact]
        public void AssignableFrom_WithNumericWidening_ByteToInt_ReturnsTrue()
        {
            //arrange
            Type to = typeof(int);
            Type from = typeof(byte);

            //act
#pragma warning disable CS0618 // Type or member is obsolete
            bool result = to.AssignableFrom(from);
#pragma warning restore CS0618 // Type or member is obsolete

            //assert
            Assert.True(result);
        }

        [Fact]
        public void AssignableFrom_WithNumericWidening_IntToLong_ReturnsTrue()
        {
            //arrange
            Type to = typeof(long);
            Type from = typeof(int);

            //act
#pragma warning disable CS0618 // Type or member is obsolete
            bool result = to.AssignableFrom(from);
#pragma warning restore CS0618 // Type or member is obsolete

            //assert
            Assert.True(result);
        }

        [Fact]
        public void AssignableFrom_WithNumericWidening_IntToDecimal_ReturnsTrue()
        {
            //arrange
            Type to = typeof(decimal);
            Type from = typeof(int);

            //act
#pragma warning disable CS0618 // Type or member is obsolete
            bool result = to.AssignableFrom(from);
#pragma warning restore CS0618 // Type or member is obsolete

            //assert
            Assert.True(result);
        }

        [Fact]
        public void AssignableFrom_WithNumericWidening_IntToDouble_ReturnsTrue()
        {
            //arrange
            Type to = typeof(double);
            Type from = typeof(int);

            //act
#pragma warning disable CS0618 // Type or member is obsolete
            bool result = to.AssignableFrom(from);
#pragma warning restore CS0618 // Type or member is obsolete

            //assert
            Assert.True(result);
        }

        [Fact]
        public void AssignableFrom_WithNumericWidening_FloatToDouble_ReturnsTrue()
        {
            //arrange
            Type to = typeof(double);
            Type from = typeof(float);

            //act
#pragma warning disable CS0618 // Type or member is obsolete
            bool result = to.AssignableFrom(from);
#pragma warning restore CS0618 // Type or member is obsolete

            //assert
            Assert.True(result);
        }

        [Fact]
        public void AssignableFrom_WithNullableToNullable_ReturnsTrue()
        {
            //arrange
            Type to = typeof(int?);
            Type from = typeof(byte?);

            //act
#pragma warning disable CS0618 // Type or member is obsolete
            bool result = to.AssignableFrom(from);
#pragma warning restore CS0618 // Type or member is obsolete

            //assert
            Assert.True(result);
        }

        [Fact]
        public void AssignableFrom_WithNonNullableToNullable_ReturnsTrue()
        {
            //arrange
            Type to = typeof(int?);
            Type from = typeof(int);

            //act
#pragma warning disable CS0618 // Type or member is obsolete
            bool result = to.AssignableFrom(from);
#pragma warning restore CS0618 // Type or member is obsolete

            //assert
            Assert.True(result);
        }

        [Fact]
        public void AssignableFrom_WithNullableToNonNullable_ReturnsFalse()
        {
            //arrange
            Type to = typeof(int);
            Type from = typeof(int?);

            //act
#pragma warning disable CS0618 // Type or member is obsolete
            bool result = to.AssignableFrom(from);
#pragma warning restore CS0618 // Type or member is obsolete

            //assert
            Assert.False(result);
        }

        [Fact]
        public void AssignableFrom_WithIncompatibleTypes_ReturnsFalse()
        {
            //arrange
            Type to = typeof(int);
            Type from = typeof(string);

            //act
#pragma warning disable CS0618 // Type or member is obsolete
            bool result = to.AssignableFrom(from);
#pragma warning restore CS0618 // Type or member is obsolete

            //assert
            Assert.False(result);
        }

        #endregion
    }
}