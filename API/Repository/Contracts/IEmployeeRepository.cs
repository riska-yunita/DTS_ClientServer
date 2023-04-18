using API.Models;
using API.ViewModels;
using System.Collections;

namespace API.Repository.Contracts;

public interface IEmployeeRepository : IGeneralRepository<Employee, string>
{
    Task<IEnumerable<EmployeeVM>> GetAllTenure();
    Task<EmployeeVM?> GetTenureId(int id);
}
