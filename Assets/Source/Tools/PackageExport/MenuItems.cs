// Smidgens @ github

namespace Smidgenomics.Tools.Packages
{
	using UnityEditor;

	/// <summary>
	/// Menu items to open windows
	/// </summary>
	internal static class MenuItems
	{
		/// <summary>
		/// Open export window
		/// </summary>
		[MenuItem(CFG.MenuItem.EXPORT_PACKAGE)]
		private static void ExportItems() => PackageExporter.Open();
	}
}