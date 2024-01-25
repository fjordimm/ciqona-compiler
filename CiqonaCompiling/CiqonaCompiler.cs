
using System;
using System.Collections.Generic;
using System.Linq;
using CiqonaCompiling.Parsing;

namespace CiqonaCompiling
{
	public static class CiqonaCompiler
	{
		public static string Compile(IEnumerable<string> ciqonaScripts)
		{
			IEnumerable<Token> bruh = Parser.Parse(ciqonaScripts.First());

			Console.WriteLine("tokens: \n[");
			foreach (Token token in bruh)
			{
				Console.WriteLine($"  token ({token.tk}, {token.contents})");
			}
			Console.WriteLine("]");

			return "yeah huh";
		}
	}
}