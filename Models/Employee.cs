using FluentValidation;

namespace Employee_Management.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public int? DepartmentId { get; set; }
        public string? UserId { get; set; }
        public Department? Department { get; set; }
        public ICollection<Task>? Tasks { get; set; }
    }

    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").Length(0,15);
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress();
        }
    }

    public class EmployeeDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public int? DepartmentId { get; set; }
        public List<TaskDto>? Tasks { get; set; }
    }

}
