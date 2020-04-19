using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UniOpenAssetCustomizer
{
	[Serializable]
	internal sealed class OpenAssetSettingsData
	{
		[SerializeField] private string m_applicationPath = null;
		[SerializeField] private string m_extension       = null;

		public string ApplicationPath => m_applicationPath;
		public string Extension       => m_extension;
	}

	[Serializable]
	internal sealed class OpenAssetSettings : ScriptableObject
	{
		private const string KEY = "UniOpenAssetCustomizer";

		[SerializeField] private OpenAssetSettingsData[] m_list = null;

		private static OpenAssetSettings m_instance;

		public IList<OpenAssetSettingsData> List => m_list;

		public static OpenAssetSettings Instance
		{
			get
			{
				if ( m_instance != null ) return m_instance;

				m_instance = CreateInstance<OpenAssetSettings>();

				var json = EditorPrefs.GetString( KEY );

				JsonUtility.FromJsonOverwrite( json, m_instance );

				if ( m_instance == null )
				{
					m_instance = CreateInstance<OpenAssetSettings>();
				}

				return m_instance;
			}
		}

		public static void Save()
		{
			if ( m_instance == null ) return;
		
			var json = JsonUtility.ToJson( m_instance );
			EditorPrefs.SetString( KEY, json );
		}
	}
}