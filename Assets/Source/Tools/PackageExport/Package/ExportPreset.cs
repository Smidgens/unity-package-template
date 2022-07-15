// Smidgens @ github

namespace Smidgenomics.Tools.Packages
{
	using UnityEngine;

	/// <summary>
	/// Export configuration
	/// </summary>
	[CreateAssetMenu(menuName = CFG.CreateMenu.EXPORT_PRESET)]
	internal class ExportPreset : ScriptableObject
	{
		public string Folder => _folder;
		public ZipType Archive => _archive;

		[Space(5f)]
		[SerializeField] private ZipType _archive = ZipType.Zip;

		[HideInInspector]
		// folder under Packages
		[SerializeField] private string _folder = "";

	}
}


namespace Smidgenomics.Tools.Packages
{
	using UnityEngine;
	using UnityEditor;
	using System.IO;

	[CustomEditor(typeof(ExportPreset))]
	internal class PackageExportPreset_Editor : Editor
	{
		public override void OnInspectorGUI()
		{
			serializedObject.UpdateIfRequiredOrScript();
			base.OnInspectorGUI();
			DrawFolderPopup();
			serializedObject.ApplyModifiedProperties();
			DrawInfos();
		}

		private SerializedProperty _folder = null;

		private string _packageRoot = "";

		private void OnEnable()
		{
			_packageRoot = Folders.Packages;
			_folder = serializedObject.FindProperty("_folder");
		}

		private void DrawInfos()
		{
			if(_folder.stringValue.Length == 0) { return; }
			var fpath = $"{_packageRoot}/{_folder.stringValue}";

			if(!Directory.Exists(fpath))
			{
				EditorGUILayout.HelpBox($"Package folder '{_folder.stringValue}' missing", MessageType.Error);
				return;
			}

			using (new GUILayout.VerticalScope(EditorStyles.helpBox))
			{
				GUILayout.Label(fpath, EditorStyles.wordWrappedMiniLabel);

				using(new GUILayout.HorizontalScope())
				{
					GUILayout.FlexibleSpace();

					if(GUILayout.Button("Show in Explorer", EditorStyles.miniButton))
					{
						Explorer.Open(fpath + "/package.json");
					}
				}
			}

		}

		/// <summary>
		/// Select folder
		/// </summary>
		private void DrawFolderPopup()
		{
			var label = _folder.stringValue;

			if(string.IsNullOrEmpty(label)) { label = "<unset>"; }

			var pos = EditorGUILayout.GetControlRect();

			pos = EditorGUI.PrefixLabel(pos, new GUIContent(_folder.displayName));

			if(GUI.Button(pos, label, EditorStyles.popup))
			{
				var m = new GenericMenu();
				var names = ListPackageFolders();
				m.AddItem(new GUIContent("<unset>"), string.IsNullOrEmpty(_folder.stringValue), () => SetFolder(""));
				if(names.Length == 0)
				{
					m.AddDisabledItem(new GUIContent("No Packages"));
				}
				foreach (var folderName in names)
				{
					var val = folderName;
					var isActive = folderName == _folder.stringValue;
					m.AddItem(new GUIContent(folderName), isActive, () => SetFolder(val));
				}
				m.ShowAsContext();
			}
		}

		private void SetFolder(string v)
		{
			_folder.stringValue = v;
			serializedObject.ApplyModifiedProperties();
		}

		private static string[] ListPackageFolders()
		{
			var packageFolder = Folders.Packages;
			DirectoryInfo d = new DirectoryInfo(packageFolder); //Assuming Test is your Folder
			var subFolders = d.GetDirectories("*", SearchOption.TopDirectoryOnly);
			var names = new string[subFolders.Length];
			for(var i = 0; i < subFolders.Length; i++)
			{
				names[i] = subFolders[i].Name;
			}
			return names;
		}
	}
}