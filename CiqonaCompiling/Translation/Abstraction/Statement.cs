
namespace CiqonaCompiling.Translation.Abstraction
{
	internal abstract class Statement
	{
		public abstract string InC();
	}

	internal class StatementPrint : Statement
	{
		public readonly Expression exp;

		public StatementPrint(Expression exp)
		{
			this.exp = exp;
		}

		public override string InC()
		{
			return $"printf({exp.InC()});";
		}
	}
}
