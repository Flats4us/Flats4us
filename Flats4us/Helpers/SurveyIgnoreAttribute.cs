using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class SurveyIgnoreAttribute : Attribute
    {
    }
}
