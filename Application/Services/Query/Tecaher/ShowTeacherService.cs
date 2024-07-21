using Core.Concrets;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using M = Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Query.Tecaher
{
    public class ShowTeacherService
    {
        readonly UnitOfWork _unitOfWork;

        public ShowTeacherService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void ShowTeachers()
        {
            if (!_unitOfWork.Teachers.GetAll().Any())
            {
                Console.WriteLine("There is no teacher");
                return;
            }

            Console.WriteLine($"{"Id",-20} {"Name",-20} {"Surname",-20}");
            foreach (var teacher in _unitOfWork.Teachers.GetAll())
            {
                Console.WriteLine($"{teacher.Id,-20} {teacher.Name,-20} {teacher.Surname,-20}");
            }
        }

        public void ShowDetails()
        {
        IdLable: Messages.InputMessages("Teacher id");
            bool isSucceded = int.TryParse(Console.ReadLine(), out int id);

            if (!isSucceded)
            {
                Messages.InvalidInput();
                goto IdLable;
            }

            M.Teacher? teacher = _unitOfWork.Teachers.GetByIdWithGroup(id);

            if (teacher is null)
            {
                Messages.NotFound("Teacher");
                return;
            }
            Console.WriteLine($"{"Id",-20} {"Name",-20} {"Surname",-20} {"Groups"}");
            Console.Write($"{teacher.Id,-20} {teacher.Name,-20} {teacher.Surname,-20}");

            if (teacher.Groups.Count == 0)
                Console.WriteLine("No group");

            for (int i = 0; i < teacher.Groups.Count; i++)
            {
                if (i == 0)
                {
                    Console.WriteLine($"{teacher.Groups.ToList()[i].Name}");
                    continue;
                }
                Console.WriteLine($"{teacher.Groups.ToList()[i].Name,70}");
            }
        }
    }
}
