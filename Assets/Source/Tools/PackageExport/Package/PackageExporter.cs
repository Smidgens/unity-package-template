// Smidgens @ github

namespace Smidgenomics.Tools.Packages
{
	using UnityEngine;
	using UnityEditor;

	/// <summary>
	/// Package export
	/// </summary>
	internal class PackageExporter : EditorWindow
	{
		public static class BLabel
		{
			public static string EXPORT = "Export";
			public static string VIEW = "View";
		}

		public static void Open()
		{
			var w = GetWindow<PackageExporter>(typeof(SceneView));
			w.titleContent.text = CFG.WindowTitle.EXPORT_PACKAGE;
			w.Show();
		}

		// presets found in project
		private ExportPreset[] _presets = { };

		// find query for asset database (cached due to interpolation)
		private readonly string _PRESET_FIND_QUERY = $"t:{nameof(ExportPreset)}";

		// folders to load export presets from
		private readonly string[] _PRESET_FOLDERS =
		{
			CFG.AssetFolder.EXPORT_PRESETS,
		};

		private void OnGUI()
		{
			using (new GUILayout.HorizontalScope(EditorStyles.helpBox))
			{
				GUILayout.FlexibleSpace();

				// reload list of presets
				if (GUILayout.Button(CFG.ButtonLabel.REFRESH_PRESET_LIST))
				{
					RefreshPresets();
				}
			}

			GUILayout.Space(10f);
			DrawPresetList();
		}

		private void OnEnable()
		{
			RefreshPresets();
		}

		// export preset (after gui update)
		private void ExportPreset(ExportPreset p) => EditorApplication.delayCall += () => PackageExport.ExportPreset(p);

		/// <summary>
		/// Find presets in project
		/// </summary>
		private void RefreshPresets()
		{
			var guids = AssetDatabase.FindAssets(_PRESET_FIND_QUERY, _PRESET_FOLDERS);
			_presets = new ExportPreset[guids.Length];
			for(var i = 0; i < guids.Length; i++)
			{
				var path = AssetDatabase.GUIDToAssetPath(guids[i]);
				_presets[i] = AssetDatabase.LoadAssetAtPath<ExportPreset>(path);
			}
		}

		/// <summary>
		/// Draw list of presets in project
		/// </summary>
		private void DrawPresetList()
		{
			if(_presets.Length == 0)
			{
				EditorGUILayout.HelpBox("No Presets Found", MessageType.Info);
				return;
			}
			
			foreach(var p in _presets)
			{
				if (!p) { continue; } // maybe preset got deleted?

				var preset = p; // loop scope thingy

				// [label(name)] <flex> [btn(view)][btn(export)]
				using (new EditorGUILayout.HorizontalScope(EditorStyles.helpBox))
				{
					EditorGUILayout.LabelField(p.name);

					GUILayout.FlexibleSpace();

					// show preset in inspector
					if (GUILayout.Button(BLabel.VIEW)) { Selection.activeObject = preset; }

					// run export with preset
					if (GUILayout.Button(BLabel.EXPORT)) { ExportPreset(preset); }
				}
			}
		}
	}
}