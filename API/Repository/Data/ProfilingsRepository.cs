using API.Context;
using API.Models;
using API.Repository;
using API.Repository.Contracts;

namespace API.Repository.Data;

public class ProfilingsRepository : GeneralRepository<Profiling, string, MyContext>, IProfilingsRepository
{
    public ProfilingsRepository(MyContext myContext) : base(myContext)
    {

    }
}