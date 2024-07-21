// See https://aka.ms/new-console-template for more information

using Application.Services.Command.Group;
using Application.Services.Command.Student;
using Application.Services.Command.Teacher;
using Application.Services.Query.Group;
using Application.Services.Query.Student;
using Application.Services.Query.Tecaher;
using Core.Concrets;
using Data.UnitOfWork;

UnitOfWork unitOfWork = new();
AddTeacherService addTeacherService = new(unitOfWork);
ShowTeacherService showTeacherService = new(unitOfWork);
UpdateTeacherService updateTeacherService = new(unitOfWork);
DeleteTeacherService deleteTeacherService = new(unitOfWork);
AddGroupService addGroupService = new(unitOfWork, showTeacherService);
ShowGroupService showGroupService = new(unitOfWork);
UpdateGroupService updateGroupService = new(unitOfWork, showTeacherService);
DeleteGroupService deleteGroupService = new(unitOfWork);
AddStudentService addStudentService = new(unitOfWork);
ShowStudentService showStudentsService = new(unitOfWork);
UpdateStudentService updateStudentService = new(unitOfWork, showGroupService);
DeleteStudentService deleteStudentService = new(unitOfWork);


while (true)
{
    Console.WriteLine("-----MENU-----");
    Console.WriteLine("0.Exit");
    Console.WriteLine("1.Add Teacher");
    Console.WriteLine("2.Show Teachers");
    Console.WriteLine("3.Show details of teacher");
    Console.WriteLine("4.Update Teacher");
    Console.WriteLine("5.Delete Teacher");
    Console.WriteLine("6.Add Group");
    Console.WriteLine("7.Show Groups");
    Console.WriteLine("8.Show details of group");
    Console.WriteLine("9.Update Group");
    Console.WriteLine("10.Delete Group");
    Console.WriteLine("11.Add Student");
    Console.WriteLine("12.Show Students");
    Console.WriteLine("13.Show details of student");
    Console.WriteLine("14.Update Student");
    Console.WriteLine("15.Delete Student");

    Console.Write("Write your choice: ");
    bool canConverte = int.TryParse(Console.ReadLine(), out int choice);


    if (canConverte)
    {
        switch ((Operations)choice)
        {
            case Operations.Exit:
                return;
            case Operations.AddTeacher:
                addTeacherService.AddTeacher();
                break;
            case Operations.ShowTeachers:
                showTeacherService.ShowTeachers();
                break;
            case Operations.ShowDetailsTeacher:
                showTeacherService.ShowDetails();
                break;
            case Operations.UpdateTeacher:
                showTeacherService.ShowTeachers();
                updateTeacherService.UpdateTeacher();
                break;
            case Operations.DeleteTeacher:
                showTeacherService.ShowTeachers();
                deleteTeacherService.DeleteTeacher();
                break;
            case Operations.AddGroup:
                addGroupService.AddGroup();
                break;
            case Operations.ShowGroups:
                showGroupService.ShowGroups();
                break;
            case Operations.ShowDetailsGroup:
                showGroupService.ShowDetails();
                break;
            case Operations.UpdateGroups:
                showGroupService.ShowGroups();
                updateGroupService.UpdateGroup();
                break;
            case Operations.DeleteGroup:
                showGroupService.ShowGroups();
                deleteGroupService.DeleteGroup();
                break;
            case Operations.AddStudent:
                showGroupService.ShowGroups();
                addStudentService.AddStudent();
                break;
            case Operations.ShowStudents:
                showStudentsService.ShowStudents();
                break;
            case Operations.ShowDetailsStudents:
                showStudentsService.ShowDetails();
                break;
            case Operations.UpdateStudent:
                showStudentsService.ShowStudents();
                updateStudentService.UpdateStudent();
                break;
            case Operations.DeleteStudent:
                showStudentsService.ShowStudents();
                deleteStudentService.DeleteStudent();
                break;
            default:
                Console.WriteLine("There is not a option like that");
                break;
        }
    }
    else
        Messages.InvalidInput();
}