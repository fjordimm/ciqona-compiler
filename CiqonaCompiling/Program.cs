
using System;
using System.Collections.Generic;
using System.IO;
using CiqonaCompiling;

static class Program
{
	private const string _ciqonaSrcPath = @"C:\Users\billm\bill_files\files\ciqona_projects\TestProject\src";
	private const string _targetCFilePath = @"C:\Users\billm\bill_files\files\ciqona_projects\TestProject\target\c_code\target.c";

	public static void Main()
	{
		Queue<string> ciqonaFilePathsStk = new();
		foreach (string ciqonaFilePath in Directory.GetFiles(_ciqonaSrcPath, "*.ciq", SearchOption.AllDirectories))
		{
			ciqonaFilePathsStk.Enqueue(ciqonaFilePath);
		}
		
		CiqonaCompiler.Compile(ciqonaFilePathsStk, _targetCFilePath);
	}
}
