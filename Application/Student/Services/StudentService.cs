using Application.Student.Contract;
using Application.Student.Interfaces;
using Infrastructure.Data;

namespace Application.Student.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext context;

        public StudentService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> AddStudent(StudentDto request)
        {
            try
            {
                var student = new Domain.Student.Student
                {
                    Id = new Guid(),
                    Name = request.Name,
                    Lastname = request.LastName,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    University = request.University,
                    Department = request.Department,
                    CreatedDate = System.DateTime.Now,
                };

                await context.AddAsync(student);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}