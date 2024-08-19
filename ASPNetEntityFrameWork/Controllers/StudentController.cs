//using ASPCoreWebApi.Models;
using ASPCoreWebApi.Models;
using ASPNetEntityFrameWork.Data;
using ASPNetEntityFrameWork.Models;
using ASPNetEntityFrameWork.Requests;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ASPCoreWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly CollegeDBContext _collegeDbContext;

        public StudentController(CollegeDBContext collegeDbContext)
        {
            _collegeDbContext = collegeDbContext;
        }



        //public static List<Student> students = new List<Student>()
        //{

        //        new Student
        //        {
        //            Id = 1,
        //            Name = "Student 1",
        //            Address = "Address 1",
        //            Email = "Abc@gmail.com"
        //        },
        //        new Student
        //        {
        //            Id = 2,
        //            Name = "Student 2",
        //            Address = "Address 2",
        //            Email = "Abc2@gmail.com"
        //        }

        //};

        [HttpGet]
        [Route("All", Name = "GetAllStudents")]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<StudentDTO>> Getstudents()
        {
            //var studentsdto =new List<StudentDTO>();
            //foreach(var item in _collegeDbContext.studentcs)
            //{
            //    StudentDTO obj = new StudentDTO()
            //    {
            //        Id= item.Id,
            //        Name = item.Name,
            //        Address = item.Address,
            //        Email = item.Email       
            //    };
            //    studentsdto.Add(obj);

            //}
            //OK -200 status
            // return Ok(studentsdto);

            var students = _collegeDbContext.studentcs.Select(s => new StudentDTO()
            {
                Id = s.Id,
                Name = s.Name,
                Address = s.Address,
                Email = s.Email,
                DOB = s.DOB

            });
            //OK -200 status
            return Ok(students);

        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<StudentDTO> getallstudent(int id) {

            if (id <= 0)
            {
                // Badrequest -400 status
                return BadRequest("The id cannot be 0 or less than 0");
            }

            var student = _collegeDbContext.studentcs.FirstOrDefault(x => x.Id == id);
            if (student == null)
                //NotFound -404 status
                return NotFound($"The student id {id} you are looking doesnot exist");

            var studentdto = new StudentDTO()
            {
                Id = student.Id,
                Name = student.Name,
                Address = student.Address,
                Email = student.Email,
                DOB = student.DOB
            };
            //// Ok -200 status
            return Ok(studentdto);


        }

        [HttpGet("{name:alpha}")]
        public ActionResult<StudentDTO> getallstudent(string name)
        {
            if (name == null)
            {
                return NotFound("The");
            }

            var student = _collegeDbContext.studentcs.FirstOrDefault(n => n.Name == name);
            if (student == null)
                //NotFound -404 status
                return NotFound($"The student name {name} you are looking doesnot exist");

            var studentdto = new StudentDTO()
            {
                Id = student.Id,
                Name = student.Name,
                Address = student.Address,
                Email = student.Email,
                DOB = student.DOB
            };

            //// OK -200 status
            //return Ok(StudentRepository.students.FirstOrDefault(x => x.Name == name));

            return Ok(studentdto);
        }


        [HttpPost]
        [ProducesResponseType(typeof(Student), 201)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StudentDTO> CreateStudent(StudentDTO request)
        {
            if (request == null)
            {
                return BadRequest("Please enter data");
            }
            //Custom Validation
            if (request.DOB > DateTime.Now)
            {
                ModelState.AddModelError("DOB Error", "DOB should be less than current date");
                return BadRequest(ModelState);

            }

            Student student = new()
            {
                Address = request.Address,
                Email = request.Email,
                Name = request.Name,
                DOB = request.DOB
            };

            _collegeDbContext.studentcs.Add(student);
            _collegeDbContext.SaveChanges();

            return CreatedAtAction("getallstudent", new { id = student.Id }, student);
        }


        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateStudent(StudentDTO request)
        {
            if (request == null || request.Id < 0)
                BadRequest();

            var existingStudent = _collegeDbContext.studentcs.Where(s => s.Id == request.Id).FirstOrDefault();

            if (existingStudent == null)
                return NotFound();

            existingStudent.Name = request.Name;
            existingStudent.Email = request.Email;
            existingStudent.DOB = request.DOB;

            _collegeDbContext.studentcs.Update(existingStudent);
            _collegeDbContext.SaveChanges();

            return NoContent();

        }


        [HttpPatch]
        [Route("UpdateStuPartially/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateStudentPartialy(int id,JsonPatchDocument<StudentDTO> request)
        {
            if (request == null || id < 0)
                BadRequest();

            var existingStudent = _collegeDbContext.studentcs.Where(s => s.Id == id).FirstOrDefault();

            if (existingStudent == null)
                return NotFound();
            var studentdto = new StudentDTO
            {
                Id = existingStudent.Id,
                Name = existingStudent.Name,
                Email = existingStudent.Email,
                DOB = existingStudent.DOB

            };
             request.ApplyTo(studentdto,ModelState);
            if(ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            existingStudent.Name = studentdto.Name;
            existingStudent.Email = studentdto.Email;
            existingStudent.DOB = studentdto.DOB;

            _collegeDbContext.studentcs.Update(existingStudent);
            _collegeDbContext.SaveChanges();

            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var delstudent = _collegeDbContext.studentcs.FirstOrDefault(x => x.Id == id);
            _collegeDbContext.studentcs.Remove(delstudent);
            _collegeDbContext.SaveChanges();
            return NoContent();
        }

        //[HttpPut("{id}")]
        //public IActionResult Update(int id, StudentRequest request)
        //{
        //    var updateStudent = _collegeDbContext.studentcs.FirstOrDefault(x => x.Id == id);
        //    updateStudent.Address = request.Address;
        //    updateStudent.Email = request.Email;
        //    updateStudent.Name = request.Name;
        //    updateStudent.DOB = request.DOB;

        //    _collegeDbContext.studentcs.Update(updateStudent);
        //    _collegeDbContext.SaveChanges();

        //    return Ok(updateStudent);
        //}
    }
}

