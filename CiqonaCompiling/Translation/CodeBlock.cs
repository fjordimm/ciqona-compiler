
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CiqonaCompiling.Translation
{
	internal class CodeBlock
	{
		private readonly List<CodeStatement> statements;

		public CodeBlock()
		{
			statements = new();
		}

		public void AddCodeStatement(CodeStatement statement)
		{
			statements.Add(statement);
		}

		public void WriteToC(StreamWriter sw, int indent)
		{
			AppendTabs(sw, indent);
			sw.WriteLine("{");

			foreach (CodeStatement statement in statements)
			{
				AppendTabs(sw, indent + 1);
				sw.WriteLine(statement.InC());
			}

			AppendTabs(sw, indent);
			sw.WriteLine("}");
		}

		private static void AppendTabs(StreamWriter sw, int n)
		{
			for (int i = 0; i < n; i++)
			{ sw.Write('\t'); }
		}
	}
}
