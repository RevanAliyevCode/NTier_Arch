using Core.Concrets;
using Data.UnitOfWork;
using M = Core.Entities;
using Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Command.Group
{
    public class DeleteGroupService
    {
        readonly UnitOfWork _unitOfWork;

        public DeleteGroupService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteGroup()
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

        OpinionLabel: Messages.Opinion("group", "delete");
            string? input = Console.ReadLine();
            isSucceded = char.TryParse(input, out char choice);

            if (string.IsNullOrWhiteSpace(input) || !isSucceded || !input.IsValidChoice())
            {
                Messages.InvalidInput();
                goto OpinionLabel;
            }

            if (choice.Equals('y'))
            {
                _unitOfWork.Groups.Delete(group);

                _unitOfWork.Students.DeleteRange(group.Students);

               _unitOfWork.Commit();

                Messages.SuccessMessage("group", "deleted");
            }
        }
    }
}
