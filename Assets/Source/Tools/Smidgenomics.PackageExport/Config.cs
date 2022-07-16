// Smidgens @ github

namespace Smidgenomics.Tools.Packages
{
	/// <summary>
	/// Magic strings, Lt.Dan
	/// </summary>
	internal static class CFG
	{
		public const string AUTHOR = "Smidgenomics";

		/// <summary>
		/// Resource paths starting from Assets folder
		/// </summary>
		public static class AssetPath
		{
			/// <summary>
			/// Path to look for export profiles under
			/// </summary>
			public const string EXPORT_PROFILES = "Assets/Config/Editor";
		}

		/// <summary>
		/// Paths to project folders
		/// </summary>
		public static class ProjectPath
		{
			/// <summary>
			/// [project root]/Packages
			/// </summary>
			public static readonly string PACKAGES = ProjectExplorer.GetPathTo("Packages");

			/// <summary>
			/// [project root]/[export folder]
			/// </summary>
			public static readonly string PACKAGE_EXPORTS = ProjectExplorer.GetPathTo("Export");
		}

		/// <summary>
		/// Create menu for assets
		/// </summary>
		public static class CreateMenu
		{
			public const string EXPORT_PROFILE = _PREFIX + "Packages/Export Profile";
		
			private const string _PREFIX = AUTHOR + "/"; // prefixed to every
		}

		/// <summary>
		/// Menu paths in nav
		/// </summary>
		public static class MenuItem
		{
			// export window
			public const string EXPORT_PACKAGE = _PREFIX + "Package Exporter";

			// Prefixed to every menu path
			private const string _PREFIX = "Window/";
		}
	}
}