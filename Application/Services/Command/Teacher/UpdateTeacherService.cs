using Data.UnitOfWork;
using M = Core.Entities;
using Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Concrets;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Command.Teacher
{
    public class UpdateTeacherService
    {
        readonly UnitOfWork _unitOfWork;

        public UpdateTeacherService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void UpdateTeacher()
        {
        IdLable: Messages.InputMessages("Teacher id");
            bool isSucceded = int.TryParse(Console.ReadLine(), out int id);

            if (!isSucceded)
            {
                Messages.InvalidInput();
                goto IdLable;
            }

            M.Teacher? teacher = _unitOfWork.Teachers.Get(id);

            if (teacher is null)
            {
                Messages.NotFound("Teacher");
                return;
            }

        OpinionLabel: Messages.Opinion("name", "change");
            string? input = Console.ReadLine();
            isSucceded = char.TryParse(input, out char choice);

            if (string.IsNullOrWhiteSpace(input) || !isSucceded || !input.IsValidChoice())
            {
                Messages.InvalidInput();
                goto OpinionLabel;
            }

            string? newName = "";

            if (choice.Equals('y'))
            {
            NewNameLabel: Messages.InputMessages("new name");
                newName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(newName))
                {
                    Messages.InvalidInput();
                    goto NewNameLabel;
                }
            }

        OpinionSurnameLabel: Messages.Opinion("surname", "change");
            input = Console.ReadLine();
            isSucceded = char.TryParse(input, out choice);

            if (string.IsNullOrWhiteSpace(input) || !isSucceded || !input.IsValidChoice())
            {
                Messages.InvalidInput();
                goto OpinionSurnameLabel;
            }

            string? newSurname = "";

            if (choice.Equals('y'))
            {
            NewSurnameLabel: Messages.InputMessages("new surname");
                newSurname = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(newSurname))
                {
                    Messages.InvalidInput();
                    goto NewSurnameLabel;
                }
            }

            if (newName != "")
                teacher.Name = newName;

            if (newSurname != "")
                teacher.Surname = newSurname;

            _unitOfWork.Teachers.Update(teacher);

            _unitOfWork.Commit();

            Messages.SuccessMessage("Tecaher", "updated");
        }
    }
}
