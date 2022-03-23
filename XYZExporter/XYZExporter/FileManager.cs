using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZExporter
{
    public class FileManager
    {

        private List<string> availableStudies = new List<string>();
        private List<Student> studentsWithWrongData = new List<Student>();
        private List<Student> studentsWithProperData = new List<Student>();

        public void ReadAllLines(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    var values = line.Split(',');

                    var student = new Student()
                    {
                        FirstName = values[0],
                        LastName = values[1],
                        Studies = new Studies()
                        {
                            Name = values[2],
                            Mode = values[3],
                        },
                        Index = values[4],
                        BirthDate = values[5],
                        Email = values[6],
                        MotherName = values[7],
                        FatherName = values[8]
                    };
                    

                    if (student.IsValid() && !studentsWithProperData.Any(s => s.Equals(student)))
                    {
                        studentsWithProperData.Add(student);
                        if (!availableStudies.Any(s => s == student.Studies?.Name))
                            availableStudies.Add(student.Studies?.Name);
                    }
                    else
                    {
                        studentsWithWrongData.Add(student);
                    }
                }
            }
        }


        public bool SaveToFile(string resultPath)
        {
            var activeStudies = new List<ActiveStudies>();
            foreach (string activeStudy in availableStudies)
            {
                int count = studentsWithProperData.Where(student => student.Studies?.Name == activeStudy).Count();
                activeStudies.Add(new ActiveStudies
                {
                    Name=activeStudy,
                    NumberOfStudents = count
                });
            }


            Logger.Log(studentsWithWrongData);

            using (StreamWriter sw = new StreamWriter(resultPath + "/resultJson.txt"))
            {
               sw.WriteLine(JsonConvert.SerializeObject(new StudiesResponse
                   {
                       CreatedAt = DateTimeOffset.Now.ToString(),
                       Students = studentsWithProperData,
                       Author = "Mateusz Wolniarski",
                       ActiveStudies = activeStudies
                   }
               ));
            }

            return true;
        }
    }
}
