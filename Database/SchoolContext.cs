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
            return Task<List<StudentClass>>.Run(() => StudentClasses.ToListAsync()).Result;
        }

        public Task<StudentClass> GetStudentClass(string studentClass)
        {
            return Task<StudentClass>.Run(() => FindClass(studentClass));
        }

        public Task ChangeClassRoom(string studentClass, int room)
        {
            var classForChange = FindClass(studentClass);
            classForChange.Result.classRoom = room;
            var task = Task.Run(() =>
            {
                StudentClasses.Update(classForChange.Result);
                SaveChangesAsync();
            });
            return task;
        }

        public Task AddNewStudentToClass(string studentClass, Student student)
        {
            var classForChange = FindClass(studentClass);
            //classForChange.Result.addStudent(student);
            var task = Task.Run(() =>
            {
                StudentClasses.Update(classForChange.Result);
                SaveChangesAsync();
            });
            return task;
        }

        private Task<StudentClass> FindClass(string studentClass)
        {
            long classId = 0;
            foreach (var sClass in StudentClasses)
            {
                if (sClass.getClassName() == studentClass)
                {
                    classId = sClass.ID;
                }
            }
            return Task.Run(() => StudentClasses.FindAsync(classId).Result);
        }

        public async Task AddNewClasses(int numberOfClasses)
        {
            const int asciiForA = 65;
            for (var i = 0; i < numberOfClasses; i++)
            {
                var newClass = new StudentClass((Convert.ToChar(asciiForA + i)).ToString());
                StudentClasses.Add(newClass);
            }
            await SaveChangesAsync();
        }

        public Task EndOfYear()
        {
            foreach (var studentClass in StudentClasses)
            {
                if (studentClass.grade >= 12)
                {
                    StudentClasses.Remove(studentClass);
                }

                studentClass.yearPassing();
                StudentClasses.Update(studentClass);
            }
            return Task.Run(() => SaveChangesAsync());
        }
        
        /* methods for StudentController */

        public Task<Student> GetStudent(string studentId)
        {
            return Task<Student>.Run(() => FindStudent(studentId));
        }

        /*
         * {
         *    "oldClassId": {string},
         *    "newClassId": {string}
         * }
         */
        public Task ChangeClassOfStudent(string studentId, Dictionary<string, string> dict)
        {
            var student = Students.Find(studentId);
            var oldClass = FindClass(dict["oldClassId"]).Result;
            var newClass = FindClass(dict["newClassId"]).Result;
            
            //newClass.addStudent(student);
            //oldClass.removeStudent(student);
            return Task.Run(() => SaveChangesAsync());
        }

        public Task AddParentOfStudent(string studentId, Dictionary<string, string> dict)
        {
            Student student = FindStudent(studentId).Result;
            Parent parent = new Parent(dict["name"], student);
            student.AddParent(parent);
            return Task.Run(() => Students.Update(student));
        }

        public Task<HashSet<Parent>> GetParents(string studentId)
        {
            return Task<HashSet<Parent>>.Run(() => FindStudent(studentId).Result.parents);
        }


        private Task<Student> FindStudent(string studentId)
        {
            return Task<Student>.Run(() => Students.Find(studentId));
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