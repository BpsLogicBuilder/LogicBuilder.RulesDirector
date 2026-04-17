using System;
using System.Collections;

namespace LogicBuilder.RulesDirector.Tests
{
    public class FlowBackupDataTest
    {
        [Fact]
        public void Constructor_CreatesInstance_WithAllPropertiesSet()
        {
            //arrange
            string driver = "TestDriver";
            string selection = "TestSelection";
            Stack callingModuleDriverStack = new();
            callingModuleDriverStack.Push("driver1");
            callingModuleDriverStack.Push("driver2");
            Stack callingModuleStack = new();
            callingModuleStack.Push("module1");
            callingModuleStack.Push("module2");
            string moduleBeginName = "BeginModule";
            string moduleEndName = "EndModule";

            //act
            var flowBackupData = new FlowBackupData(driver, selection, callingModuleDriverStack, callingModuleStack, moduleBeginName, moduleEndName);

            //assert
            Assert.NotNull(flowBackupData);
            Assert.Equal(driver, flowBackupData.Driver);
            Assert.Equal(selection, flowBackupData.Selection);
            Assert.Equal(callingModuleDriverStack, flowBackupData.CallingModuleDriverStack);
            Assert.Equal(callingModuleStack, flowBackupData.CallingModuleStack);
            Assert.Equal(moduleBeginName, flowBackupData.ModuleBeginName);
            Assert.Equal(moduleEndName, flowBackupData.ModuleEndName);
        }

        [Fact]
        public void Constructor_CreatesInstance_WithNullSelection()
        {
            //arrange
            string driver = "TestDriver";
            string selection = null!;
            Stack callingModuleDriverStack = new();
            Stack callingModuleStack = new();
            string moduleBeginName = "BeginModule";
            string moduleEndName = "EndModule";

            //act
            var flowBackupData = new FlowBackupData(driver, selection, callingModuleDriverStack, callingModuleStack, moduleBeginName, moduleEndName);

            //assert
            Assert.NotNull(flowBackupData);
            Assert.Null(flowBackupData.Selection);
        }

        [Fact]
        public void GetHashCode_ReturnsHashCodeBasedOnDriver()
        {
            //arrange
            string driver = "TestDriver";
            var flowBackupData = new FlowBackupData(driver, "selection", new Stack(), new Stack(), "begin", "end");
            int expectedHashCode = driver.GetHashCode();

            //act
            int actualHashCode = flowBackupData.GetHashCode();

            //assert
            Assert.Equal(expectedHashCode, actualHashCode);
        }

        [Fact]
        public void GetHashCode_ReturnsSameHashCode_ForObjectsWithSameDriver()
        {
            //arrange
            string driver = "TestDriver";
            var flowBackupData1 = new FlowBackupData(driver, "selection1", new Stack(), new Stack(), "begin1", "end1");
            var flowBackupData2 = new FlowBackupData(driver, "selection2", new Stack(), new Stack(), "begin2", "end2");

            //act
            int hashCode1 = flowBackupData1.GetHashCode();
            int hashCode2 = flowBackupData2.GetHashCode();

            //assert
            Assert.Equal(hashCode1, hashCode2);
        }

