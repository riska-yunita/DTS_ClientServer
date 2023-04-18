using API.Context;
using API.Models;
using API.Repository;
using API.Repository.Contracts;

namespace API.Repository.Data;

public class AccountsRolesRepository : GeneralRepository<AccountRole, int, MyContext>, IAccountRolesRepository
{
    private IRoleRepository _roleRepository;
    private IAccountRolesRepository _accountRolesRepository;        
    private IEmployeeRepository _employeeRepository;

    public AccountsRolesRepository(
    	MyContext myContext,
    	IRoleRepository roleRepository, IAccountRolesRepository accountRolesRepository, IEmployeeRepository employeeRepository) : base(myContext)
    {
    	_roleRepository = roleRepository;
        _accountRolesRepository = accountRolesRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<string>> GetRolesByNikAsync(string nik)
    {
    	var getAcccountRoleByAccountNik = GetAllAsync().Result.Where(x=>x.AccountNik == nik);
    	var getRole = await _roleRepository.GetAllAsync();

    	var getRoleByNik = from ar in getAcccountRoleByAccountNik
    						join r in getRole on ar.RoleId equals r.Id
    						select r.Name;
    	return getRoleByNik;
    }

}