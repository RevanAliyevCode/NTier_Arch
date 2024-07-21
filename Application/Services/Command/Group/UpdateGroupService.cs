using Application.Services.Query.Tecaher;
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
    public class UpdateGroupService
    {
        readonly UnitOfWork _unitOfWork;
        readonly ShowTeacherService _showTeacherService;

        public UpdateGroupService(UnitOfWork unitOfWork, ShowTeacherService showTeacherService)
        {
            _unitOfWork = unitOfWork;
            _showTeacherService = showTeacherService;
        }

        public void UpdateGroup()
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

        OpinionLimitLabel: Messages.Opinion("limit", "change");
            input = Console.ReadLine();
            isSucceded = char.TryParse(input, out choice);

            if (string.IsNullOrWhiteSpace(input) || !isSucceded || !input.IsValidChoice())
            {
                Messages.InvalidInput();
                goto OpinionLimitLabel;
            }

            int newLimit = 0;

            if (choice.Equals('y'))
            {
            NewLimitLabel: Messages.InputMessages("new limit");
                isSucceded = int.TryParse(Console.ReadLine(), out newLimit);

                if (!isSucceded || newLimit < group.Students.Count)
                {
                    Messages.InvalidInput();
                    goto NewLimitLabel;
                }
            }

        OpinionTeacherLabel: Messages.Opinion("teacher", "change");
            input = Console.ReadLine();
            isSucceded = char.TryParse(input, out choice);

            if (string.IsNullOrWhiteSpace(input) || !isSucceded || !input.IsValidChoice())
            {
                Messages.InvalidInput();
                goto OpinionTeacherLabel;
            }

            int newTeacherId = 0;

            if (choice.Equals('y'))
            {
                _showTeacherService.ShowTeachers();
            NewTeacherLabel: Messages.InputMessages("new teacher id");
                isSucceded = int.TryParse(Console.ReadLine(), out newTeacherId);

                if (!isSucceded)
                {
                    Messages.InvalidInput();
                    goto NewTeacherLabel;
                }

                if (_unitOfWork.Teachers.Get(newTeacherId) is null)
                {
                    Messages.NotFound("Teacher");
                    goto NewTeacherLabel;
                }
            }

            DateTime newBeginDate = default;
            DateTime newEndDate = default;

        OpinionBeginLabel: Messages.Opinion("begin date", "change");
            input = Console.ReadLine();
            isSucceded = char.TryParse(input, out choice);

            if (string.IsNullOrWhiteSpace(input) || !isSucceded || !input.IsValidChoice())
            {
                Messages.InvalidInput();
                goto OpinionBeginLabel;
            }


            if (choice.Equals('y'))
            {
            NewBeginLabel: Messages.InputMessages("new begin date");
                isSucceded = DateTime.TryParse(Console.ReadLine(), out newBeginDate);

                if (!isSucceded)
                {
                    Messages.InvalidInput();
                    goto NewBeginLabel;
                }
            }

        OpinionEndLabel: Messages.Opinion("end date", "change");
            input = Console.ReadLine();
            isSucceded = char.TryParse(input, out choice);

            if (string.IsNullOrWhiteSpace(input) || !isSucceded || !input.IsValidChoice())
            {
                Messages.InvalidInput();
                goto OpinionEndLabel;
            }


            if (choice.Equals('y'))
            {
            NewEndLabel: Messages.InputMessages("new end date");
                isSucceded = DateTime.TryParse(Console.ReadLine(), out newEndDate);

                if (!isSucceded)
                {
                    Messages.InvalidInput();
                    goto NewEndLabel;
                }
            }


            if (newName != "")
                group.Name = newName;

            if (newLimit != 0)
                group.Limit = newLimit;

            if (newTeacherId != 0)
                group.TeacherId = newTeacherId;

            if (newBeginDate != default)
            {
                if (newBeginDate.AddMonths(6) > (newEndDate != default ? newEndDate : group.EndDate))
                    goto OpinionBeginLabel;
                group.BeginDate = newBeginDate;
            }

            if (newEndDate != default)
            {
                if ((newBeginDate != default ? newBeginDate : group.BeginDate) > newEndDate)
                    goto OpinionEndLabel;
                group.EndDate = newEndDate;
            }

            _unitOfWork.Groups.Update(group);

            _unitOfWork.Commit();

            Messages.SuccessMessage("Group", "updated");
        }
    }
}
