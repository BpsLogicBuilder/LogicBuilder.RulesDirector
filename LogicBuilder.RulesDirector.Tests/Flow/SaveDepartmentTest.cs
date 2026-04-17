using Contoso.Domain.Entities;
using Contoso.Test.Business.Requests;
using Contoso.Test.Flow;
using Contoso.Test.Flow.Cache;
using Contoso.Test.Flow.Rules;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace LogicBuilder.RulesDirector.Tests.Flow
{
    public class SaveDepartmentTest
    {
        public SaveDepartmentTest(ITestOutputHelper output)
        {
            this.output = output;
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        private readonly ITestOutputHelper output;
        #endregion Fields

        [Fact]
        public void SaveDepartment()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            flowManager.FlowDataCache.Request = new SaveEntityRequest
            {
                Entity = new DepartmentModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Modified,
                    InstructorID = 1,
                    Budget = 10000,
                    StartDate = new DateTime(2020, 2, 2),
                    Name = "Physics"
                }
            };

            //act
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("savedepartment");
            stopWatch.Stop();
            this.output.WriteLine("Saving valid department  = {0}", stopWatch.Elapsed.TotalMilliseconds);

            //assert
            Assert.True(flowManager.FlowDataCache.Response.Success);
        }

        [Fact]
        public void SaveInvalidDepartment()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            flowManager.FlowDataCache.Request = new SaveEntityRequest
            {
                Entity = new DepartmentModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Modified,
                    InstructorID = null,
                    Budget = -1,
                    StartDate = new DateTime(),
                    Name = ""
                }
            };

            //act
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("savedepartment");
            stopWatch.Stop();
            this.output.WriteLine("Saving invalid department  = {0}", stopWatch.Elapsed.TotalMilliseconds);

            //assert
            Assert.False(flowManager.FlowDataCache.Response.Success);
            Assert.Equal(4, flowManager.FlowDataCache.Response.ErrorMessages.Count);
        }

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
