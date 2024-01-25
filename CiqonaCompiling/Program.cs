
using System;
using System.Collections.Generic;
using System.IO;
using CiqonaCompiling;

static class Program
{
	private const string _ciqonaSrcPath = @"C:\Users\billm\bill_files\files\ciqona_projects\TestProject\src";
	private const string _targetCCodePath = @"C:\Users\billm\bill_files\files\ciqona_projects\TestProject\target\c_code\target.c";

	public static void Main()
	{
		Stack<string> ciqonaScriptsStk = new();
		foreach (string ciqonaFile in Directory.GetFiles(_ciqonaSrcPath, "*.ciq", SearchOption.AllDirectories))
		{
			ciqonaScriptsStk.Push(File.ReadAllText(ciqonaFile));
		}
		IEnumerable<string> ciqonaScripts = ciqonaScriptsStk;

		string intermediateCCode = CiqonaCompiler.Compile(ciqonaScripts);
		File.WriteAllText(_targetCCodePath, intermediateCCode);
	}
}
