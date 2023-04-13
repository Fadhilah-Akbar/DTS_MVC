using DTS_WebApplication.Context;
using DTS_WebApplication.Models;
using DTS_WebApplication.Repository.Contracts;

namespace DTS_WebApplication.Repository.Data
{
    public class RoleRepository : GeneralRepository<Role, int, MyContext>, IRoleRepository
    {
        public RoleRepository(MyContext context) : base(context)
        {
        }
    }
}
