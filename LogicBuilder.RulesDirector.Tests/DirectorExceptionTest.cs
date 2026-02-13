using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Xunit;

namespace LogicBuilder.RulesDirector.Tests
{
    public class DirectorExceptionTest
    {
        [Fact]
        public void DefaultConstructor_CreatesException_WithDefaultMessage()
        {
            //arrange & act
            var exception = new DirectorException();

            //assert
            Assert.NotNull(exception);
            Assert.NotNull(exception.Message);
            Assert.Contains("error", exception.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void MessageConstructor_CreatesException_WithCustomMessage()
        {
            //arrange
            string expectedMessage = "Custom error message";

            //act
            var exception = new DirectorException(expectedMessage);

            //assert
            Assert.NotNull(exception);
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public void MessageConstructor_CreatesException_WithEmptyMessage()
        {
            //arrange
            string expectedMessage = string.Empty;

            //act
            var exception = new DirectorException(expectedMessage);

            //assert
            Assert.NotNull(exception);
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public void MessageAndInnerExceptionConstructor_CreatesException_WithBothParameters()
        {
            //arrange
            string expectedMessage = "Outer exception message";
            var innerException = new InvalidOperationException("Inner exception message");

            //act
            var exception = new DirectorException(expectedMessage, innerException);

            //assert
            Assert.NotNull(exception);
            Assert.Equal(expectedMessage, exception.Message);
            Assert.NotNull(exception.InnerException);
            Assert.Equal(innerException, exception.InnerException);
            Assert.Equal("Inner exception message", exception.InnerException.Message);
        }

        [Fact]
        public void MessageAndInnerExceptionConstructor_CreatesException_WithNullInnerException()
        {
            //arrange
            string expectedMessage = "Exception without inner exception";

            //act
            var exception = new DirectorException(expectedMessage, null);

            //assert
            Assert.NotNull(exception);
            Assert.Equal(expectedMessage, exception.Message);
            Assert.Null(exception.InnerException);
        }

        [Fact]
        public void DirectorException_InheritsFromException()
        {
            //arrange & act
            var exception = new DirectorException();

            //assert
            Assert.IsType<Exception>(exception, exactMatch: false);
        }

        [Fact]
        public void DirectorException_CanBeCaught_AsException()
        {
            //arrange
            Exception caughtException;

            //act
            try
            {
                throw new DirectorException("Test exception");
            }
            catch (Exception ex)
            {
                caughtException = ex;
            }

            //assert
            Assert.NotNull(caughtException);
            Assert.IsType<DirectorException>(caughtException);
        }

        [Fact]
        public void DirectorException_CanBeCaught_AsDirectorException()
        {
            //arrange
            DirectorException caughtException;

            //act
            try
            {
                throw new DirectorException("Test exception");
            }
            catch (DirectorException ex)
            {
                caughtException = ex;
            }

            //assert
            Assert.NotNull(caughtException);
            Assert.Equal("Test exception", caughtException.Message);
        }

        [Fact]
        public void DirectorException_PreservesStackTrace()
        {
            //arrange
            DirectorException? caughtException = null;

            //act
            try
            {
                ThrowDirectorException();
            }
            catch (DirectorException ex)
            {
                caughtException = ex;
            }

            //assert
            Assert.NotNull(caughtException);
            Assert.NotNull(caughtException?.StackTrace);
            Assert.Contains(nameof(ThrowDirectorException), caughtException?.StackTrace);
        }

        [Fact]
        public void DirectorException_WithInnerException_PreservesInnerStackTrace()
        {
            //arrange
            Exception caughtException;

            //act
            try
            {
                try
                {
                    throw new ArgumentNullException("testParam");
                }
                catch (ArgumentNullException ex)
                {
                    throw new DirectorException("Wrapping exception", ex);
                }
            }
            catch (DirectorException ex)
            {
                caughtException = ex;
            }

            //assert
            Assert.NotNull(caughtException);
            Assert.NotNull(caughtException.InnerException);
            Assert.IsType<ArgumentNullException>(caughtException.InnerException);
            Assert.NotNull(caughtException.InnerException.StackTrace);
        }

        private static void ThrowDirectorException()
        {
            throw new DirectorException("Exception thrown from helper method");
        }
    }
}