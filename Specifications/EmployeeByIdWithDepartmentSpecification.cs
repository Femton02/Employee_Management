using Employee_Management.Models;

namespace Employee_Management.Specifications
{
    public class EmployeeByIdWithDepartmentSpecification : BaseSpecifcation<Employee>
    {
        public EmployeeByIdWithDepartmentSpecification(int? employeeId) : base(e => e.Id == employeeId)
        {
            AddInclude(e => e.Department);
            AddInclude(d => d.Department.Employees);
        }
    }
}
