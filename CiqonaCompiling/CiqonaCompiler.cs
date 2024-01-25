
using System;
using System.Collections.Generic;
using System.Linq;
using CiqonaCompiling.Abstraction;
using CiqonaCompiling.Parsing;

namespace CiqonaCompiling
{
	public static class CiqonaCompiler
	{
		public static string Compile(IEnumerable<string> ciqonaScripts)
		{
			return Abstractifier.Abstractify(Parser.Parse(ciqonaScripts.First()));
		}
	}
}