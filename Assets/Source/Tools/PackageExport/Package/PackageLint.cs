// Smidgens @ github

namespace Smidgenomics.Tools.Packages
{
	internal static class PackageLint
	{
		public static bool IsValidName(in string n)
		{
			// TODO: check if properly formatted name (as per unity package conventions)
			return !string.IsNullOrEmpty(n);
		}

		public static bool IsValidVersion(in string v)
		{
			// TODO: check if proper semver string
			return !string.IsNullOrEmpty(v);
		}
	}
}