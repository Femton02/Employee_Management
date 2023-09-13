using FluentValidation;
using System.ComponentModel.DataAnnotations.Schema;
namespace Employee_Management.Models
{
    public class Department
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int? ManagerID { get; set; }
        public Employee? Manager { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }

    public class DepartmentValidator : AbstractValidator<Department>
    {
        public DepartmentValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").Length(0,50);
            RuleFor(x => x.Description).Length(0, 200);
        }
    }

    public class DepartmentDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int? ManagerID { get; set; }
    }
}
