
namespace CiqonaCompiling.Translation
{
	internal abstract class CodeStatement
	{
		public abstract string InC();
	}

	internal class CodeStatementPrint : CodeStatement
	{
		public readonly Expression exp;

		public CodeStatementPrint(Expression exp)
		{
			this.exp = exp;
		}

		public override string InC()
		{
			return exp.InC();
		}
	}
}
