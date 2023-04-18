using API.Models;
using API.Repository.Contracts;
using API.Repository;
using API.ViewModels;
using API.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Repository.Data;

public class EmployeeRepository : GeneralRepository<Employee, string, MyContext>, IEmployeeRepository
{

    private readonly IProfilingsRepository _profilingsRepository;
    private readonly IEducationRepository _educationRepository;

    public EmployeeRepository(
       MyContext myContext,
        IProfilingsRepository profilingsRepository,
        IEducationRepository educationRepository
        ) : base(myContext) 
    {
        _profilingsRepository = profilingsRepository;
        _educationRepository = educationRepository;
    }
   
    public async Task<IEnumerable<EmployeeVM>> GetAllTenure()
    {
        var getEmployees = await GetAllAsync();
        var getProfilings = await _profilingsRepository.GetAllAsync();
        var getEducations = await _educationRepository.GetAllAsync();

        IEnumerable<EmployeeVM> employees = getEmployees
                        .Join(getProfilings,
                            e => e.Nik,
                            p => p.EmployeeNik,
                            (e, p) => new
                            {
                                JobTenure = DateTime.Today.Year - e.HiringDate.Year,
                                p.EducationId,
                                p.EmployeeNik,e,p
                            })
                        .Join(getEducations,
                            ep => ep.EducationId,
                            edu => edu.Id,
                            (ep, edu) => new EmployeeVM
                            {
                                EmployeeNIK = ep.e.Nik,
                                EmployeeName = ep.e.FirstName +" "+ep.e.LastName,
                                JobTenure = ep.JobTenure,
                                Degrees = edu.Degree
                            })
                       .Where(ep => ep.JobTenure > 5)
                       .OrderByDescending(ep => ep.JobTenure);
        return employees;
    }

    public async Task<EmployeeVM?> GetTenureId(int id)
    {
        return await _context.Set<EmployeeVM>().FindAsync(id);
    }

    public async Task<string> GetFullNameByEmailAsync(string email)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
        return employee is null 
            ? string.Empty
            : string.Concat(employee.FirstName + " " + employee.LastName);
    }
}