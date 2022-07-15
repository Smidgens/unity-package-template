// Smidgens @ github

namespace Smidgenomics.Tools.Packages
{
	using UnityEngine;

	/// <summary>
	/// Zip types
	/// </summary>
	internal enum ZipType
	{
		[InspectorName(".zip")] Zip,
		[InspectorName(".gz")] GZip,
		[InspectorName(".tar")] Tar,
	}
}


namespace Smidgenomics.Tools.Packages
{
	internal static class Enum_Extensions
	{
		public static string GetFileEnding(this ZipType t)
		{
			switch (t)
			{
				case ZipType.GZip: return ".gz";
				case ZipType.Zip: return ".zip";
				case ZipType.Tar: return ".tar";
			}
			return "";
		}
	}
}