
using System.Collections.Generic;
using System.Linq;
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

		public string InC(int indent)
		{
			StringBuilder sb = new();

			sb.AppendLine($"{Enumerable.Repeat("\t", indent)}{{");
			foreach (CodeStatement statement in statements)
			{ sb.AppendLine(statement.InC()); }
			sb.AppendLine($"{Enumerable.Repeat("\t", indent)}}}");

			return sb.ToString();
		}
	}
}
