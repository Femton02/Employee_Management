using FluentValidation;
namespace Employee_Management.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }

    public class TaskValidator : AbstractValidator<Task>
    {
        public TaskValidator()
        {
            RuleFor(x => x.EmployeeId).NotEmpty().WithMessage("EmployeeId is required");
            RuleFor(x => x.Description).Length(0, 200);
        }
    }

    public class TaskDto
    {
        public string? Description { get; set; }
        public int EmployeeId { get; set; }
    }
}
