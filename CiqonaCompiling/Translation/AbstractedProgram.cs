
using System.IO;
using System.Text;

namespace CiqonaCompiling.Translation
{
	internal class AbstractedProgram
	{
		private readonly CodeBlock mainBlock;

		public AbstractedProgram(CodeBlock mainBlock)
		{
			this.mainBlock = mainBlock;
		}

		public void WriteToC(StreamWriter sw)
		{
			sw.WriteLine("// C code generated from Ciqona code");
			sw.WriteLine("#include <stdio.h>");
			sw.WriteLine("#include <stdlib.h>");
			sw.WriteLine();
			sw.WriteLine("int main(void)");
			mainBlock.WriteToC(sw, 0);
		}
	}
}
