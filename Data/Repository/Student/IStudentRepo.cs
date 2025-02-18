﻿using Core.Entities;
using Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IStudentRepo : IRepository<Student>
    {
        Student? GetByIdWithGroup(int id);
    }
}
