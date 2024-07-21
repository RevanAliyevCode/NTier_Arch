using Core.Concrets;
using Data.UnitOfWork;
using M = Core.Entities;
using Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Command.Student
{
    public class DeleteStudentService
    {
        readonly UnitOfWork _unitOfWork;

        public DeleteStudentService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteStudent()
        {
        IdLable: Messages.InputMessages("student id");
            bool isSucceded = int.TryParse(Console.ReadLine(), out int id);

            if (!isSucceded)
            {
                Messages.InvalidInput();
                goto IdLable;
            }

            M.Student? student = _unitOfWork.Students.Get(id);

            if (student is null)
            {
                Messages.NotFound("Student");
                return;
            }

        OpinionLabel: Messages.Opinion("student", "delete");
            string? input = Console.ReadLine();
            isSucceded = char.TryParse(input, out char choice);

            if (string.IsNullOrWhiteSpace(input) || !isSucceded || !input.IsValidChoice())
            {
                Messages.InvalidInput();
                goto OpinionLabel;
            }

            if (choice.Equals('y'))
            {
                _unitOfWork.Students.Delete(student);

                _unitOfWork.Commit();

                Messages.SuccessMessage("Student", "deleted");
            }
        }
    }
}
