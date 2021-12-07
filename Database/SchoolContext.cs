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
        public DbSet<School> School{ get; set; }
        public DbSet<Homework> Homework{ get; set; }
        
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
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