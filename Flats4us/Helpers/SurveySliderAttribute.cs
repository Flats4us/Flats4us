using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class SurveySliderAttribute : Attribute
    {
        public int MinimumValue { get; }
        public int MaximumValue { get; }

        public SurveySliderAttribute(int minimumValue, int maximumValue)
        {
            MinimumValue = minimumValue;
            MaximumValue = maximumValue;
        }
    }

}
