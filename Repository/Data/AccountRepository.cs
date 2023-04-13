using DTS_WebApplication.Context;
using DTS_WebApplication.Models;
using DTS_WebApplication.Repository.Contracts;
using DTS_WebApplication.ViewModels;

namespace DTS_WebApplication.Repository.Data
{
    public class AccountRepository  : GeneralRepository<Account, string, MyContext>, IAccountRepository
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoleRepository _roleRepository;
        public AccountRepository(MyContext context, IUniversityRepository universityRepository, IEducationRepository educationRepository, IEmployeeRepository employeeRepository, IRoleRepository roleRepository) : base(context)
        {
            _universityRepository = universityRepository;
            _educationRepository = educationRepository;
            _employeeRepository = employeeRepository;
            _roleRepository = roleRepository;
        }

        public int Login(LoginVM loginVM)
        {
            var user = _employeeRepository.GetAll().Where(u => u.Email == loginVM.Email);
            var nik = "";
            if(user.Any())
            {
                foreach(var item in user)
                {
                    if(item.Email == loginVM.Email)
                    {
                        nik = item.NIK; 
                        break;
                    }
                }
                var data = GetAll().Where(u => u.EmployeeNIK == nik);
                foreach(var item in data)
                {
                    if(item.Password == loginVM.Password)
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }

        public int Register(RegisterVM registerVM)
        {
            // Validasi untuk input masing" entitas jika gagal lakukan rollback

            // Validasi apakah input university name ada di database/ tidak
            var transaction = _context.Database.BeginTransaction();
            {
                try
               {
                    var university = new University
                    {
                        Name = registerVM.UniversityName,
                    };

                    var result = _universityRepository.SearchByName(registerVM.UniversityName);
                    if (result.Any())
                    {
                        foreach(var item in result)
                        {
                            if( item.Name == registerVM.UniversityName)
                            {
                                university.Id = item.Id;
                                break;
                            }
                        }
                    }
                    else
                    {
                        _context.Universities.Add(university);
                        _context.SaveChanges();
                    }
                    var educationMajor = _educationRepository.GetAll().Where(u => u.Major == registerVM.Major);
                    var education = new Education
                    {
                        Major = registerVM.Major,
                        Degree = registerVM.Degree,
                        GPA = registerVM.GPA,
                        UniversityId = university.Id,
                    };
                    if (educationMajor.Any())
                    {
                        foreach(var item in educationMajor)
                        {
                            if (item.Major == registerVM.Major)
                            {
                                education.Id = item.Id;
                                break;
                            }
                        }
                    }
                    else
                    {
                        _context.Educations.Add(education);
                        _context.SaveChanges();
                    }                    

                    var employee = new Employee
                    {
                        NIK = registerVM.NIK,
                        FirstName = registerVM.FirstName,
                        LastName = registerVM.LastName,
                        BirthDate = registerVM.BirthDate,
                        Gender = registerVM.Gender,
                        PhoneNumber = registerVM.PhoneNumber,
                        Email = registerVM.Email,
                        HiringDate = DateTime.Now
                    };
                    _context.Employees.Add(employee);
                    _context.SaveChanges();

                    var account = new Account
                    {
                        EmployeeNIK = registerVM.NIK,
                        Password = registerVM.Password,
                    };
                    _context.Accounts.Add(account);
                    _context.SaveChanges();

                    var profiling = new Profiling
                    {
                        EmployeeNIK = registerVM.NIK,
                        EducationId = education.Id,
                    };
                    _context.Profilings.Add(profiling);
                    _context.SaveChanges();
                    
                    var role = _roleRepository.GetAll().Where(u => u.Name.Contains("User"));
                    int roleId = -1 ;
                    foreach(var item in role)
                    {
                        if(item.Name == "User")
                        {
                            roleId = item.Id;
                            break;
                        }
                    }                    

                    var accountRole = new AccountRole
                    {
                        RoleId = roleId,
                        EmployeeNIK = registerVM.NIK,
                    };
                    _context.AccountRoles.Add(accountRole);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    return 0;
               }
            }
            return 1;
        }
    }
}
