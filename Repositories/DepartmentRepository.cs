using Employee_Management.Models;
namespace Employee_Management.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(EmployeeContext context) : base(context)
        {
        }
    }
}
