// Smidgens @ github

namespace Smidgenomics.Tools.Packages
{
	using UnityEditor;
	using UnityEngine;
	using System.Text;

	/// <summary>
	/// Utils for project and file explorer
	/// </summary>
	internal static class ProjectExplorer
	{
		/// <summary>
		/// Show folder/file in file explorer
		/// </summary>
		public static void Open(string path) => EditorUtility.RevealInFinder(path);

		/// <summary>
		/// Get path to folder under project root
		/// </summary>
		public static string GetPathTo(in string folder)
		{
			var p = Application.dataPath;
			var sb = new StringBuilder();
			sb.Append(p, 0, p.Length - 6);
			sb.Append(folder);
			return sb.ToString();
		}
	}
}