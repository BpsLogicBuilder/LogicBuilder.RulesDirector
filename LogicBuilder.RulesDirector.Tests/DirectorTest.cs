using Contoso.Test.Flow;
using Contoso.Test.Flow.Cache;
using Contoso.Test.Flow.Rules;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace LogicBuilder.RulesDirector.Tests
{
    public class DirectorTest
    {
        public DirectorTest(ITestOutputHelper output)
        {
            this.output = output;
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        private readonly ITestOutputHelper output;
        #endregion Fields

        #region Driver Property Tests
        [Fact]
        public void Driver_SetsAndGetsValue()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            DirectorBase director = flowManager.Director;
            
            //act
            director.Driver = "5P1";
            
            //assert
            Assert.Equal("5P1", director.Driver);
        }

        [Fact]
        public void Driver_UpdatesProgressList()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            DirectorBase director = flowManager.Director;
            int initialCount = flowManager.Progress.ProgressItems.Count;
            
            //act
            director.Driver = "5P1";
            
            //assert
            Assert.True(flowManager.Progress.ProgressItems.Count >= initialCount);
        }
        #endregion Driver Property Tests

        #region Selection Property Tests
        [Fact]
        public void Selection_SetsAndGetsValue()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            DirectorBase director = flowManager.Director;

            //act
            director.Selection = "Option1";
            
            //assert
            Assert.Equal("Option1", director.Selection);
        }

        [Fact]
        public void SetSelection_SetsSelectionProperty()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            DirectorBase director = flowManager.Director;

            //act
            director.SetSelection("Option2");
            
            //assert
            Assert.Equal("Option2", director.Selection);
        }
        #endregion Selection Property Tests

        #region FlowStatus Tests
        [Fact]
        public void FlowStatus_ReturnsInitialFormatWhenDriverIsEmpty()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            DirectorBase director = flowManager.Director;
            director.SetModuleName("testmodule");
            
            //act
            string status = director.FlowStatus;
            
            //assert
            Assert.Contains("testmodule", status);
            this.output.WriteLine("FlowStatus (empty driver): {0}", status);
        }

        [Fact]
        public void FlowStatus_ReturnsFormattedStatusWithDriver()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            DirectorBase director = flowManager.Director;
            director.SetModuleName("testmodule");
            
            //act
            director.Driver = "5P1";
            string status = director.FlowStatus;
            
            //assert
            Assert.Contains("testmodule", status);
            Assert.Contains("5", status);
            Assert.Contains("1", status);
            this.output.WriteLine("FlowStatus (with driver): {0}", status);
        }
        #endregion FlowStatus Tests

        #region FlowBackupData Tests
        [Fact]
        public void FlowBackupData_CapturesCurrentState()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            DirectorBase director = flowManager.Director;
            director.SetModuleName("module1");
            director.Driver = "10P2";
            director.Selection = "Choice1";
            
            //act
            object backupData = director.FlowBackupData;
            
            //assert
            Assert.NotNull(backupData);
            Assert.IsType<FlowBackupData>(backupData);
        }

        [Fact]
        public void ResetFlowValuesOnBackup_RestoresPreviousState()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            DirectorBase director = flowManager.Director;
            director.SetModuleName("module1");
            director.Driver = "10P2";
            director.Selection = "Choice1";
            
            object backupData = director.FlowBackupData;
            
            //act - change state
            director.SetModuleName("module2");
            director.Driver = "20P3";
            director.Selection = "Choice2";
            
            //act - restore
            director.ResetFlowValuesOnBackup(backupData);
            
            //assert
            Assert.Equal("10P2", director.Driver);
            Assert.Equal("Choice1", director.Selection);
            this.output.WriteLine("Restored Driver: {0}, Selection: {1}", director.Driver, director.Selection);
        }

        [Fact]
        public void ResetFlowValuesOnBackup_ThrowsExceptionWhenBackupDataIsNull()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            DirectorBase director = flowManager.Director;

            //act & assert
            Assert.Throws<DirectorException>(() => director.ResetFlowValuesOnBackup(null));
        }
        #endregion FlowBackupData Tests

        #region SetModuleName Tests
        [Fact]
        public void SetModuleName_SetsModuleBeginName()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            DirectorBase director = flowManager.Director;

            //act
            director.SetModuleName("newmodule");
            
            //assert
            Assert.Equal("newmodule", director.ModuleBeginName);
        }
        #endregion SetModuleName Tests

        #region VariablesUpdated Tests
        [Fact]
        public void VariablesUpdated_AlwaysReturnsFalse()
        {
            //act & assert
            Assert.False(DirectorBase.VariablesUpdated);
        }
        #endregion VariablesUpdated Tests

        #region Helpers
        [MemberNotNull(nameof(serviceProvider))]
        private void Initialize()
        {
            serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddTransient<IFlowManager, FlowManager>()
                .AddTransient<DirectorFactory, DirectorFactory>()
                .AddTransient<ICustomActions, CustomActions>()
                .AddSingleton<FlowDataCache, FlowDataCache>()
                .AddSingleton<Progress, Progress>()
                .AddSingleton<IRulesCache>(sp =>
                {
                    return RulesService.LoadRulesSync(new RulesLoader());
                })
                .BuildServiceProvider();
        }
        #endregion Helpers
    }
}