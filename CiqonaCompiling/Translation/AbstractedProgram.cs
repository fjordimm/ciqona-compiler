
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

		public string InC()
		{
			StringBuilder sb = new();

			sb.AppendLine("// C code generated from Ciqona code");
			sb.AppendLine("#include <stdio.h>");
			sb.AppendLine("#include <stdlib.h>");
			sb.AppendLine();
			sb.AppendLine("int main(void)");
			sb.AppendLine(mainBlock.InC(0));

			return sb.ToString();
		}
	}
}
