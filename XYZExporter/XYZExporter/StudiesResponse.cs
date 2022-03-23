using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYZExporter
{
    public class StudiesResponse
    {
        public string CreatedAt { get; set; }
        public string Author { get; set; }
        public List<Student> Students { get; set; }
        public List<ActiveStudies> ActiveStudies  { get; set; }
    }
}
