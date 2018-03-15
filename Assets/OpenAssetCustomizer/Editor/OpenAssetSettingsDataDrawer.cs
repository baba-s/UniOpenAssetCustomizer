using UnityEditor;
using UnityEngine;

namespace KoganeEditorLib
{
	[CustomPropertyDrawer( typeof( OpenAssetSettingsData ) )]
	public sealed class OpenAssetSettingsDataDrawer : PropertyDrawer
	{
		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
		{
			using ( new EditorGUI.PropertyScope( position, label, property ) )
			{
				position.height = EditorGUIUtility.singleLineHeight;

				var applicationPathRect = new Rect( position )
				{
					width = position.width - 18,
				};
				var dialogRect = new Rect( position )
				{
					x = applicationPathRect.xMax + 2,
					width = 16,
				};
				var extensionRect = new Rect( position )
				{
					y = applicationPathRect.yMax + 2,
				};

				var applicationPathProperty = property.FindPropertyRelative( "m_applicationPath" );
				var extensionProperty = property.FindPropertyRelative( "m_extension" );

				applicationPathProperty.stringValue = EditorGUI.TextField( applicationPathRect, applicationPathProperty.displayName, applicationPathProperty.stringValue );
				extensionProperty.stringValue = EditorGUI.TextField( extensionRect, extensionProperty.displayName, extensionProperty.stringValue );

				if ( GUI.Button( dialogRect, GUIContent.none, "ShurikenDropdown" ) )
				{
					var path = EditorUtility.OpenFilePanel( "Application Path", "", "" );
					if ( !string.IsNullOrEmpty( path ) )
					{
						applicationPathProperty.stringValue = path;
					}
				}
			}
		}
	}
}