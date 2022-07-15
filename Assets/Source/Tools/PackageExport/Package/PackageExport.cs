// Smidgens @ github

namespace Smidgenomics.Tools.Packages
{
	using System;
	using UnityEngine;

	/// <summary>
	/// Package exporter
	/// </summary>
	internal static class PackageExport
	{
		/// <summary>
		/// Export from preset
		/// </summary>
		public static void ExportPreset(ExportPreset preset)
		{
			var ex = PackageFolder.FromPreset(preset);
			try { ex?.Export(); }
			catch (Exception e) { Debug.LogError(e.Message); }
		}
	}
}