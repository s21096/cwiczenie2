using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZExporter
{
    public class Student
    {
       /* 
        indexNumber: "s1234",
        fname: "Jan",
        lname: "Kowalski",
        birthdate: "02.05.1980",
        email: "kowalski@wp.pl",
        mothersName: "Alina",
        fathersName: "Jan",
        studies: {
            name: "Computer Science",
            mode: "Dzienne"
        }
       */

        public string Index { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string BirthDate { get; set; }

        public string Email { get; set; }

        public string MotherName { get; set; }

        public string FatherName { get; set; }

        public Studies Studies { get; set; }


        public bool IsValid()
        {
            return !(FirstName is null ||
                    LastName is null ||
                    BirthDate is null ||
                    FatherName is null ||
                    MotherName is null ||
                    Email is null ||
                    Studies?.Name is null ||
                    Studies?.Mode is null ||
                    Index is null);
        }

        public bool Equals(Student other)
        {
            return this.Index == other.Index;
        }
    }
}
