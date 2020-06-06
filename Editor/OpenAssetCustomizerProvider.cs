using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kogane.Internal
{
	internal sealed class OpenAssetCustomizerProvider : SettingsProvider
	{
		private OpenAssetSettings  m_settings;
		private SerializedObject   m_serializedObject;
		private SerializedProperty m_property;
		private ReorderableList    m_reorderableList;

		public OpenAssetCustomizerProvider( string path, SettingsScope scope )
			: base( path, scope )
		{
		}

		public override void OnActivate
		(
			string        searchContext,
			VisualElement rootElement
		)
		{
			m_settings         = OpenAssetSettings.Instance;
			m_serializedObject = new SerializedObject( m_settings );
			m_property         = m_serializedObject.FindProperty( "m_list" );

			m_reorderableList = new ReorderableList
			(
				serializedObject: m_serializedObject,
				elements: m_property,
				draggable: true,
				displayHeader: false,
				displayAddButton: true,
				displayRemoveButton: true
			)
			{
				elementHeight       = 44,
				drawElementCallback = OnDrawElement,
			};
		}

		private void OnDrawElement( Rect rect, int index, bool isActive, bool isFocused )
		{
			var element = m_property.GetArrayElementAtIndex( index );
			rect.height -= 4;
			rect.y      += 2;
			EditorGUI.PropertyField( rect, element );
		}

		public override void OnDeactivate()
		{
			OpenAssetSettings.Save();
		}

		public override void OnGUI( string searchContext )
		{
			m_serializedObject.Update();
			m_reorderableList.DoLayoutList();
			m_serializedObject.ApplyModifiedProperties();
		}

		[SettingsProvider]
		private static SettingsProvider Create()
		{
			var path     = "Preferences/UniOpenAssetCustomizer";
			var provider = new OpenAssetCustomizerProvider( path, SettingsScope.User );

			return provider;
		}
	}
}