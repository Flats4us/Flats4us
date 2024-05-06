using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    internal class SurveyJson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Trigger { get; set; }
        public bool Optional { get; set; }
        public string TypeName { get; set; }
        public string[] Answers { get; set; }
    }
}
