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
			if (!PackageLint.IsValidName(mf.name)) { return false; }
			if (!PackageLint.IsValidVersion(mf.name)) { return false; }
			return true;
		}

		public static string GetFullName(this in PackageManifest mf)
		{
			return $"{mf.name}@{mf.version}";
		}
	}
}