        [Fact]
        public void Equals_ReturnsFalse_WhenComparedWithNull()
        {
            //arrange
            var flowBackupData = new FlowBackupData("driver", "selection", new Stack(), new Stack(), "begin", "end");

            //act
            bool result = flowBackupData.Equals((object?)null);

            //assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_ReturnsFalse_WhenComparedWithDifferentType()
        {
            //arrange
            var flowBackupData = new FlowBackupData("driver", "selection", new Stack(), new Stack(), "begin", "end");
            object other = "string object";

            //act
            bool result = flowBackupData.Equals(other);

            //assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_ReturnsTrue_WhenComparedWithIdenticalObject()
        {
            //arrange
            string driver = "TestDriver";
            string selection = "TestSelection";
            string moduleBeginName = "BeginModule";
            string moduleEndName = "EndModule";
            var flowBackupData1 = new FlowBackupData(driver, selection, new Stack(), new Stack(), moduleBeginName, moduleEndName);
            var flowBackupData2 = new FlowBackupData(driver, selection, new Stack(), new Stack(), moduleBeginName, moduleEndName);

            //act
            bool result = flowBackupData1.Equals(flowBackupData2);

            //assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_ReturnsFalse_WhenDriverIsDifferent()
        {
            //arrange
            var flowBackupData1 = new FlowBackupData("driver1", "selection", new Stack(), new Stack(), "begin", "end");
            var flowBackupData2 = new FlowBackupData("driver2", "selection", new Stack(), new Stack(), "begin", "end");

            //act
            bool result = flowBackupData1.Equals(flowBackupData2);

            //assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_ReturnsFalse_WhenSelectionIsDifferent()
        {
            //arrange
            var flowBackupData1 = new FlowBackupData("driver", "selection1", new Stack(), new Stack(), "begin", "end");
            var flowBackupData2 = new FlowBackupData("driver", "selection2", new Stack(), new Stack(), "begin", "end");

            //act
            bool result = flowBackupData1.Equals(flowBackupData2);

            //assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_ReturnsFalse_WhenModuleBeginNameIsDifferent()
        {
            //arrange
            var flowBackupData1 = new FlowBackupData("driver", "selection", new Stack(), new Stack(), "begin1", "end");
            var flowBackupData2 = new FlowBackupData("driver", "selection", new Stack(), new Stack(), "begin2", "end");

            //act
            bool result = flowBackupData1.Equals(flowBackupData2);

            //assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_ReturnsFalse_WhenModuleEndNameIsDifferent()
        {
            //arrange
            var flowBackupData1 = new FlowBackupData("driver", "selection", new Stack(), new Stack(), "begin", "end1");
            var flowBackupData2 = new FlowBackupData("driver", "selection", new Stack(), new Stack(), "begin", "end2");

            //act
            bool result = flowBackupData1.Equals(flowBackupData2);

            //assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_IgnoresStackDifferences()
        {
            //arrange
            Stack stack1 = new();
            stack1.Push("item1");
            Stack stack2 = new();
            stack2.Push("item2");
            var flowBackupData1 = new FlowBackupData("driver", "selection", stack1, new Stack(), "begin", "end");
            var flowBackupData2 = new FlowBackupData("driver", "selection", stack2, new Stack(), "begin", "end");

            //act
            bool result = flowBackupData1.Equals(flowBackupData2);

            //assert
            Assert.True(result);
        }

        [Fact]
        public void IEquatable_Equals_ReturnsFalse_WhenComparedWithNull()
        {
            //arrange
            var flowBackupData = new FlowBackupData("driver", "selection", new Stack(), new Stack(), "begin", "end");

            //act
            bool result = flowBackupData.Equals(null);

            //assert
            Assert.False(result);
        }

        [Fact]
        public void IEquatable_Equals_ReturnsTrue_WhenAllPropertiesMatch()
        {
            //arrange
            string driver = "TestDriver";
            string selection = "TestSelection";
            string moduleBeginName = "BeginModule";
            string moduleEndName = "EndModule";
            var flowBackupData1 = new FlowBackupData(driver, selection, new Stack(), new Stack(), moduleBeginName, moduleEndName);
            var flowBackupData2 = new FlowBackupData(driver, selection, new Stack(), new Stack(), moduleBeginName, moduleEndName);

            //act
            bool result = ((IEquatable<FlowBackupData>)flowBackupData1).Equals(flowBackupData2);

            //assert
            Assert.True(result);
        }

        [Fact]
        public void ObjectEquals_UsesIEquatableEquals()
        {
            //arrange
            string driver = "TestDriver";
            string selection = "TestSelection";
            string moduleBeginName = "BeginModule";
            string moduleEndName = "EndModule";
            var flowBackupData1 = new FlowBackupData(driver, selection, new Stack(), new Stack(), moduleBeginName, moduleEndName);
            object flowBackupData2 = new FlowBackupData(driver, selection, new Stack(), new Stack(), moduleBeginName, moduleEndName);

            //act
            bool result = flowBackupData1.Equals(flowBackupData2);

            //assert
            Assert.True(result);
        }
    }
}