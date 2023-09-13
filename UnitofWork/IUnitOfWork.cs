using Employee_Management.Repositories;

namespace Employee_Management.UnitofWork
{
    public interface IUnitOfWork : IDisposable
    {
        IDepartmentRepository Departments { get; }
        IEmployeeRepository Employees { get; }
        ITaskRepository Tasks { get; }
        Task<int> CompleteAsync();
    }
}
