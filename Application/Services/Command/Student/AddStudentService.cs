using Core.Concrets;
using Data.UnitOfWork;
using M = Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Command.Student
{
    public class AddStudentService
    {
        readonly UnitOfWork _unitOfWork;

        public AddStudentService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddStudent()
        {
        IdLable: Messages.InputMessages("Group id");
            bool isSucceded = int.TryParse(Console.ReadLine(), out int id);

            if (!isSucceded)
            {
                Messages.InvalidInput();
                goto IdLable;
            }

            M.Group? group = _unitOfWork.Groups.GetByIdWithStudents(id);

            if (group is null)
            {
                Messages.NotFound("Group");
                return;
            }

            if (group.Students.Count == group.Limit)
            {
                Messages.NoSpace();
                return;
            }

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

        EmailLabel: Messages.InputMessages("email");
            string? email = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(email))
            {
                Messages.InvalidInput();
                goto EmailLabel;
            }

        BirthDateLabel: Messages.InputMessages("birth date (dd.MM.yyyy student must be 18 or older)");
            isSucceded = DateTime.TryParse(Console.ReadLine(), out DateTime birthDate);
            if (!isSucceded)
            {
                Messages.InvalidInput();
                goto BirthDateLabel;
            }

            if (DateTime.Now.Year - birthDate.Year < 18)
            {
                Console.WriteLine("Student too young for the course");
                goto BirthDateLabel;
            }

            M.Student student = new()
            {
                Name = name,
                Surname = surname,
                Email = email,
                BirthDate = birthDate,
                GroupId = id
            };

            _unitOfWork.Students.Add(student);

            _unitOfWork.Commit();

            Messages.SuccessMessage("student", "created");
        }
    }
}
