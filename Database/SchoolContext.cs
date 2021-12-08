using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Marks;
using Core.Users;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Database
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Parent> Parent { get; set; }
        public DbSet<Mark> Mark { get; set; }
        public DbSet<StudentClass> StudentClasses { get; set; }
        public DbSet<School> School { get; set; }
        public DbSet<Homework> Homework { get; set; }

        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        /* methods for SchoolController */

        public async Task<List<StudentClass>> GetStudentClasses()
        {
            return await StudentClasses.ToListAsync();
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

            newClass.addStudent(student);
            oldClass.removeStudent(student);
            await SaveChangesAsync();
        }

        public async Task AddParentOfStudent(string studentId, Dictionary<string, string> dict)
        {
            Student student = FindStudent(studentId).Result;
            Parent parent = new Parent(dict["name"], student);
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