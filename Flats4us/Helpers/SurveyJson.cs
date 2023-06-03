using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    internal class SurveyJson
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type_name { get; set; }
        public string[] answers { get; set; }
    }
}
