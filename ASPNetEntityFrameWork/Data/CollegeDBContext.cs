using Microsoft.EntityFrameworkCore;

namespace ASPNetEntityFrameWork.Data
{
    public class CollegeDBContext : DbContext
    {
        //public CollegeDBContext(DbContextOptions<CollegeDBContext> options):base(options)
        public CollegeDBContext(DbContextOptions dbContxtOptions) : base(dbContxtOptions)
        {

        }

        public DbSet<Student> studentcs { get; set; }
    }
}
