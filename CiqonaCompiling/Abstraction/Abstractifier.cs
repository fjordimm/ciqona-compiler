
using System;
using System.Collections.Generic;
using CiqonaCompiling.Parsing;

namespace CiqonaCompiling.Abstraction
{
	internal static class Abstractifier
	{
		public static string Abstractify(IEnumerable<Token> tokens)
		{
			CodeBlock mainBlock = new();

			IEnumerator<Token> tokensEnumer = tokens.GetEnumerator();
			while(tokensEnumer.MoveNext())
			{
				Token tok = tokensEnumer.Current;

				if (tok.tk == Tk.K_print)
				{
					// Make a list of all tokens up until the next semicolon
					List<Token> restOfLine = new();
					do
					{
						if (!tokensEnumer.MoveNext()) break;
						if (tokensEnumer.Current.tk == Tk.Semicolon) break;

						restOfLine.Add(tokensEnumer.Current);
					}
					while (true);

					if

					Console.WriteLine("shlingus:\n{");
					foreach (Token t in restOfLine)
					{
						Console.WriteLine($"  token ({t.tk}, {t.contents})");
					}
					Console.WriteLine("}");
				}
			}

			AbstractedProgram abstractedProgram = new(mainBlock);

			return abstractedProgram.InC();
		}
	}
}