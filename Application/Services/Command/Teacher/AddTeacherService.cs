using Core.Concrets;
using Data.UnitOfWork;
using M = Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Command.Teacher
{
    public class AddTeacherService
    {
        readonly UnitOfWork _unitOfWork;

        public AddTeacherService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddTeacher()
        {
        NameLabel: Messages.InputMessages("name");
            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Messages.InvalidInput();
                goto NameLabel;
            }

        SurnameNameLabel: Messages.InputMessages("surname");
            string? surname = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(surname))
            {
                Messages.InvalidInput();
                goto SurnameNameLabel;
            }

            M.Teacher teacher = new() { Name = name, Surname = surname };
            _unitOfWork.Teachers.Add(teacher);

            _unitOfWork.Commit();
            
            Messages.SuccessMessage("Teacher", "added");
        }
    }
}
