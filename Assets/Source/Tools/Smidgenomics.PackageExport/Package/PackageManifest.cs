// Smidgens @ github

namespace Smidgenomics.Tools.Packages
{
	using System;

	[Serializable]
	internal struct PackageManifest
	{
		public string name;
		public string version;
		// note: not all fields included
		// see: https://docs.unity3d.com/Manual/upm-manifestPkg.html
	}
}

namespace Smidgenomics.Tools.Packages
{
	using System;

	[Serializable]
	internal static class PackageManifest_Extensions
	{
		public static bool IsValid(this in PackageManifest mf)
		{
			if (!mf.HasValidName()) { return false; }
			if (!mf.HasValidVersion()) { return false; }
			return true;
		}

		public static string GetFullName(this in PackageManifest mf)
		{
			return $"{mf.name}@{mf.version}";
		}

		public static bool HasValidName(this in PackageManifest mf)
		{
			// TODO: check if properly formatted name (as per unity package conventions)
			return !string.IsNullOrEmpty(mf.name);
		}

		public static bool HasValidVersion(this in PackageManifest mf)
		{
			// TODO: check if proper semver string
			return !string.IsNullOrEmpty(mf.version);
		}
	}
}