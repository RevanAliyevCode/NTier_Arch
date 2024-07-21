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
    public class GroupRepo : Repository<Group>, IGroupRepo
    {
        readonly CourseDbContext _context;
        public GroupRepo(CourseDbContext context) : base(context)
        {
            _context = context;
        }

        public Group? GetByIdWithAll(int id)
        {
            return _context.Groups.Include(x => x.Students).Include(x => x.Teacher).FirstOrDefault(x => x.Id == id);
        }

        public Group? GetByIdWithStudents(int id)
        {
            return _context.Groups.Include(x => x.Students).FirstOrDefault(x => x.Id == id);
        }

        public Group? GetByIdWithTeacher(int id)
        {
            return _context.Groups.Include(x => x.Teacher).FirstOrDefault(x => x.Id == id);
        }

        public Group? GetByName(string name)
        {
            return _context.Groups.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
        }
    }
}
