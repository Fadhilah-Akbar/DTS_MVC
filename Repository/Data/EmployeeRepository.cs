using DTS_WebApplication.Context;
using DTS_WebApplication.Models;
using DTS_WebApplication.Repository.Contracts;

namespace DTS_WebApplication.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string, MyContext>, IEmployeeRepository
    {
        public EmployeeRepository(MyContext context) : base(context)
        {
        }
    }
}
