
namespace CiqonaCompiling.Abstraction
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
			return mainBlock.InC();
		}
	}
}
