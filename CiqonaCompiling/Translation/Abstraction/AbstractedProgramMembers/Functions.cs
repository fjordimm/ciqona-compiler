
using System.Collections.Generic;

namespace CiqonaCompiling.Translation.Abstraction.AbstractedProgramMembers
{
	internal class Functions
	{
		private readonly Dictionary<string, Function> dict;
		private uint numClientFuncs;

		public Functions()
		{
			this.dict = new();
			this.numClientFuncs = 0;
		}
	}

	internal class Function
	{
		public readonly Datatype returnType;
	}
}
