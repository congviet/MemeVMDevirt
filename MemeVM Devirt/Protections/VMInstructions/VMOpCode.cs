using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeVMDevirt.Protections.VMInstructions
{
	public enum VMOpCode : byte
	{
		Int32,
		Int64,
		Float,
		Double,
		String,
		Null,
		Add,
		Sub,
		Mul,
		Div,
		Rem,
		Dup,
		Pop,
		Jmp,
		Jt,
		Jf,
		Je,
		Jne,
		Jge,
		Jgt,
		Jle,
		Jlt,
		Cmp,
		Cgt,
		Clt,
		Newarr,
		Ldarg,
		Ldloc,
		Ldfld,
		Ldelem,
		Starg,
		Stloc,
		Stfld,
		Stelem,
		Call,
		Ret,
	}
}
