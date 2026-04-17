using Contoso.Domain.Entities;
using Contoso.Test.Business.Requests;
using Contoso.Test.Flow;
using Contoso.Test.Flow.Cache;
using Contoso.Test.Flow.Rules;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

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

        #region CopyStack Test
        [Fact]
        public void CopyStack_CopiesStateForExistingState()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            flowManager.FlowDataCache.Request = new SaveEntityRequest
            {
                Entity = new StudentModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Modified,
                    FirstName = "",
                    LastName = "",
                    EnrollmentDate = default
                }
            };

            //act - get backup data
            flowManager.Start("savestudent");
            FlowBackupData backupData = (FlowBackupData)flowManager.Director.FlowBackupData;

            //assert
            Assert.Single(backupData.CallingModuleDriverStack);
            Assert.Single(backupData.CallingModuleDriverStack);
        }
        #endregion CopyStack Test

        #region UpdateProgressList Test
        [Fact]
        public void UpdateProgressList_ReturnsForDuplicateProgressItem()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            flowManager.FlowDataCache.Request = new SaveEntityRequest
            {
                Entity = new StudentModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Modified,
                    FirstName = "",
                    LastName = "",
                    EnrollmentDate = default
                }
            };

            //act
            flowManager.Start("savestudent");
            int count = flowManager.Progress.ProgressItems.Count;
            flowManager.Director.UpdateProgressList("NewValue");
            flowManager.Director.UpdateProgressList("NewValue");

            //assert
            Assert.Equal(count + 1, flowManager.Progress.ProgressItems.Count);
        }
        #endregion UpdateProgressList Test

        #region CurrentShapeIndex Test
        [Fact]
        public void FlowStatus_CurrentModuleIfCurrentDriverIsEmpty()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            flowManager.FlowDataCache.Request = new SaveEntityRequest
            {
                Entity = new StudentModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Modified,
                    FirstName = "",
                    LastName = "",
                    EnrollmentDate = default
                }
            };

            //act
            flowManager.Start("savestudent");
            flowManager.Director.Driver = "";
            string flowStatus = flowManager.Director.FlowStatus;

            //assert
            Assert.Equal(string.Format(CultureInfo.CurrentCulture, Strings.flowStatusFormatInitial, "validatestudent"), flowStatus);
        }

        [Fact]
        public void FlowStatus_ReturnsZeroForPageNumberIf_CurrentDriverConnotBeSplit()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            flowManager.FlowDataCache.Request = new SaveEntityRequest
            {
                Entity = new StudentModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Modified,
                    FirstName = "",
                    LastName = "",
                    EnrollmentDate = default
                }
            };

            //act
            flowManager.Start("savestudent");
            flowManager.Director.Driver = "invalidDriver";
            string flowStatus = flowManager.Director.FlowStatus;

            //assert
            Assert.Equal(string.Format(CultureInfo.CurrentCulture, Strings.flowStatusFormat, "validatestudent", 0, 0), flowStatus);
        }
        #endregion CurrentShapeIndex Test

        #region ExecuteRulesEngine Test
        [Fact]
        public void ExecuteRuleEngine_DoesNotChangeState_WithNoAdditionalChanges()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            flowManager.FlowDataCache.Request = new SaveEntityRequest
            {
                Entity = new StudentModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Modified,
                    FirstName = "",
                    LastName = "",
                    EnrollmentDate = default
                }
            };

            //act
            flowManager.Start("savestudent");
            FlowBackupData backupData = (FlowBackupData)flowManager.Director.FlowBackupData;
            Assert.Single(backupData.CallingModuleStack);
            Assert.Single(backupData.CallingModuleDriverStack);
            flowManager.Director.ExecuteRulesEngine();

            //assert
            Assert.Single(backupData.CallingModuleStack);
            Assert.Single(backupData.CallingModuleDriverStack);
        }

        [Fact]
        public void ExecuteRuleEngine_ThrowsExceptionIfRulesEngineNotFound()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            flowManager.FlowDataCache.Request = new SaveEntityRequest
            {
                Entity = new StudentModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Modified,
                    FirstName = "",
                    LastName = "",
                    EnrollmentDate = default
                }
            };

            //act
            flowManager.Start("savestudent");
            flowManager.Director.SetModuleName("invalidModule");

            //assert
            Assert.Throws<InvalidOperationException>(flowManager.Director.ExecuteRulesEngine);
        }
        #endregion ExecuteRulesEngine Test

        #region StartInitialFlow Test
        [Fact]
        public void StartInitialFlow_ThrowsExceptionIfModuleNameIsNull()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();

            //act && assert
            Assert.Throws<ArgumentException>(() => flowManager.Director.StartInitialFlow(null));
        }

        [Fact]
        public void StartInitialFlow_ThrowsExceptionIfRulesEngineNotFound()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();

            //act && assert
            Assert.Throws<InvalidOperationException>(() => flowManager.Director.StartInitialFlow("invalidModule"));
        }
        #endregion StartInitialFlow Test

        #region SetModuleName Tests
        [Fact]
        public void SetModuleBeginNameThrows_IfRulesEngineNotFound()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            DirectorBase director = flowManager.Director;

            //act & assert
            Assert.Throws<InvalidOperationException>(() => director.ModuleBeginName = "invalidModule");
        }
        #endregion SetModuleName Tests

        #region SetModuleName Tests
        [Fact]
        public void SetModuleEndNameThrows_IfRulesEngineNotFound()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            flowManager.FlowDataCache.Request = new SaveEntityRequest
            {
                Entity = new StudentModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Modified,
                    FirstName = "",
                    LastName = "",
                    EnrollmentDate = default
                }
            };

            //act
            flowManager.Start("savestudent");
            FlowBackupData backupData = (FlowBackupData)flowManager.Director.FlowBackupData;
            Assert.Single(backupData.CallingModuleStack);
            Assert.Single(backupData.CallingModuleDriverStack);
            backupData = new FlowBackupData
            (
                backupData.Driver,
                backupData.Selection,
                backupData.CallingModuleDriverStack,
                new System.Collections.Stack(new List<string> { "invalidModule" }),
                backupData.ModuleBeginName,
                backupData.ModuleEndName
            );
            flowManager.Director.ResetFlowValuesOnBackup(backupData);

            //act & assert
            Assert.Throws<InvalidOperationException>(() => flowManager.Director.ModuleEndName = "savestudent");
        }
        #endregion SetModuleName Tests

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