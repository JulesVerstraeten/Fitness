using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessRunningBL.Interface
{
    public interface IFileProcessor
    {
        List<string[]> ReadRunningStats(string fileName);
    }

    
}
