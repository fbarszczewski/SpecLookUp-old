using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecLookUp.Converters
{
    public static class StringConverter
    {
        public static string NewLineToSlash(string input)
        {
            if(input.Contains(Environment.NewLine))
                return input.Replace(Environment.NewLine, " / ");
            else
                return input;
        }

    }
}
