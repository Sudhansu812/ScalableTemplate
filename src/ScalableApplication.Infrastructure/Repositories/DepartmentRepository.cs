using ScalableApplication.Application.Interfaces.Repositories;
using ScalableApplication.Domain.Entities;
using ScalableApplication.Infrastructure.Persistence;

namespace ScalableApplication.Infrastructure.Repositories
{
    public class DepartmentRepository(AppDbContext db) : BaseRepository<Department>(db), IDepartmentRepository
    {

    }
}
