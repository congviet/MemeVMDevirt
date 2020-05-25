using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeVMDevirt.Protections
{
    public static class VM
    {
        public static void Execute()
        {
            AnalyseMethod.AnalysePhase();
            AnalyseResources.InitialiseResources();
            InitiliseMethod.InitiliaseMethodage();
            InitialiseReplace.ReplacePhase();
        }
    }
}
