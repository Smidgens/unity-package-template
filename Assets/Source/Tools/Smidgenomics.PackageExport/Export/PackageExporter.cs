// Smidgens @ github

namespace Smidgenomics.Tools.Packages
{
	using UnityEngine;
	using UnityEditor;
	using System;

	/// <summary>
	/// Package export window
	/// </summary>
	internal class PackageExporter : EditorWindow
	{
		public static class WLabel
		{
			public const string W_TITLE = "Export Package";
			public const string EXPORT = "Export";
			public const string VIEW = "View";
			public const string REFRESH = "Refresh";
			public const string PRESET_LIST_HEADER = "Export Presets";
			public const string NO_PRESETS_MSG = "No Presets Found under '$PATH'";
		}

		public static void Open()
		{
			var w = GetWindow<PackageExporter>(typeof(SceneView));
			w.titleContent.text = WLabel.W_TITLE;
			w.Show();
		}

		// presets found in project
		private ExportProfile[] _profiles = { };

		// find query for asset database (cached due to interpolation)
		private readonly string _PRESET_FIND_QUERY = $"t:{typeof(ExportProfile).Name}";

		// folders to load export presets from
		private readonly string[] _PRESET_FOLDERS =
		{
			CFG.AssetPath.EXPORT_PROFILES,
		};

		private readonly Lazy<GUIStyle> _HeaderLabel = new Lazy<GUIStyle>(() =>
		{
			var s = new GUIStyle(EditorStyles.boldLabel);
			s.fontSize = 14;
			s.normal.textColor = Color.white;
			return s;
		});

		private void OnGUI()
		{
			GUILayout.Space(10f);
			DrawProfileList();
		}

		private void OnEnable()
		{
			RefreshList();
		}

		// export preset (after gui update)
		private void RunExport(ExportProfile p) => EditorApplication.delayCall += () => p.Export();

		/// <summary>
		/// Find profiles in project
		/// </summary>
		private void RefreshList()
		{
			var guids = AssetDatabase.FindAssets(_PRESET_FIND_QUERY, _PRESET_FOLDERS);
			_profiles = new ExportProfile[guids.Length];
			for(var i = 0; i < guids.Length; i++)
			{
				var path = AssetDatabase.GUIDToAssetPath(guids[i]);
				_profiles[i] = AssetDatabase.LoadAssetAtPath<ExportProfile>(path);
			}
		}

		/// <summary>
		/// Draw list of export profiles
		/// </summary>
		private void DrawProfileList()
		{

			// [header] <flex> [refresh btn]
			using (new GUILayout.HorizontalScope())
			{
				EditorGUILayout.LabelField(WLabel.PRESET_LIST_HEADER, _HeaderLabel.Value);

				GUILayout.FlexibleSpace();

				// refresh presets
				if (GUILayout.Button(WLabel.REFRESH)) { RefreshList(); }
			}

			if (_profiles.Length == 0)
			{
				var msg = WLabel.NO_PRESETS_MSG.Replace("$PATH", CFG.AssetPath.EXPORT_PROFILES);
				EditorGUILayout.HelpBox(msg, MessageType.Info);
			}

			foreach (var p in _profiles)
			{
				if (!p) { continue; } // maybe preset got deleted?

				var profile = p; // loop scope thingy

				// [name] <flex> [view btn][export btn]
				using (new EditorGUILayout.HorizontalScope(GUI.skin.box))
				{
					EditorGUILayout.LabelField(p.name, EditorStyles.whiteLargeLabel);

					// flex
					GUILayout.FlexibleSpace();
					// view btn
					if (GUILayout.Button(WLabel.VIEW)) { Selection.activeObject = profile; }
					// export btn
					if (GUILayout.Button(WLabel.EXPORT)) { RunExport(profile); }
				}
			}
		}
	}
}