// Smidgens @ github

namespace Smidgenomics.Tools.Packages
{
	using System.Text;

	/// <summary>
	/// Base class for compressing folder to archive
	/// </summary>
	internal abstract class FolderCompressor
	{
		public abstract FCompressResult Compress(FCompressOptions options);

		/// <summary>
		/// Build file path
		/// </summary>
		protected static string BuildFilePath(in FCompressOptions opts, in string ending)
		{
			var path = new StringBuilder(opts.outputFolder);
			path.Append('/');
			path.Append(opts.outputName);
			path.Append(ending);
			return path.ToString();
		}
	}
}

namespace Smidgenomics.Tools.Packages
{

	/// <summary>
	/// Archive type
	/// </summary>
	internal enum CompressionMethod
	{
		Zip,
		[UnityEngine.InspectorName("Tarball (not implemented)")]
		Tarball,
	}


	internal delegate void ProcessFileCallback(string path);

	internal struct FCompressOptions
	{
		/// <summary>
		/// Folder to 
		/// </summary>
		public string folder;
		/// <summary>
		/// Folder to save to
		/// </summary>
		public string outputFolder;
		/// <summary>
		/// Name of output file without ending/extension
		/// </summary>
		public string outputName;
		/// <summary>
		/// Password
		/// </summary>
		public string password;
		/// <summary>
		/// Callback for when file is added to archive (if supported by archive method)
		/// </summary>
		public ProcessFileCallback onFileProcessed;
	}

	/// <summary>
	/// Op success
	/// </summary>
	internal enum OpStatus { Success, Failure, }

	/// <summary>
	/// Result of folder compression
	/// </summary>
	internal struct FCompressResult
	{
		public OpStatus status;
		public string file;
		public string error;
	}
}