// Smidgens @ github

namespace Smidgenomics.Tools.Packages
{
	using UnityEngine;

	/// <summary>
	/// Export helpers
	/// </summary>
	internal static class Export_Extensions
	{

		/// <summary>
		/// Finds appropriate compression method for export
		/// </summary>
		public static FolderCompressor GetCompressor(this CompressionMethod t)
		{
			switch (t)
			{
				case CompressionMethod.Zip: return new CompressZip();
			}
			return null; // method not implemented
		}


		/// <summary>
		/// Export folder
		/// </summary>
		public static void Export(this PackageFolder package, CompressionMethod mode, in string outputFolder)
		{
			FolderCompressor compressor = mode.GetCompressor();

			if (compressor != null)
			{
				var result = compressor.Compress(new FCompressOptions
				{
					folder = package.Folder,
					outputName = package.Manifest.GetFullName(),
					password = null,
					outputFolder = outputFolder,
				});

				if (result.status == OpStatus.Success)
				{
					Debug.Log("Export complete!");
					ProjectExplorer.Open(result.file);
				}
				else
				{
					Debug.LogError(result.error);
				}
			}
			else
			{
				Debug.LogError($"{mode} export not implemented...sorry :(");
			}
		}
	}
}