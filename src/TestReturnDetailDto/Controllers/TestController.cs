using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace TestReturnDetailDto.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<PersonBase> Get()
        {
            return new List<PersonBase>()
            {
                new Student("ѧ��A", "ѧ����01"),
                new Employ("ְԱ01", "�ٶ�")
            };
        }
    }

    [JsonDerivedType(typeof(Student))]
    [JsonDerivedType(typeof(Employ))]
    public abstract class PersonBase
    {
        public string Name { get; set; }

        protected PersonBase(string name)
        {
            Name = name;
        }
    }

    public class Student : PersonBase
    {
        public string Number { get; set; }

        public Student(string name, string number) : base(name)
        {
            Number = number;
        }
    }

    public class Employ : PersonBase
    {
        public string CompanyName { get; set; }

        public Employ(string name, string companyName) : base(name)
        {
            CompanyName = companyName;
        }
    }
}