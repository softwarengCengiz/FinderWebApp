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
        public async Task<Guid> AddStudent(StudentDto request)
        {
            try
            {
                var student = new Domain.Student.Student
                {
                    Id = new Guid(),
                    UserId = request.UserId,
                    University = request.University,
                    Department = request.Department,
                    CreatedDate = System.DateTime.Now,
                };

                await context.Students.AddAsync(student);

                await context.SaveChangesAsync();

                return student.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Guid> UpdateStudent(StudentDto request)
        {
            try
            {
                var student = context.Students.FirstOrDefault(x => x.UserId == request.UserId);
                if (student != null)
                {
                    student.Department = request.Department;
                    student.University = request.University;

                    context.Students.Update(student);
                    context.SaveChanges();
                }

                return request.UserId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}