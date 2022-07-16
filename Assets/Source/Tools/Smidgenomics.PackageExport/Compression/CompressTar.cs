// Smidgens @ github

namespace Smidgenomics.Tools.Packages
{
	/// <summary>
	/// Tarball compression
	/// </summary>
	internal class CompressTar : FolderCompressor
	{
		public const string FILE_ENDING = ".tar.gz";

		public override FCompressResult Compress(FCompressOptions options)
		{
			throw new System.NotImplementedException();
		}
	}
}