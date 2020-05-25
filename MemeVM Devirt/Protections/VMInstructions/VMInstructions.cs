using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeVMDevirt.Protections.VMInstructions
{
	public struct VMInstruction
	{
		internal VMInstruction(VMOpCode code, object op = null)
		{
			this.Code = code;
			this.Operand = op;
		}
		internal VMOpCode Code;
		internal object Operand;
	}
}
