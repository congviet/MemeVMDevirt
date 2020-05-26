using MemeVMDevirt.Protections.VMInstructions;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace MemeVMDevirt.Protections
{
	public static class InitiliseMethod
	{
		public static void InitiliaseMethodage()
		{
			using (DeflateStream deflateStream = new DeflateStream(Stores.Resources_Virt, CompressionMode.Decompress))
			{
				using (BinaryReader binaryReader = new BinaryReader(deflateStream))
				{
					int num = binaryReader.ReadInt32();
					for (int i = 0; i < num; i++)
					{
						int count = binaryReader.ReadInt32();
						Stores._references.Add(Encoding.UTF8.GetString(binaryReader.ReadBytes(count)), null);
					}
					int num2 = binaryReader.ReadInt32();
					for (int j = 0; j < num2; j++)
					{
						int num3 = binaryReader.ReadInt32();
						List<VMInstruction> list = new List<VMInstruction>();
						for (int k = 0; k < num3; k++)
						{
							VMOpCode code = (VMOpCode)binaryReader.ReadByte();
							list.Add(Map(code, binaryReader));
						}
						Stores._methods.Add(list);
					}
				}
			}
		}
		public static VMInstruction Map(VMOpCode vMOpCode, BinaryReader binaryReader)
		{
			switch (vMOpCode)
			{
				case VMOpCode.Add: return Add(binaryReader);
				case VMOpCode.Call: return Call(binaryReader);
				case VMOpCode.Cgt: return Cgt(binaryReader);
				case VMOpCode.Clt: return Clt(binaryReader);
				case VMOpCode.Cmp: return Cmp(binaryReader);
				case VMOpCode.Dup: return Dup(binaryReader);
				case VMOpCode.Int32: return Int32(binaryReader);
				case VMOpCode.Jf: return Jf(binaryReader);
				case VMOpCode.Jmp: return Jmp(binaryReader);
				case VMOpCode.Jt: return Jt(binaryReader);
				case VMOpCode.Ldarg: return Ldarg(binaryReader);
				case VMOpCode.Ldfld: return Ldfld(binaryReader);
				case VMOpCode.Ldloc: return Ldloc(binaryReader);
				case VMOpCode.Int64: return Int64(binaryReader);
				case VMOpCode.Newarr: return NewArray(binaryReader);
				case VMOpCode.Null: return Null(binaryReader);
				case VMOpCode.Pop: return Pop(binaryReader);
				case VMOpCode.Ret: return Ret(binaryReader);
				case VMOpCode.Stfld: return Stfld(binaryReader);
				case VMOpCode.Stloc: return Stloc(binaryReader);
				case VMOpCode.String: return Ldstr(binaryReader);


				default:
					throw new Exception($"OpCode : {vMOpCode} Not Supported");
			}
		}
		public static VMInstruction Add(BinaryReader binaryReader)
		{
			return new VMInstruction(VMOpCode.Add);
		}
		public static VMInstruction Call(BinaryReader binaryReader)
		{
			return new VMInstruction(VMOpCode.Call, new Tuple<short, int, bool>(binaryReader.ReadInt16(), binaryReader.ReadInt32(), binaryReader.ReadBoolean()));
		}
		public static VMInstruction Cgt(BinaryReader binaryReader)
		{
			return new VMInstruction(VMOpCode.Cgt, null);
		}
		public static VMInstruction Clt(BinaryReader binaryReader)
		{
			return new VMInstruction(VMOpCode.Clt, null);
		}
		public static VMInstruction Cmp(BinaryReader binaryReader)
		{
			return new VMInstruction(VMOpCode.Cmp, null);
		}
		public static VMInstruction Dup(BinaryReader binaryReader)
		{
			return new VMInstruction(VMOpCode.Dup, null);
		}
		public static VMInstruction Pop(BinaryReader binaryReader)
		{
			return new VMInstruction(VMOpCode.Pop, null);
		}
		public static VMInstruction Ret(BinaryReader binaryReader)
		{
			return new VMInstruction(VMOpCode.Ret, null);
		}
		public static VMInstruction Null(BinaryReader binaryReader)
		{
			return new VMInstruction(VMOpCode.Null, null);
		}
		public static VMInstruction Int32(BinaryReader binaryReader)
		{
			return new VMInstruction(VMOpCode.Int32, binaryReader.ReadInt32());
		}
		public static VMInstruction Jf(BinaryReader binaryReader)
		{
			return new VMInstruction(VMOpCode.Jf, binaryReader.ReadInt32());
		}
		public static VMInstruction Jmp(BinaryReader binaryReader)
		{
			return new VMInstruction(VMOpCode.Jmp, binaryReader.ReadInt32());
		}
		public static VMInstruction Jt(BinaryReader binaryReader)
		{
			return new VMInstruction(VMOpCode.Jt, binaryReader.ReadInt32());
		}
		public static VMInstruction Ldarg(BinaryReader binaryReader)
		{
			return new VMInstruction(VMOpCode.Ldarg, binaryReader.ReadInt16());
		}
		public static VMInstruction Ldfld(BinaryReader binaryReader)
		{
			return new VMInstruction(VMOpCode.Ldfld, new Tuple<short, int>(binaryReader.ReadInt16(), binaryReader.ReadInt32()));
		}
		public static VMInstruction Stfld(BinaryReader binaryReader)
		{
			return new VMInstruction(VMOpCode.Stfld, new Tuple<short, int>(binaryReader.ReadInt16(), binaryReader.ReadInt32()));
		}
		public static VMInstruction Ldloc(BinaryReader binaryReader)
		{
			return new VMInstruction(VMOpCode.Ldloc, binaryReader.ReadInt16());
		}
		public static VMInstruction Stloc(BinaryReader binaryReader)
		{
			return new VMInstruction(VMOpCode.Stloc, binaryReader.ReadInt16());
		}
		public static VMInstruction Int64(BinaryReader binaryReader)
		{
			return new VMInstruction(VMOpCode.Int64, binaryReader.ReadInt64());
		}
		public static VMInstruction Ldstr(BinaryReader binaryReader)
		{
			int count = binaryReader.ReadInt32();
			return new VMInstruction(VMOpCode.String, Encoding.UTF8.GetString(binaryReader.ReadBytes(count)));
		}
		public static VMInstruction NewArray(BinaryReader binaryReader)
		{
			return new VMInstruction(VMOpCode.Newarr, new Tuple<short, int>(binaryReader.ReadInt16(), binaryReader.ReadInt32()));
		}
	}
}
