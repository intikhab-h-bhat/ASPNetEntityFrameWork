using Microsoft.EntityFrameworkCore;

namespace ASPNetEntityFrameWork.Data
{
    public class CollegeDBContext:DbContext
    {
        public CollegeDBContext(DbContextOptions<CollegeDBContext> options):base(options)
        {
            
        }

        DbSet<Student> studentcs {  get; set; }
    }
}
