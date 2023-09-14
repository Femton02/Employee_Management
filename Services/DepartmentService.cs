using AutoMapper;
using Employee_Management.Models;
using Employee_Management.UnitofWork;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management.Services
{

    [Serializable]
    public class NotFoundException : Exception
    {
        public int StatusCode { get; set; } = 404;
        public NotFoundException() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, Exception inner) : base(message, inner) { }
        protected NotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<DepartmentDto> CreateDepartment(DepartmentDto departmentDto)
        {
            var departments = await _unitOfWork.Departments.GetAllAsync();
            if (departments.Any(d => d.Name == departmentDto.Name))
            {
                throw new Exception("Department already exists");
            }
            var department = _mapper.Map<Department>(departmentDto);
            await _unitOfWork.Departments.AddAsync(department);
            await _unitOfWork.CompleteAsync();
            return departmentDto;
        }

        public async Task<DepartmentDto> DeleteDepartment(int id)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id) ?? throw new NotFoundException("Department not found");
            _unitOfWork.Departments.Remove(department);
            await _unitOfWork.CompleteAsync();
            var departmentdto = _mapper.Map<DepartmentDto>(department);
            return departmentdto;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartments()
        {
            var departments = await _unitOfWork.Departments.GetAllAsync() ?? throw new Exception("No departments found");
            var departmentsDto = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
            return departmentsDto;
        }

        public async Task<DepartmentDto> GetDepartmentById(int id)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id) ?? throw new NotFoundException("Department not found");
            var departmentDto = _mapper.Map<DepartmentDto>(department);
            return departmentDto;
        }

        public async Task<DepartmentDto> UpdateDepartment(DepartmentDto departmentDto)
        {
            var updatemodel = _mapper.Map<Department>(departmentDto);
            _unitOfWork.Departments.Update(updatemodel);
            await _unitOfWork.CompleteAsync();
            return departmentDto;
        }
    }
}
