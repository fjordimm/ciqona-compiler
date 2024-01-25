
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

			AppendTabs(sb, indent);
			sb.AppendLine("{");

			foreach (CodeStatement statement in statements)
			{
				AppendTabs(sb, indent + 1);
				sb.AppendLine(statement.InC());
			}

			AppendTabs(sb, indent);
			sb.AppendLine("}");

			return sb.ToString();
		}

		private static void AppendTabs(StringBuilder sb, int n)
		{
			for (int i = 0; i < n; i++)
			{
				sb.Append('\t');
			}
		}
	}
}
