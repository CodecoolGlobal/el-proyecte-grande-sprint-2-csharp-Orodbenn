using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Marks;
using Core.Users;
using Core.Utils;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Database
{
    public class SchoolContext : DbContext
    {
        public DbSet<StudentClass> StudentClasses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Homework> Homework { get; set; }

        public DbSet<Mark> Mark { get; set; }
        //public DbSet<School> School { get; set; }

        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        /* methods for SchoolController */

        public async Task<List<StudentClass>> GetStudentClasses()
        {
            return await StudentClasses.ToListAsync();
        }

        public async Task DeleteUser(string userId)
        {
            string userType = userId.Substring(0, 1);
            switch (userType)
            {
                case "S":
                    var student = Students.FindAsync(userId).Result;
                    Students.Remove(student);
                    break;
                case "P":
                    var parent = Parents.FindAsync(userId).Result;
                    Parents.Remove(parent);
                    break;
                case "T":
                    var teacher = Teachers.FindAsync(userId).Result;
                    Teachers.Remove(teacher);
                    break;
            }

            await SaveChangesAsync();
        }

        public async Task<List<Mark>> GetMarks(string studentId)
        {
            Student student = Students.FindAsync(studentId).Result;
            return await Task<List<Mark>>.Run(() => student.marks);
        }

        public async Task<StudentClass> GetStudentClass(string studentClass)
        {
            return await FindClass(studentClass);
        }

        public async Task ChangeClassRoom(string studentClass, int room)
        {
            var classForChange = FindClass(studentClass);
            classForChange.Result.classRoom = room;
            StudentClasses.Update(classForChange.Result);
            await SaveChangesAsync();
        }

        public async Task AddNewStudentToClass(string studentClass, Student student)
        {
            var classForChange = FindClass(studentClass);
            student.AssignId(classForChange.Result.classIdentifier);
            classForChange.Result.addStudent(student);
            StudentClasses.Update(classForChange.Result);
            await SaveChangesAsync();
        }

        public async Task AddNewClasses(int numberOfClasses)
        {
            const int asciiForA = 65;
            for (var i = 0; i < numberOfClasses; i++)
            {
                var classIdentifier = Convert.ToChar(asciiForA + i).ToString().ToLower();
                var newClass = new StudentClass(9)
                {
                    classIdentifier = classIdentifier
                };
                StudentClasses.Add(newClass);
            }

            await SaveChangesAsync();
        }

        public async Task EndOfYear()
        {
            foreach (var studentClass in StudentClasses)
            {
                if (studentClass.grade >= 12)
                {
                    StudentClasses.Remove(studentClass);
                }

                studentClass.grade += 1;
                StudentClasses.Update(studentClass);
            }

            await SaveChangesAsync();
        }

        /* methods for StudentController */

        public async Task<Student> GetStudent(string studentId)
        {
            return await FindStudent(studentId);
        }

        /*
         * {
         *    "oldClassId": {string},
         *    "newClassId": {string}
         * }
         */
        public async Task ChangeClassOfStudent(string studentId, Dictionary<string, string> dict)
        {
            var student = Students.FindAsync(studentId).Result;
            var oldClass = FindClass(dict["oldClassId"]).Result;
            var newClass = FindClass(dict["newClassId"]).Result;

            student.AssignId(newClass.classIdentifier);
            newClass.addStudent(student);
            oldClass.removeStudent(student);
            await SaveChangesAsync();
        }

        public async Task<List<Homework>> GetHomework(string studentId)
        {
            return await Task.Run(() => StudentClasses.FindAsync(FindStudent(studentId).Result).Result.Homework);
        }

        public async Task SaveEmail(string studentId, Dictionary<string, string> address)
        {
            Student student = FindStudent(studentId).Result;
            student.Email = address["email"];
            Students.Update(student);
            await SaveChangesAsync();
        }

        public async Task SavePhoneNumber(string studentId, Dictionary<string, string> number)
        {
            Student student = FindStudent(studentId).Result;
            student.Email = number["phone"];
            Students.Update(student);
            await SaveChangesAsync();
        }

        public async Task UpdateStudent(string studentId, Dictionary<string, string> data)
        {
            var student = FindStudent(studentId);
            student.Result.name = data["name"];
            student.Result.DateOfBirth = DateTime.Parse(data["birthDate"]);
            Students.Update(student.Result);
            await SaveChangesAsync();
        }

        public async Task SaveEmailParent(string parentId, Dictionary<string, string> address)
        {
            Parent parent = Parents.FindAsync(parentId).Result;
            parent.Email = address["email"];
            Parents.Update(parent);
            await SaveChangesAsync();
        }

        public async Task SavePhoneNumberParent(string parentId, Dictionary<string, string> number)
        {
            Parent parent = Parents.FindAsync(parentId).Result;
            parent.Email = number["phone"];
            Parents.Update(parent);
            await SaveChangesAsync();
        }

        public async Task UpdateStudentParent(string studentId, Dictionary<string, string> data)
        {
            var student = FindStudent(studentId);
            student.Result.name = data["name"];
            student.Result.DateOfBirth = DateTime.Parse(data["birthDate"]);
            Students.Update(student.Result);
            await SaveChangesAsync();
        }

        public async Task AddParentOfStudent(string studentId, Dictionary<string, string> dict)
        {
            Student student = FindStudent(studentId).Result;
            Parent parent = new Parent(dict["name"], student);
            parent.AssignId(student);
            student.AddParent(parent);
            Students.Update(student);
            await SaveChangesAsync();
        }

        public async Task<HashSet<Parent>> GetParents(string studentId)
        {
            return await Task<HashSet<Parent>>.Run(() => FindStudent(studentId).Result.parents);
        }


        private async Task<Student> FindStudent(string studentId)
        {
            return await Task<Student>.Run(() => Students.FindAsync(studentId).Result);
        }

        private async Task<StudentClass> FindClass(string studentClass)
        {
            long classId = 0;
            foreach (var sClass in StudentClasses)
            {
                if (sClass.getClassName() == studentClass)
                {
                    classId = sClass.ID;
                }
            }

            return await StudentClasses.FindAsync(classId);
        }

        /* methods for TeacherController */

        public async Task<List<Teacher>> GetAllTeachers()
        {
            return await Teachers.ToListAsync();
        }

        public async Task AddTeacher(Teacher teacher)
        {
            Teachers.Add(teacher);
            teacher.AssignId();
            await SaveChangesAsync();
        }

        public async Task<Teacher> GetTeacherById(string teacherId)
        {
            return await Teachers.FindAsync(teacherId);
        }

        public async Task<List<Homework>> GetHomeworkForTeacher(string teacherId)
        {
            var teacher = await Teachers.FindAsync(teacherId);
            List<Homework> homeworkByTeacher = Homework.Where(homework => homework.teacher == teacher).ToList();

            return await Task<List<Homework>>.Run(() => homeworkByTeacher);
        }

        public async Task AddHomework(Homework homework, string teacherId)
        {
            Homework.Add(homework);
            Teachers.FindAsync(teacherId).Result.AddHomeWork(homework);
            await SaveChangesAsync();
        }

        public async Task AddMark(Dictionary<string, string> markData)
        {
            Util util = new Util();
            
            var student = FindStudent(markData["studentId"]).Result;
            var teacher = Teachers.FindAsync(markData["teacherId"]).Result;
            var mark = new Mark(int.Parse(markData["value"]), teacher, util.checkSubject(markData["value"]),
                util.checkMarkweight(markData["weight"]));
            
            student.AddMark(mark);
            Students.Update(student);
            await SaveChangesAsync();
        }

        /*
        public DbSet<Student> Students { get; set; }
        public DbSet<Room> Rooms { get; set; }
    
    
        public async Task AddRoom(Room room)
        {
            this.Rooms.Add(room);
            await SaveChangesAsync();
        }
    
        public Task<Room> GetRoom(long roomId)
        {
            var findRoom = Task<Room>.Run(() => Rooms.FindAsync(roomId).Result);
            return findRoom;
        }*/
    }
}