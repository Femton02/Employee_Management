using Employee_Management.Models;
using AutoMapper;

namespace Employee_Management
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore());
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentDto, Department>();
            CreateMap<Models.Task, TaskDto>();
            CreateMap<TaskDto, Models.Task>();
        }
    }
}