using Employee_Management.Models;
namespace Employee_Management.Repositories
{
    public class TaskRepository : Repository<Models.Task>, ITaskRepository
    {
        public TaskRepository(EmployeeContext context) : base(context)
        {
        }
    }
}
