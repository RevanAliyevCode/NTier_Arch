using Core.Entities;
using Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IGroupRepo : IRepository<Group>
    {
        Group? GetByName(string name);
        Group? GetByIdWithStudents(int id);
        Group? GetByIdWithTeacher(int id);
        Group? GetByIdWithAll(int id);
    }
}
