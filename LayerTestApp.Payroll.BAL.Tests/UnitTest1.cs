using LayerTestApp.Payroll.BAL.ServiceContracts;
using LayerTestApp.Payroll.BAL.Services;
using LayerTestApp.Payroll.DAL.Data;
using LayerTestApp.Payroll.DAL.Repositories;
using LayerTestApp.Payroll.DAL.RepositoryContracts;
using Microsoft.Extensions.Hosting;

namespace LayerTestApp.Payroll.BAL.Tests
{
    public class Tests : BaseTestClass
    {        
        [SetUp]
        public void Setup()
        {
            //var context = Host.Services.GetRequiredService<LTAPayrollDbContext>();
            //payGradeService = new PayGradeService(new PayGradeRepository( context,);
        }

        [Test]
        public void Test1()
        {

            var pay = Task.Run(() => PayGradeService.GetAllAsync()).Result;
        }
    }
}