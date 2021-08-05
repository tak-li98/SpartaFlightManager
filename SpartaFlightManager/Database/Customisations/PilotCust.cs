using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public partial class Pilot
    {
        public override string ToString()
        {
            return $"{Title} {FirstName} {LastName}";
        }
    }
}
