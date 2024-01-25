
using System.Collections.Generic;

namespace CiqonaCompiling.Parsing
{
	internal class Token
	{
		public readonly Tk tk;
		public readonly string contents;
		public readonly LineColTrace lineColTrace;

		public Token(Tk tk, string contents/*, LineColTrace lineColTrace*/)
		{
			this.tk = tk;
			this.contents = contents;
			this.lineColTrace = new LineColTrace(-1, -1);
		}
	}

    internal enum Tk
    {
		Bruh,

        Identifier,
		NumLiteral,
		CharLiteral,
		StringLiteral,

		BraceOpen,
		BraceClose,
		BracketOpen,
		BracketClose,
		ParenOpen,
		ParenClose,
		Semicolon,

		Lit_null,
		Lit_true,
		Lit_false,

		K_print
    }

	internal static class KeywordDict
	{
		public static void Initialize(Dictionary<string, Tk> dict)
		{
			dict.Add("{", Tk.BraceOpen);
			dict.Add("}", Tk.BraceClose);
			dict.Add("[", Tk.BracketOpen);
			dict.Add("]", Tk.BracketClose);
			dict.Add("(", Tk.ParenOpen);
			dict.Add(")", Tk.ParenClose);
			dict.Add(";", Tk.Semicolon);

			dict.Add("null", Tk.Lit_null);
			dict.Add("true", Tk.Lit_true);
			dict.Add("false", Tk.Lit_false);

			dict.Add("print", Tk.K_print);
		}
	}

	internal readonly struct LineColTrace
	{
		public readonly int lineNum;
		public readonly int colNum;

		public LineColTrace(int lineNum, int colNum)
		{
			this.lineNum = lineNum;
			this.colNum = colNum;
		}

		public override string ToString()
		{
			return $"{lineNum}:{colNum}";
		}
	}
}