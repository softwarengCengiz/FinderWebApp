using Application.Common;
using Application.Student.Contract;

namespace Application.Student.Interfaces
{
    public interface IStudentService
    {
        Task<bool> AddStudent(StudentDto request);
    }
}
