using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System.Collections.Generic;
using System.Linq;
using static Context;

namespace MemeVMDevirt.Protections
{
    public static class AnalyseMethod
    {
        public static void AnalysePhase()
        {
			foreach (TypeDef type in (from x in module.Types where x.HasMethods select x).ToArray())
			{
				foreach (MethodDef method in (from x in type.Methods where x.HasBody && x.Body.HasInstructions select x).ToArray())
				{
					IList<Instruction> Instr = method.Body.Instructions;
					if (Instr[0].OpCode == OpCodes.Ldtoken && Instr[1].IsLdcI4() && Instr[Instr.Count - 3].OpCode == OpCodes.Call && Instr[Instr.Count - 3].Operand.ToString().Contains("Run"))
					{
						Stores.MethodVirt.Add(method, Instr[1].GetLdcI4Value());
					}
				}
			}
		}
    }
}
