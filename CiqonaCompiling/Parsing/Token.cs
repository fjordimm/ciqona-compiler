
using System.Collections.Generic;

namespace CiqonaCompiling.Parsing
{
	internal class Token
	{
		public readonly Tk tk;
		public readonly string contents;
		public readonly LineColTrace lineColTrace;

		public Token(Tk tk, string contents, LineColTrace lineColTrace)
		{
			this.tk = tk;
			this.contents = contents;
			this.lineColTrace = lineColTrace;
		}

		public override string ToString()
		{
			return this.contents is null ? $"{this.lineColTrace} ({this.tk})" : $"{this.lineColTrace} ({this.tk}, {this.contents})";
		}
	}

    internal enum Tk
    {
		BadToken,

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
		LessThanLessThan,

		Lit_null,
		Lit_true,
		Lit_false,

		K_print
    }

	internal static class ParseDict
	{
		internal static readonly Dictionary<string, Tk> Dict = new()
		{
			{ "{", Tk.BraceOpen },
			{ "}", Tk.BraceClose },
			{ "[", Tk.BracketOpen },
			{ "]", Tk.BracketClose },
			{ "(", Tk.ParenOpen },
			{ ")", Tk.ParenClose },
			{ ";", Tk.Semicolon },

			{ "null", Tk.Lit_null },
			{ "true", Tk.Lit_true },
			{ "false", Tk.Lit_false },

			{ "print", Tk.K_print }
		};
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