using Core.Concrets;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly CourseDbContext _context;
        public readonly GroupRepo Groups;
        public readonly TeacherRepo Teachers;
        public readonly StudentRepo Students;

        public UnitOfWork()
        {
            _context = new();
            Groups = new(_context);
            Teachers = new(_context);
            Students = new(_context);
        }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Messages.ErrorOcured();
            }
        }
    }
}
