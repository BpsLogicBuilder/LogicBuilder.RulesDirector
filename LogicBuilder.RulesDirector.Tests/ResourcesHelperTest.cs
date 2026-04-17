using Contoso.Test.Flow;
using Contoso.Test.Flow.Cache;
using Contoso.Test.Flow.Rules;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace LogicBuilder.RulesDirector.Tests
{
    public class ResourcesHelperTest
    {
        public ResourcesHelperTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        #endregion Fields

        #region GetResource Tests - Success Cases
        [Fact]
        public void GetResource_ReturnsValueFromCache_WhenResourceStringExists()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            DirectorBase director = flowManager.Director;
            string shortValue = "testKey";

            //act
            int result = ResourcesHelper<int>.GetResource(shortValue, director);

            //assert
            Assert.Equal(42, result);
        }

        [Fact]
        public void GetResource_ParsesShortValue_WhenResourceStringDoesNotExist()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            DirectorBase director = flowManager.Director;
            string shortValue = "100";

            //act
            int result = ResourcesHelper<int>.GetResource(shortValue, director);

            //assert
            Assert.Equal(100, result);
        }

        [Fact]
        public void GetResource_ReturnsStringValue_WhenTypeIsString()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            DirectorBase director = flowManager.Director;
            string shortValue = "stringKey";
            string longValue = "Hello World";

            //act
            string result = ResourcesHelper<string>.GetResource(shortValue, director);

            //assert
            Assert.Equal(longValue, result);
        }

        [Fact]
        public void GetResource_ReturnsDecimalValue_WhenTypeIsDecimal()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            DirectorBase director = flowManager.Director;
            string shortValue = "decimalKey";

            //act
            decimal result = ResourcesHelper<decimal>.GetResource(shortValue, director);

            //assert
            Assert.Equal(123.45m, result);
        }

        [Fact]
        public void GetResource_ReturnsDoubleValue_WhenTypeIsDouble()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            DirectorBase director = flowManager.Director;
            string shortValue = "doubleKey";

            //act
            double result = ResourcesHelper<double>.GetResource(shortValue, director);

            //assert
            Assert.Equal(3.14159, result);
        }

        [Fact]
        public void GetResource_ReturnsBoolValue_WhenTypeIsBool()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            DirectorBase director = flowManager.Director;
            string shortValue = "boolKey";

            //act
            bool result = ResourcesHelper<bool>.GetResource(shortValue, director);

            //assert
            Assert.True(result);
        }

        [Fact]
        public void GetResource_ReturnsDateTimeValue_WhenTypeIsDateTime()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            DirectorBase director = flowManager.Director;
            string shortValue = "dateKey";

            //act
            DateTime result = ResourcesHelper<DateTime>.GetResource(shortValue, director);

            //assert
            Assert.Equal(new DateTime(2026, 2, 13), result);
        }

        [Fact]
        public void GetResource_ParsesShortValue_ForDecimalWhenNotInCache()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            DirectorBase director = flowManager.Director;
            string shortValue = "99.99";

            //act
            decimal result = ResourcesHelper<decimal>.GetResource(shortValue, director);

            //assert
            Assert.Equal(99.99m, result);
        }
        #endregion GetResource Tests - Success Cases

        #region GetResource Tests - Exception Cases
        [Fact]
        public void GetResource_ThrowsDirectorException_WhenLongValueCannotBeParsed()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            DirectorBase director = flowManager.Director;
            string shortValue = "invalidIntKey";
            string longValue = "not_a_number";

            //act & assert
            DirectorException exception = Assert.Throws<DirectorException>(() => 
                ResourcesHelper<int>.GetResource(shortValue, director));
            
            Assert.Contains("System.Int32", exception.Message);
            Assert.Contains(shortValue, exception.Message);
            Assert.Contains(longValue, exception.Message);
        }

        [Fact]
        public void GetResource_ThrowsDirectorException_WhenShortValueCannotBeParsed()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            DirectorBase director = flowManager.Director;
            string shortValue = "invalid_int_value";

            //act & assert
            DirectorException exception = Assert.Throws<DirectorException>(() => 
                ResourcesHelper<int>.GetResource(shortValue, director));
            
            Assert.Contains(shortValue, exception.Message);
        }

        [Fact]
        public void GetResource_ThrowsDirectorException_WhenDateTimeParsingFails()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            DirectorBase director = flowManager.Director;
            string shortValue = "invalidDate";

            //act & assert
            DirectorException exception = Assert.Throws<DirectorException>(() => 
                ResourcesHelper<DateTime>.GetResource(shortValue, director));
            
            Assert.Contains(shortValue, exception.Message);
        }

        [Fact]
        public void GetResource_ThrowsDirectorException_WhenBoolParsingFailsFromCache()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            DirectorBase director = flowManager.Director;
            string shortValue = "invalidBoolKey";

            //act & assert
            DirectorException exception = Assert.Throws<DirectorException>(() => 
                ResourcesHelper<bool>.GetResource(shortValue, director));
            
            Assert.Contains("System.Boolean", exception.Message);
            Assert.Contains(shortValue, exception.Message);
        }
        #endregion GetResource Tests - Exception Cases

        #region Helpers
        [MemberNotNull(nameof(serviceProvider))]
        private void Initialize()
        {
            // Create test resource strings dictionary
            var resourceStrings = new ConcurrentDictionary<string, string>
            {
                ["testKey"] = "42",
                ["stringKey"] = "Hello World",
                ["decimalKey"] = "123.45",
                ["doubleKey"] = "3.14159",
                ["boolKey"] = "true",
                ["dateKey"] = "2026-02-13",
                ["invalidIntKey"] = "not_a_number",
                ["invalidBoolKey"] = "not_a_bool"
            };

            var ruleEngines = new ConcurrentDictionary<string, Workflow.Activities.Rules.RuleEngine>();

            serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddTransient<IFlowManager, FlowManager>()
                .AddTransient<DirectorFactory, DirectorFactory>()
                .AddTransient<ICustomActions, CustomActions>()
                .AddSingleton<FlowDataCache, FlowDataCache>()
                .AddSingleton<Progress, Progress>()
                .AddSingleton<IRulesCache>(sp =>
                {
                    return new RulesCache(ruleEngines, resourceStrings);
                })
                .BuildServiceProvider();
        }
        #endregion Helpers
    }
}