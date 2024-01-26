
using System.Collections.Generic;
using System.IO;

namespace CiqonaCompiling.Translation.Abstraction
{
	internal class Block
	{
		private readonly List<Statement> statements;

		public Block()
		{
			statements = new();
		}

		public void AddCodeStatement(Statement statement)
		{
			statements.Add(statement);
		}

		public void WriteToC(StreamWriter sw, int indent)
		{
			AppendTabs(sw, indent);
			sw.WriteLine("{");

			foreach (Statement statement in statements)
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
