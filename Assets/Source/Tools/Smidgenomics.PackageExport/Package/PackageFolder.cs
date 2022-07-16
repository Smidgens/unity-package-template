// Smidgens @ github

namespace Smidgenomics.Tools.Packages
{
	using System.IO;
	using UnityEngine;
	using System.Text;

	internal class PackageFolder
	{
		/// <summary>
		/// Loads folder under Packages
		/// </summary>
		public static PackageFolder Load(in string name)
		{
			// full path to package folder in project
			var folderPath = GetFullPackagePath(name);

			// folder doesn't exist
			if(string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
			{
				Debug.LogWarning($"Package Folder '{name}' invalid or not found");
				return null;
			}

			var manifestPath = $"{folderPath}/package.json";

			// no manifest file in folder (package.json)
			if(!File.Exists(manifestPath))
			{
				Debug.LogWarning($"Manifest not found for package '{name}'");
				return null;
			}

			// invalid json
			if (!TryReadManifest(manifestPath, out var manifest))
			{
				Debug.Log($"Package manifest for '{name}' has syntax errors");
				return null;
			}

			// manifest has invalid fields
			if(!manifest.IsValid())
			{
				Debug.Log($"Package manifest for '{name}' has invalid properties");
				return null;
			}

			// fiiiiiiinally
			return new PackageFolder(folderPath, manifest);
		}

		/// <summary>
		/// Full path to package folder in project
		/// </summary>
		public string Folder => _folder;

		/// <summary>
		/// Package manifest
		/// </summary>
		public PackageManifest Manifest => _manifest;

		private string _folder = default;
		private PackageManifest _manifest = default;

		private PackageFolder(in string folder, in PackageManifest manifest)
		{
			_folder = folder;
			_manifest = manifest;
		}

		/// <summary>
		/// Try to load package manifest at given path
		/// </summary>
		private static bool TryReadManifest(in string manifestPath, out PackageManifest manifest)
		{
			try
			{
				var json = File.ReadAllText(manifestPath);
				manifest = JsonUtility.FromJson<PackageManifest>(json);
				return true;
			}
			catch { }
			manifest = default;
			return false;
		}

		/// <summary>
		/// Get full path to package folder from name
		/// </summary>
		private static string GetFullPackagePath(in string name)
		{
			if (string.IsNullOrEmpty(name)) { return null; }
			if (name.Contains("/") || name.Contains("\\")) { return null; }
			var path = new StringBuilder(CFG.ProjectPath.PACKAGES);
			path.Append('/');
			path.Append(name);
			return path.ToString();
		}
	}
}