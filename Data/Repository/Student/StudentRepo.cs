using Core.Entities;
using Data.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class StudentRepo : Repository<Student>, IStudentRepo
    {
        readonly CourseDbContext _context;
        public StudentRepo(CourseDbContext context) : base(context)
        {
            _context = context;
        }

        public Student? GetByIdWithGroup(int id)
        {
            return _context.Students.Include(x => x.Group).FirstOrDefault(x => x.Id == id);
        }
    }
}
