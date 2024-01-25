
namespace CiqonaCompiling.Abstraction
{
	internal abstract class Expression
	{
		public abstract string InC();
	}

	internal class ExpressionStringLiteral : Expression
	{
		private readonly string contents;

		public ExpressionStringLiteral(string contents)
		{
			this.contents = contents;
		}

		public override string InC()
		{
			return $"\"{contents}\"";
		}
	}
}
