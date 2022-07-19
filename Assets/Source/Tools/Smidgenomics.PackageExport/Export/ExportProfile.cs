// Smidgens @ github

namespace Smidgenomics.Tools.Packages
{
	using UnityEngine;

	/// <summary>
	/// Export configuration
	/// </summary>
	[CreateAssetMenu(menuName = CFG.CreateMenu.EXPORT_PROFILE)]
	internal class ExportProfile : ScriptableObject
	{
		public string Package => _package;
		public CompressionMethod CompressionMode => _compression;

		// helper for inspector
		public static class FName
		{
			public const string PACKAGE_NAME = nameof(_package);
		}

		[Space(5f)]
		[PackageName]
		[Tooltip("Folder name under Packages")]
		[SerializeField] private string _package = "";

		[SerializeField] private CompressionMethod _compression = CompressionMethod.Zip;
	}
}

namespace Smidgenomics.Tools.Packages
{
	using System;
	using UnityEngine;

	internal static class ExportProfile_Extensions
	{
		/// <summary>
		/// Export from preset
		/// </summary>
		public static void Export(this ExportProfile preset)
		{
			try
			{
				var exportFolder = CFG.ProjectPath.PACKAGE_EXPORTS;
				var package = PackageFolder.Load(preset.Package);
				package?.Export(preset.CompressionMode, exportFolder);
			}
			catch (Exception e)
			{
				Debug.LogError("Unknown error exporting package.");
				Debug.LogError(e.Message);
			}
		}
	}
}


namespace Smidgenomics.Tools.Packages
{
	using UnityEngine;
	using UnityEditor;
	using System.IO;

	[CustomEditor(typeof(ExportProfile))]
	internal class ExportProfile_Editor : Editor
	{
		public override void OnInspectorGUI()
		{
			serializedObject.UpdateIfRequiredOrScript();
			base.OnInspectorGUI();
			serializedObject.ApplyModifiedProperties();
			GUILayout.Space(5f);
			DrawInfos();
		}

		private SerializedProperty _package = null;


		private void OnEnable()
		{
			_package = serializedObject.FindProperty(ExportProfile.FName.PACKAGE_NAME);
		}

		private void DrawInfos()
		{
			if(_package.stringValue.Length == 0) { return; }

			var proot = CFG.ProjectPath.PACKAGES;
			var fpath = $"{proot}/{_package.stringValue}";

			if(!Directory.Exists(fpath))
			{
				EditorGUILayout.HelpBox($"Package folder '{_package.stringValue}' missing", MessageType.Error);
				return;
			}

			using (new GUILayout.HorizontalScope(EditorStyles.helpBox))
			{
				GUILayout.Label(fpath, EditorStyles.wordWrappedMiniLabel);

				GUILayout.FlexibleSpace();

				if (GUILayout.Button("Open Folder...", EditorStyles.miniButton))
				{
					ProjectExplorer.Open(fpath + "/package.json");
				}
			}

		}
	}
}