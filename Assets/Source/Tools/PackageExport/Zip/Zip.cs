// Smidgens @ github

namespace Smidgenomics.Tools.Packages
{
	using Unity.SharpZipLib.Utils;

	internal static class Zip
	{
		public static void Folder(string folder, string outputFile)
		{
			ZipUtility.CompressFolderToZip(outputFile, null, folder);
		}

	}
}
