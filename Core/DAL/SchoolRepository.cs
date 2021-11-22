namespace Core.DAL
{
    public class SchoolRepository : IRepository<School>
    {
        public static School school { get; set; }

        public SchoolRepository()
        {
            school = new School();
        }

        public static School GetSchool()
        {
            return school;
        }

        School IRepository<School>.GetSchool()
        {
            return GetSchool();
        }
    }
}