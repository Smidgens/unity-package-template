namespace Smidgenomics.Tools.Packages
{
	using UnityEditor;

	internal static class Explorer
	{
		/// <summary>
		/// Show folder/file in file explorer
		/// </summary>
		public static void Open(string path) => EditorUtility.RevealInFinder(path);
	}
}
