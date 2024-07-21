using Core.Concrets;
using Data.UnitOfWork;
using M = Core.Entities;
using Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Command.Teacher
{
    public class DeleteTeacherService
    {
        readonly UnitOfWork _unitOfWork;

        public DeleteTeacherService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteTeacher()
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

        OpinionLabel: Messages.Opinion("teacher", "delete");
            string? input = Console.ReadLine();
            isSucceded = char.TryParse(input, out char choice);

            if (string.IsNullOrWhiteSpace(input) || !isSucceded || !input.IsValidChoice())
            {
                Messages.InvalidInput();
                goto OpinionLabel;
            }

            if (choice.Equals('y'))
            {
                _unitOfWork.Teachers.Delete(teacher);

                _unitOfWork.Commit();

                Messages.SuccessMessage("Teacher", "deleted");
            }
        }
    }
}
