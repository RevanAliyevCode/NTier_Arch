using Core.Concrets;
using Data.UnitOfWork;
using M = Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Query.Student
{
    public class ShowStudentService
    {
        readonly UnitOfWork _unitOfWork;

        public ShowStudentService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void ShowStudents()
        {
            if (!_unitOfWork.Students.GetAll().Any())
            {
                Console.WriteLine("There is no student");
                return;
            }

            Console.WriteLine($"{"Id",-20} {"Name",-20} {"Surname",-20}");
            foreach (var student in _unitOfWork.Students.GetAll())
            {
                Console.WriteLine($"{student.Id,-20} {student.Name,-20} {student.Surname,-20}");
            }
        }

        public void ShowDetails()
        {
        IdLable: Messages.InputMessages("student id");
            bool isSucceded = int.TryParse(Console.ReadLine(), out int id);

            if (!isSucceded)
            {
                Messages.InvalidInput();
                goto IdLable;
            }

            M.Student? student = _unitOfWork.Students.GetByIdWithGroup(id);

            if (student is null)
            {
                Messages.NotFound("student");
                return;
            }

            Console.WriteLine($"{"Id",-20} {"Name",-20} {"Surname",-20} {"Email",-20} {"Birth Date",-20} {"Group"}");
            Console.WriteLine($"{student.Id,-20} {student.Name,-20} {student.Surname,-20} {student.Email,-20} {student.BirthDate.ToShortDateString(),-20} {student.Group.Name}");
        }
    }
}
