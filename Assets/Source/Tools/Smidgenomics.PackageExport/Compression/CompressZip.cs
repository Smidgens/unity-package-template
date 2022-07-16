// Smidgens @ github

namespace Smidgenomics.Tools.Packages
{
	using System;
	using Unity.SharpZipLib.Utils;

	/// <summary>
	/// Zip compression
	/// </summary>
	internal class CompressZip : FolderCompressor
	{
		public const string FILE_ENDING = ".zip";

		public override FCompressResult Compress(FCompressOptions options)
		{
			try
			{
				var outputPath = BuildFilePath(options, FILE_ENDING);

				ZipUtility.CompressFolderToZip
				(
					outPathname: outputPath,
					password: options.password,
					folderName: options.folder
				);

				return new FCompressResult
				{
					status = OpStatus.Success,
					file = outputPath,
				};
			}
			catch(Exception e)
			{
				return new FCompressResult
				{
					status = OpStatus.Failure,
					error = e.Message,
				};
			}
		}
	}
}