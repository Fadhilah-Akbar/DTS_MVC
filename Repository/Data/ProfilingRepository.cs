using DTS_WebApplication.Context;
using DTS_WebApplication.Models;
using DTS_WebApplication.Repository.Contracts;

namespace DTS_WebApplication.Repository.Data
{
    public class ProfilingRepository : GeneralRepository<Profiling, string, MyContext>, IProfilingRepository
    {
        public ProfilingRepository(MyContext context) : base(context)
        {
        }
    }
}
