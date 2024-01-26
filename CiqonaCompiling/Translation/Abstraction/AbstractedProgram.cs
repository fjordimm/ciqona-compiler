
using System.IO;
using CiqonaCompiling.Translation.Abstraction.AbstractedProgramMembers;

namespace CiqonaCompiling.Translation.Abstraction
{
	internal class AbstractedProgram
	{
		public readonly Block mainBlock;
		public readonly Datatypes datatypes;

		public AbstractedProgram(Block mainBlock)
		{
			this.mainBlock = mainBlock;
			this.datatypes = new();
		}

		public void WriteToC(StreamWriter sw)
		{
			sw.WriteLine("// C code generated from Ciqona code");
			sw.WriteLine("#include <stdio.h>");
			sw.WriteLine("#include <stdlib.h>");
			sw.WriteLine("#include <stdint.h>");
			sw.WriteLine();
			sw.WriteLine("int main(void)");
			mainBlock.WriteToC(sw, 0);
		}
	}
}
