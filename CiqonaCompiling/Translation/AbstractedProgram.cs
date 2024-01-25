
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
			return mainBlock.InC(1);
		}
	}
}