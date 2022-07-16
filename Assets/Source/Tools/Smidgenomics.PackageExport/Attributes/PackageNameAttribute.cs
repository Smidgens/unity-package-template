// Smidgens @ github

namespace Smidgenomics.Tools.Packages
{
	using UnityEngine;
	using System;

	/// <summary>
	/// Select field
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class PackageNameAttribute : PropertyAttribute
	{
		/// <summary>
		/// Require package.json file
		/// </summary>
		public bool RequireManifest { get; set; } = true;
	}
}

namespace Smidgenomics.Tools.Packages
{
	using UnityEngine;
	using UnityEditor;
	using System.IO;
	using System.Collections.Generic;

	// cached gui label
	using LZLabel = System.Lazy<UnityEngine.GUIContent>;

	[CustomPropertyDrawer(typeof(PackageNameAttribute))]
	public class PackageNameAttribute_PD : PropertyDrawer
	{
		public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent l)
		{
			if(l != GUIContent.none) { pos = EditorGUI.PrefixLabel(pos, l); }

			// error check
			DrawErrorIfAny(ref pos, prop);

			// unsupported type
			if (prop.propertyType != SerializedPropertyType.String)
			{
				EditorGUI.DrawRect(pos, Color.red * 0.3f);
				EditorGUI.LabelField(pos, "Must be string", EditorStyles.miniLabel);
				return;
			}

			// show popup
			DrawPackageDropdown(pos, prop, attribute as PackageNameAttribute);
		}

		// error label
		private static LZLabel _MissingPackageLabel = new LZLabel(() => new GUIContent("!", "Folder not found"));

		/// <summary>
		/// Dropdown
		/// </summary>
		private static void DrawPackageDropdown(Rect pos, SerializedProperty prop, PackageNameAttribute attr)
		{
			var label = prop.stringValue;

			if (string.IsNullOrEmpty(label)) { label = "<unset>"; }

			if (GUI.Button(pos, label, EditorStyles.popup))
			{
				var m = new GenericMenu();
				var names = GetPackageNames(attr.RequireManifest);
				m.AddItem(new GUIContent("<unset>"), string.IsNullOrEmpty(prop.stringValue), () => SetAndApply(prop, ""));
				if (names.Length == 0)
				{
					m.AddDisabledItem(new GUIContent("No Packages"));
				}
				foreach (var folderName in names)
				{
					var val = folderName;
					var isActive = folderName == prop.stringValue;
					m.AddItem(new GUIContent(folderName), isActive, () => SetAndApply(prop, val));
				}
				m.ShowAsContext();
			}
		}

		/// <summary>
		/// Show error info if needed (missing folder etc.)
		/// </summary>
		private static void DrawErrorIfAny(ref Rect pos, SerializedProperty prop)
		{
			// no value set
			if (string.IsNullOrEmpty(prop.stringValue)) { return; }
			// path is legit
			if (PackageExists(prop.stringValue)) { return; }

			pos.width -= pos.height;
			var iconPos = pos.position;
			iconPos.x += pos.width;
			var iconRect = pos;
			iconRect.width = iconRect.height;
			iconRect.position = iconPos;
			EditorGUI.DrawRect(iconRect, Color.red * 0.5f);
			EditorGUI.LabelField(iconRect, _MissingPackageLabel.Value);
		}

		private static bool PackageExists(in string name)
		{
			var path = Path.Combine(CFG.ProjectPath.PACKAGES, name);
			return Directory.Exists(path);
		}

		/// <summary>
		/// Apply value to string prop
		/// </summary>
		private static void SetAndApply(SerializedProperty prop, in string v)
		{
			prop.stringValue = v;
			prop.serializedObject.ApplyModifiedProperties();
		}

		/// <summary>
		/// Get list of package names
		/// </summary>
		private static string[] GetPackageNames(bool requireManifest)
		{
			// todo: check if folders have package.json files
			var packageFolder = CFG.ProjectPath.PACKAGES;
			DirectoryInfo d = new DirectoryInfo(packageFolder); //Assuming Test is your Folder
			var subFolders = d.GetDirectories("*", SearchOption.TopDirectoryOnly);
			var names = new List<string>();
			for (var i = 0; i < subFolders.Length; i++)
			{
				if(requireManifest)
				{
					var manifestPath = Path.Combine(subFolders[i].FullName, "package.json");
					var hasManifest = File.Exists(manifestPath);
					if (!hasManifest) { continue; }
				}
				names.Add(subFolders[i].Name);
			}
			return names.ToArray();
		}

	}
}
