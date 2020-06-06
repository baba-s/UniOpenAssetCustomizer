using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
	[CustomPropertyDrawer( typeof( OpenAssetSettingsData ) )]
	internal sealed class OpenAssetSettingsDataDrawer : PropertyDrawer
	{
		public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
		{
			using ( new EditorGUI.PropertyScope( position, label, property ) )
			{
				position.height = EditorGUIUtility.singleLineHeight;

				var extensionRect = new Rect( position )
				{
				};
				var applicationPathRect = new Rect( position )
				{
					y     = extensionRect.yMax + 2,
					width = position.width - 18,
				};
				var dialogRect = new Rect( position )
				{
					x     = applicationPathRect.xMax + 2,
					y     = extensionRect.yMax + 2,
					width = 16,
				};
				
				var extensionProperty = property.FindPropertyRelative( "m_extension" );
				var applicationPathProperty = property.FindPropertyRelative( "m_applicationPath" );
				
				extensionProperty.stringValue = EditorGUI.TextField( extensionRect, extensionProperty.displayName, extensionProperty.stringValue );
				applicationPathProperty.stringValue = EditorGUI.TextField( applicationPathRect, applicationPathProperty.displayName, applicationPathProperty.stringValue );

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