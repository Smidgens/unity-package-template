// Smidgens @ github

namespace Smidgenomics.Tools.Packages
{
	using UnityEngine;
	using System;
	using System.Text;

	/// <summary>
	/// Project folders
	/// </summary>
	internal static class Folders
	{
		/// <summary>
		/// [project-path]/Packages
		/// </summary>
		public static string Packages { get; } = GetProjectFolder("Packages");
		public static string Export { get; } = GetProjectFolder("Export");

		/// <summary>
		/// [project-path]/Export
		/// </summary>
		//public static string Export => _ExportFolder.Value;

		private static string _packageFolder = GetProjectFolder("Packages");

		// lazy because unity 
		private static Lazy<string>
		_PackageFolder = new Lazy<string>(() => GetProjectFolder("Packages")),
		_ExportFolder = new Lazy<string>(() => GetProjectFolder("Export"));

		private static string GetProjectFolder(in string folder)
		{
			var p = Application.dataPath;
			var sb = new StringBuilder();
			sb.Append(p, 0, p.Length - 6);
			sb.Append(folder);
			return sb.ToString();
		}
	}
}