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
            return await StudentClasses
            .Include("Students")
            .ToListAsync();
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

        public async Task AddNewStudent(string className, Student student)
        {
            Students.Add(student);
            var classForChange = FindClass(className);
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
                newClass.className = newClass.getClassName();
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
            return await Students.FindAsync((long)int.Parse(studentId));
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
            return await Teachers.FindAsync((long)int.Parse(teacherId));
        }
        public async Task<Teacher> GetTeacher(string username, string password)
        {
            var teacher = await Teachers.FirstAsync(t => t.name == username);

            if(teacher == null || !teacher.Password.Equals(password))
            {
                
                return null;
            }
            else
            {
                return teacher;
            }
            
        }

        public async Task<List<Homework>> GetHomeworkForTeacher(long teacherId)
        {
            var teacher = await Teachers.FindAsync(teacherId);
            List<Homework> homeworkByTeacher = Homework.Where(homework => homework.teacher == teacher).ToList();

            return await Task<List<Homework>>.Run(() => homeworkByTeacher);
        }

        public async Task AddHomework(Dictionary<string, string> homeworkData, string teacherId)
        {
            Util util = new Util();
            var teacher = Teachers.FindAsync((long) int.Parse(teacherId)).Result;
            var homework = new Homework(FindClass(homeworkData["studentClass"]).Result,
                util.checkSubject(homeworkData["subject"]), homeworkData["desc"], teacher);
            teacher.AddHomeWork(homework);
            Homework.Add(homework);
            Teachers.Update(teacher);
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

        public async Task UpdateHomework(Dictionary<string, string> data)
        {
            var homework = Homework.FindAsync(data["homeworkId"]).Result;
            homework.description = data["desc"];
            Homework.Update(homework);
            await SaveChangesAsync();
        }

        public async Task SaveEmailTeacher(string teacherId, Dictionary<string, string> address)
        {
            Teacher teacher = Teachers.FindAsync(teacherId).Result;
            teacher.Email = address["email"];
            Teachers.Update(teacher);
            await SaveChangesAsync();
        }

        public async Task SavePhoneNumberTeacher(string teacherId, Dictionary<string, string> number)
        {
            Teacher teacher = Teachers.FindAsync(teacherId).Result;
            teacher.Email = number["phone"];
            Teachers.Update(teacher);
            await SaveChangesAsync();
        }

        public async Task UpdateTeacher(string teacherId, Dictionary<string, string> data)
        {
            var teacher = Teachers.FindAsync(teacherId).Result;
            teacher.name = data["name"];
            teacher.DateOfBirth = DateTime.Parse(data["birthDate"]);
            Teachers.Update(teacher);
            await SaveChangesAsync();
        }

        public async Task UpdateMark(Dictionary<string, string> markData)
        {
            Util util = new Util();

            var mark = Mark.FindAsync(markData["markId"]).Result;
            mark.Subject = util.checkSubject(markData["subject"]);
            mark.Weight = util.checkMarkweight(markData["weight"]);
            mark.MarkValue = int.Parse(markData["value"]);

            Mark.Update(mark);
            await SaveChangesAsync();
        }

        public async Task DeleteMark(string markId)
        {
            var mark = Mark.FindAsync(markId.LongCount()).Result;
            Mark.Remove(mark);
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
