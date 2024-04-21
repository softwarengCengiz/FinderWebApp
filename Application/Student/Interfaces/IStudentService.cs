using Application.Common;
using Application.Student.Contract;

namespace Application.Student.Interfaces
{
    public interface IStudentService
    {
        Task<Guid> AddStudent(StudentDto request);
        Task<Guid> UpdateStudent(StudentDto request);
    }
}
