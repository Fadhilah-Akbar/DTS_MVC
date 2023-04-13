using DTS_WebApplication.Context;
using DTS_WebApplication.Models;
using DTS_WebApplication.Repository.Contracts;

namespace DTS_WebApplication.Repository.Data
{
    public class UniversityRepository : GeneralRepository<University, int, MyContext>, IUniversityRepository
    {
        public UniversityRepository(MyContext context) : base(context)
        {
        }

        public IEnumerable<University> SearchByName(string email)
        {
            return GetAll().Where(u => u.Name == email);
        }
    }
}
