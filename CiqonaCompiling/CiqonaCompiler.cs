
using System;
using System.Collections.Generic;
using System.Linq;
using CiqonaCompiling.Translation;
using CiqonaCompiling.Parsing;

namespace CiqonaCompiling
{
	public static class CiqonaCompiler
	{
		internal const bool EnableCompilerPrintingWalkthrough = true;

		public static string Compile(IEnumerable<string> ciqonaScripts)
		{
			if (EnableCompilerPrintingWalkthrough) Console.WriteLine("===[ Started compilation... ]===");
			return Translator.Translate(Parser.Parse(ciqonaScripts.First()));
		}
	}
}