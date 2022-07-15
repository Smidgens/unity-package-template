// Smidgens @ github

namespace Smidgenomics.Tools.Packages
{
	using System;
	using System.IO;
	using System.IO.Compression;

	/// <summary>
	/// GZip methods
	/// </summary>
	internal static class GZip
	{
		/// <summary>
		/// Create .gz archive from directory
		/// 
		/// Note: currently broken
		public static void Folder(string folder, string outputFile, Action<string> onFile = null)
		{
			if(onFile == null) { onFile = NoOp; }

			string[] sFiles = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories);
			int iDirLen = folder[folder.Length - 1] == Path.DirectorySeparatorChar ? folder.Length : folder.Length + 1;

			using (FileStream outFile = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				using (GZipStream str = new GZipStream(outFile, CompressionMode.Compress))
				{
					foreach (string sFilePath in sFiles)
					{
						string currentFile = sFilePath.Substring(iDirLen);
						onFile.Invoke(currentFile);
						AddFile(folder, currentFile, str);
					}
				}
			}
		}

		// add file to archive
		private static void AddFile(string dir, string relativePath, GZipStream zipStream)
		{
			//Compress file name
			char[] chars = relativePath.ToCharArray();
			zipStream.Write(BitConverter.GetBytes(chars.Length), 0, sizeof(int));
			foreach (char c in chars)
			{
				zipStream.Write(BitConverter.GetBytes(c), 0, sizeof(char));
			}
			//Compress file content
			byte[] bytes = File.ReadAllBytes(Path.Combine(dir, relativePath));
			zipStream.Write(BitConverter.GetBytes(bytes.Length), 0, sizeof(int));
			zipStream.Write(bytes, 0, bytes.Length);
		}

		private static void NoOp(string _) { } // default callback

	}

}