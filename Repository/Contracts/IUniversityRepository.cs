using DTS_WebApplication.Models;

namespace DTS_WebApplication.Repository.Contracts
{
    public interface IUniversityRepository : IGeneralRepository<University, int>
    {
       IEnumerable<University> SearchByName(string email);
    }
}
