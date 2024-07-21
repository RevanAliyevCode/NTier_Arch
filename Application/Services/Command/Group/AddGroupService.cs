using Application.Services.Query.Tecaher;
using Core.Concrets;
using Data.UnitOfWork;
using M = Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Command.Group
{
    public class AddGroupService
    {
        readonly UnitOfWork _unitOfWork;
        readonly ShowTeacherService _showTeacherService;

        public AddGroupService(UnitOfWork unitOfWork, ShowTeacherService showTeacherService)
        {
            _unitOfWork = unitOfWork;
            _showTeacherService = showTeacherService;
        }

        public void AddGroup()
        {
        NameLabel: Messages.InputMessages("name");
            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Messages.InvalidInput();
                goto NameLabel;
            }

            var group = _unitOfWork.Groups.GetByName(name);

            if (group is not null)
            {
                Messages.Exist("Group", name);
                goto NameLabel;
            }

        LimitLabel: Messages.InputMessages("limit");
            bool isSucceded = int.TryParse(Console.ReadLine(), out int limit);

            if (!isSucceded)
            {
                Messages.InvalidInput();
                goto LimitLabel;
            }

        BeginDateLabel: Messages.InputMessages("begin date (dd.MM.yyyy)");
            isSucceded = DateTime.TryParse(Console.ReadLine(), out DateTime beginDate);

            if (!isSucceded)
            {
                Messages.InvalidInput();
                goto BeginDateLabel;
            }

        EndDateLabel: Messages.InputMessages("end date (dd.MM.yyyy)");
            isSucceded = DateTime.TryParse(Console.ReadLine(), out DateTime endDate);

            if (!isSucceded || beginDate.AddMonths(6) > endDate)
            {
                Messages.InvalidInput();
                goto EndDateLabel;
            }

            _showTeacherService.ShowTeachers();
        TeacherIdLabel: Messages.InputMessages("teacher id");
            isSucceded = int.TryParse(Console.ReadLine(), out int teacherId);

            if (!isSucceded)
            {
                Messages.InvalidInput();
                goto TeacherIdLabel;
            }

            var teacher = _unitOfWork.Teachers.Get(teacherId);

            if (teacher is null)
            {
                Messages.NotFound("teacher");
                goto TeacherIdLabel;
            }

            M.Group newGroup = new()
            {
                Name = name,
                Limit = limit,
                BeginDate = beginDate,
                EndDate = endDate,
                TeacherId = teacherId,
            };

            _unitOfWork.Groups.Add(newGroup);

            _unitOfWork.Commit();

            Messages.SuccessMessage("group", "added");
        }
    }
}
