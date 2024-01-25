
using System;
using System.Collections.Generic;
using CiqonaCompiling.Errors;
using CiqonaCompiling.Parsing;

namespace CiqonaCompiling.Abstraction
{
	internal static class Abstractifier
	{
		public static string Abstractify(IEnumerable<Token> tokens)
		{
			if (CiqonaCompiler.EnableCompilerPrintingWalkthrough) Console.WriteLine("===[ Started abstractifying... ]===");

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

					if (restOfLine.Count != 1)
					{ CompilerErrors.PrintCompilerError(tokensEnumer.Current.lineColTrace, CompilerError.PrintExpectation); }
					else if (restOfLine[0].tk != Tk.StringLiteral)
					{ CompilerErrors.PrintCompilerError(tokensEnumer.Current.lineColTrace, CompilerError.PrintExpectation); }
					else
					{

					}
				}
			}

			AbstractedProgram abstractedProgram = new(mainBlock);

			if (CiqonaCompiler.EnableCompilerPrintingWalkthrough) Console.WriteLine("===[ Finished abstractifying ]===");
			return abstractedProgram.InC();
		}
	}
}