
using System;
using System.Collections.Generic;
using System.Linq;
using CiqonaCompiling.Translation;
using CiqonaCompiling.Parsing;
using System.IO;

namespace CiqonaCompiling
{
	public static class CiqonaCompiler
	{
		internal const bool EnableCompilerPrintingWalkthrough = true;

		public static void Compile(IEnumerable<string> ciqonaFilePaths, string targetCFilePath)
		{
			if (EnableCompilerPrintingWalkthrough) Console.WriteLine("===[ Started compilation... ]===");

			using (StreamWriter sw = new(targetCFilePath))
			{
				foreach (string ciqonaFilePath in ciqonaFilePaths)
				{ Translator.Translate(sw, Parser.Parse(ciqonaFilePath)); }
			}

			if (EnableCompilerPrintingWalkthrough) Console.WriteLine("===[ Finished compilation ]===");
		}
	}
}