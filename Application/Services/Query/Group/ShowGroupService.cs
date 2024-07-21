using Core.Concrets;
using Data.UnitOfWork;
using M = Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Query.Group
{
    public class ShowGroupService
    {
        readonly UnitOfWork _unitOfWork;

        public ShowGroupService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void ShowGroups()
        {
            if (!_unitOfWork.Groups.GetAll().Any())
            {
                Console.WriteLine("There is no course");
                return;
            }

            Console.WriteLine($"{"Id",-20} {"Name",-20} {"Limit",-20} Begin Date");
            foreach (var group in _unitOfWork.Groups.GetAll())
            {
                Console.WriteLine($"{group.Id,-20} {group.Name,-20} {group.Limit,-20} {group.BeginDate.ToShortDateString()}");
            }
        }

        public void ShowDetails()
        {
        IdLable: Messages.InputMessages("Group id");
            bool isSucceded = int.TryParse(Console.ReadLine(), out int id);

            if (!isSucceded)
            {
                Messages.InvalidInput();
                goto IdLable;
            }

            M.Group? group = _unitOfWork.Groups.GetByIdWithAll(id);

            if (group is null)
            {
                Messages.NotFound("Group");
                return;
            }

            Console.WriteLine($"{"Id",-20} {"Name",-20} {"Limit",-20} {"Teacher",-20} {"Begin Date",-20} {"End Date",-20} {"Students"}");
            Console.Write($"{group.Id,-20} {group.Name,-20} {group.Limit,-20} {group.Teacher?.Name ?? "No teacher",-20} {group.BeginDate.ToShortDateString(),-20} {group.EndDate.ToShortDateString(),-20}");

            if (group.Students.Count == 0)
                Console.WriteLine("No student");

            for (int i = 0; i < group.Students.Count; i++)
            {
                if (i == 0)
                {
                    Console.WriteLine($"{group.Students.ToList()[i].Name}");
                    continue;
                }
                Console.WriteLine($"{group.Students.ToList()[i].Name,130}");
            }

        }
    }
}
