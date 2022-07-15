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
		/// Resource paths relative to Assets folder
		/// </summary>
		public static class AssetFolder
		{
			/// <summary>
			/// Path to look for export presets under
			/// </summary>
			public const string EXPORT_PRESETS = "Assets/Config/_editor";
		}

		/// <summary>
		/// Editor windows
		/// </summary>
		public static class WindowTitle
		{
			public const string EXPORT_PACKAGE = "Export Package";
		}

		/// <summary>
		/// Create menu for scriptable objects
		/// </summary>
		public static class CreateMenu
		{
			public const string EXPORT_PRESET = _PREFIX + "Packages/Export Preset";
			// Prefixed to every menu path
			private const string _PREFIX = AUTHOR + "/";
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

		/// <summary>
		/// Inspector buttons
		/// </summary>
		public static class ButtonLabel
		{
			public const string REFRESH_PRESET_LIST = "Refresh List";
		}
	}
}