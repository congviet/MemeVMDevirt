using dnlib.DotNet;
using dnlib.DotNet.Emit;
using MemeVMDevirt.Protections.VMInstructions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Context;

namespace MemeVMDevirt.Protections
{
    public static class InitialiseReplace
    {
		public static List<Instruction> _method;
		public static MethodDef _current;
		public static void ReplacePhase()
        {
            foreach (var methodvirt in Stores.MethodVirt)
            {
				_current = methodvirt.Key;
				_method = new List<Instruction>();
				methodvirt.Key.Body.Instructions.Clear();
				foreach(VMInstruction instruction in Stores._methods[methodvirt.Value])
				{
					_method.Add(Reconvert(instruction, instruction.Code));
				}
				foreach (Instruction instruction in _method)
				{
					methodvirt.Key.Body.Instructions.Add(instruction);
				}
			}
        }
		public static Instruction Reconvert(VMInstruction _instruction, VMOpCode vMOpCode)
		{
			switch (vMOpCode)
			{
				case VMOpCode.Add: return Add(_instruction);
				case VMOpCode.Call: return Call(_instruction);
				case VMOpCode.Cgt: return Cgt(_instruction);
				case VMOpCode.Clt: return Clt(_instruction);
				case VMOpCode.Cmp: return Cmp(_instruction);
				case VMOpCode.Dup: return Dup(_instruction);
				case VMOpCode.Int32: return Ldc_I4(_instruction);
				case VMOpCode.Jf: return Brfalse(_instruction);
				case VMOpCode.Jmp: return Br(_instruction);
				case VMOpCode.Jt: return Brtrue(_instruction);
				case VMOpCode.Ldarg: return Ldarg(_instruction);
				case VMOpCode.Ldfld: return Ldfld(_instruction);
				case VMOpCode.Ldloc: return Ldloc(_instruction);
				case VMOpCode.Int64: return Ldc_I8(_instruction);
				case VMOpCode.Newarr: return NewArray(_instruction);
				case VMOpCode.Null: return Null(_instruction);
				case VMOpCode.Pop: return Pop(_instruction);
				case VMOpCode.Ret: return Ret(_instruction);
				case VMOpCode.Stfld: return Stfld(_instruction);
				case VMOpCode.Stloc: return Stloc(_instruction);
				case VMOpCode.String: return Ldstr(_instruction);


				default:
					throw new Exception($"OpCode : {vMOpCode} Not Supported");
			}
		}
		public static Assembly GetReference(short index)
		{
			KeyValuePair<string, Assembly> keyValuePair = Stores._references.ElementAt((int)index);
			bool flag = keyValuePair.Value == null;
			if (flag)
			{
				Stores._references[keyValuePair.Key] = AppDomain.CurrentDomain.Load(new AssemblyName(keyValuePair.Key));
			}
			return Stores._references[keyValuePair.Key];
		}
		public static Instruction Brfalse(VMInstruction instruction)
		{
			return Instruction.Create(OpCodes.Brfalse, _method[(int)instruction.Operand]);
		}
		public static Instruction Brtrue(VMInstruction instruction)
		{
			return Instruction.Create(OpCodes.Brtrue, _method[(int)instruction.Operand]);
		}
		public static Instruction Br(VMInstruction instruction)
		{
			return Instruction.Create(OpCodes.Br, _method[(int)instruction.Operand]);
		}
		public static Instruction Dup(VMInstruction instruction)
		{
			return Instruction.Create(OpCodes.Dup);
		}
		public static Instruction Rem(VMInstruction instruction)
		{
			return Instruction.Create(OpCodes.Rem);
		}
		public static Instruction Pop(VMInstruction instruction)
		{
			return Instruction.Create(OpCodes.Pop);
		}
		public static Instruction Ret(VMInstruction instruction)
		{
			return Instruction.Create(OpCodes.Ret);
		}
		public static Instruction Starg(VMInstruction instruction)
		{
			return Instruction.Create(OpCodes.Starg, (byte)instruction.Operand);
		}
		public static Instruction Cgt(VMInstruction instruction)
		{
			return Instruction.Create(OpCodes.Cgt);
		}
		public static Instruction Cmp(VMInstruction instruction)
		{
			return Instruction.Create(OpCodes.Ceq);
		}
		public static Instruction Clt(VMInstruction instruction)
		{
			return Instruction.Create(OpCodes.Clt);
		}
		public static Instruction Add(VMInstruction instruction)
		{
			return Instruction.Create(OpCodes.Add);
		}
		public static Instruction Div(VMInstruction instruction)
		{
			return Instruction.Create(OpCodes.Div);
		}
		public static Instruction Sub(VMInstruction instruction)
		{
			return Instruction.Create(OpCodes.Sub);
		}
		public static Instruction Mul(VMInstruction instruction)
		{
			return Instruction.Create(OpCodes.Mul);
		}
		public static Instruction Null(VMInstruction instruction)
		{
			return Instruction.Create(OpCodes.Ldnull);
		}
		public static Instruction Ldc_R8(VMInstruction instruction)
		{
			return Instruction.Create(OpCodes.Ldc_R8, (double)instruction.Operand);
		}
		public static Instruction Ldstr(VMInstruction instruction)
		{
			return Instruction.Create(OpCodes.Ldstr, (string)instruction.Operand);
		}
		public static Instruction Ldc_R4(VMInstruction instruction)
		{
			return Instruction.Create(OpCodes.Ldc_R4, (float)instruction.Operand);
		}
		public static Instruction Ldc_I8(VMInstruction instruction)
		{
			return Instruction.Create(OpCodes.Ldc_I8, (long)instruction.Operand);
		}
		public static Instruction Ldc_I4(VMInstruction instruction)
		{
			return Instruction.Create(OpCodes.Ldc_I4, (int)instruction.Operand);
		}
		public static Instruction Call(VMInstruction instruction)
		{
			Tuple<short, int, bool> tuple_Call = (Tuple<short, int, bool>)instruction.Operand;
			Assembly reference_Call = GetReference(tuple_Call.Item1);
			MethodInfo method_call = reference_Call.ManifestModule.ResolveMethod(tuple_Call.Item2) as MethodInfo;
			return Instruction.Create(OpCodes.Call, module.Import(method_call));
		}
		public static Instruction NewArray(VMInstruction instruction)
		{
			Tuple<short, int, bool> tuple_Newarr = (Tuple<short, int, bool>)instruction.Operand;
			Assembly reference_Newarr = GetReference(tuple_Newarr.Item1);
			TypeInfo elementType = reference_Newarr.ManifestModule.ResolveType(tuple_Newarr.Item2) as TypeInfo;
			return Instruction.Create(OpCodes.Newarr, module.Import(elementType));
		}
		public static Instruction Ldloc(VMInstruction instruction)
		{
			short num = (short)instruction.Operand;
			Local locals = _current.Body.Variables[num];
			return new Instruction(OpCodes.Ldloc, locals);
		}
		public static Instruction Stloc(VMInstruction instruction)
		{
			object num = instruction.Operand;//i know than is not that but i'm lazy
			Local locals = _current.Body.Variables.Add(new Local(module.Import(num.GetType()).ToTypeSig()));
			return new Instruction(OpCodes.Stloc, locals);
		}
		public static Instruction Ldarg(VMInstruction instruction)
		{
			short num = (short)instruction.Operand;
			switch (num)
			{
				case 0:
					return new Instruction(OpCodes.Ldarg_0);
				case 1:
					return new Instruction(OpCodes.Ldarg_1);
				case 2:
					return new Instruction(OpCodes.Ldarg_2);
				case 3:
					return new Instruction(OpCodes.Ldarg_3);
				default:
					return new Instruction(OpCodes.Ldarg_S, num);
			}
		}
		public static Instruction Ldfld(VMInstruction instruction)
		{
			Tuple<short, int, bool> tuple_Stfld = (Tuple<short, int, bool>)instruction.Operand;
			Assembly reference_Stfld = GetReference(tuple_Stfld.Item1);
			FieldInfo field_Stfld = reference_Stfld.ManifestModule.ResolveField(tuple_Stfld.Item2);
			return Instruction.Create(OpCodes.Ldfld, module.Import(field_Stfld));
		}
		public static Instruction Stfld(VMInstruction instruction)
		{
			Tuple<short, int, bool> tuple_Stfld = (Tuple<short, int, bool>)instruction.Operand;
			Assembly reference_Stfld = GetReference(tuple_Stfld.Item1);
			FieldInfo field_Stfld = reference_Stfld.ManifestModule.ResolveField(tuple_Stfld.Item2);
			return Instruction.Create(OpCodes.Stfld, module.Import(field_Stfld));
		}
	}
}
