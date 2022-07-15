// Smidgens @ github

namespace Smidgenomics.Tools.Packages
{
	using System.IO;
	using UnityEngine;
	using System.Text;

	internal class PackageFolder
	{
		public static PackageFolder FromPreset(ExportPreset preset)
		{
			if(!TryGetPackageFolderPath(preset, out string packageFolder))
			{
				Debug.LogWarning($"Cannot export: package '{preset.Folder}' is invalid");
				return null; // package not found
			}

			if (!TryReadPackageManifest(packageFolder, out var manifest))
			{
				Debug.Log("Error reading package manifest");
				return null;
			}
			var exportFolder = Folders.Export;
			var zipName = GetExportName(manifest, preset.Archive);
			var zipPath = $"{exportFolder}/{zipName}";
			return new PackageFolder(packageFolder, zipPath, preset.Archive);
		}

		/// <summary>
		/// Export
		/// </summary>
		public void Export()
		{
			var exported = false;
			switch (_zipType)
			{
				case ZipType.Zip:
					Zip.Folder(_packageFolder, _zipPath);
					exported = true;
					break;
				default:
					Debug.LogError($"{_zipType.GetFileEnding()} export not implemented...sorry :(");
					break;
			}

			if (exported)
			{
				Debug.Log("Export complete!");
				Explorer.Open(_zipPath);
			}
		}

		private ZipType _zipType = default;
		private string _packageFolder = default;
		private string _zipPath = default;

		private PackageFolder(in string packageFolder, in string zipPath, in ZipType zipType)
		{
			_packageFolder = packageFolder;
			_zipPath = zipPath;
			_zipType = zipType;
		}

		// get name of export file without directory
		private static string GetExportName(in PackageManifest mf, in ZipType type)
		{
			var sb = new StringBuilder(mf.GetFullName());
			sb.Append(type.GetFileEnding());
			return sb.ToString();
		}

		// try to load package.json file
		private static bool TryReadPackageManifest(in string packageFolder, out PackageManifest manifest)
		{
			try
			{
				var fpath = $"{packageFolder}/package.json";
				var json = File.ReadAllText(fpath);
				var mf = JsonUtility.FromJson<PackageManifest>(json);

				if (!mf.IsValid())
				{
					manifest = default;
					return false;
				}

				manifest = mf;
				return true;
			}
			catch { }
			manifest = default;
			return false;
		}

		// Get full path to package folder
		private static bool TryGetPackageFolderPath(ExportPreset preset, out string path)
		{
			path = null;
			var pfolder = preset.Folder;
			if (string.IsNullOrEmpty(pfolder)) { return false; }
			if (pfolder.Contains("/") || pfolder.Contains("\\")) { return false; }

			var packageFolder = Folders.Packages;

			var fp = $"{packageFolder}/{pfolder}";
			var exists = Directory.Exists(fp);
			if (!exists) { return false; }
			path = fp;
			return true;
		}


	}
}
