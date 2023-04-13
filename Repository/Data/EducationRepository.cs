using DTS_WebApplication.Context;
using DTS_WebApplication.Models;
using DTS_WebApplication.Repository.Contracts;

namespace DTS_WebApplication.Repository.Data
{
    public class EducationRepository : GeneralRepository<Education, int, MyContext>, IEducationRepository
    {
        public EducationRepository(MyContext context) : base(context)
        {
        }
    }
}
