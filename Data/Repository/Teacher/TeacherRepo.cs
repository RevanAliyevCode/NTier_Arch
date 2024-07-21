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
    public class TeacherRepo : Repository<Teacher>, ITeacherRepo
    {
        readonly CourseDbContext _context;
        public TeacherRepo(CourseDbContext context) : base(context)
        {
            _context = context;
        }

        public Teacher? GetByIdWithGroup(int id)
        {
            return _context.Teachers.Include(x => x.Groups).FirstOrDefault(x => x.Id == id);
        }
    }
}
