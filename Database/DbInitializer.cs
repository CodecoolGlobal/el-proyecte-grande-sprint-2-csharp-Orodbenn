using System;
using System.Linq;

namespace Database
{
    public static class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            context.Database.EnsureCreated();

            context.SaveChanges();
        }
    }
}
