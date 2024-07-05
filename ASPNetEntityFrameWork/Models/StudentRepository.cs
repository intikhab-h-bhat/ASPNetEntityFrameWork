namespace ASPCoreWebApi.Models
{
    public static class StudentRepository
    {

        public static List<Student> students = new List<Student>() {


                new Student
                {
                    Id = 1,
                    Name = "Intikhab",
                    Address = "Address 1",
                    Email = "Abc@gmail.com"
                },
                new Student
                {
                    Id = 2,
                    Name = "Student 2",
                    Address = "Address 2",
                    Email = "Abc2@gmail.com"
                }
        };

    }
}
