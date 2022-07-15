using System.Runtime.CompilerServices;

#if UNITY_EDITOR
[assembly: InternalsVisibleTo("Moi.CustomPackage.Editor")]
[assembly: InternalsVisibleTo("Moi.CustomPackage.Tests")]
#endif