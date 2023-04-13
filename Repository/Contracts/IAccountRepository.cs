using DTS_WebApplication.ViewModels;
using DTS_WebApplication.Models;

namespace DTS_WebApplication.Repository.Contracts
{
    public interface IAccountRepository : IGeneralRepository<Account, string>
    {
        int Register(RegisterVM registerVM);
        int Login(LoginVM loginVM);
    }
}
