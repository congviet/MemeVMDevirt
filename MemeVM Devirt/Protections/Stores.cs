using dnlib.DotNet;
using dnlib.DotNet.Emit;
using MemeVMDevirt.Protections.VMInstructions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MemeVMDevirt.Protections
{
    public static class Stores
    {
        public static Stream Resources_Virt;
        public static Dictionary<MethodDef, int> MethodVirt = new Dictionary<MethodDef, int>();
        public static Dictionary<string, Assembly> _references = new Dictionary<string, Assembly>();
        public static List<List<VMInstruction>> _methods = new List<List<VMInstruction>>();
    }
}
