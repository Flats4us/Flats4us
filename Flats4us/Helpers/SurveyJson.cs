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
        public string title { get; set; }
        public string content { get; set; }
        public string typeName { get; set; }
        public string[] answers { get; set; }
    }
}
