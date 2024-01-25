
using System.Collections.Generic;
using System.Text;

namespace CiqonaCompiling.Abstraction
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

		public string InC()
		{
			StringBuilder sb = new();

			sb.AppendLine("{");
			foreach (CodeStatement statement in statements)
			{ sb.AppendLine(statement.InC()); }
			sb.AppendLine("}");

			return sb.ToString();
		}
	}
}
