using System;
using System.Collections.Generic;
using UnityEngine;

namespace KoganeEditorLib
{
	[Serializable]
	public sealed class OpenAssetSettingsData
	{
		[SerializeField] private string m_applicationPath	= null;
		[SerializeField] private string m_extension			= null;

		public string ApplicationPath	{ get { return m_applicationPath	; } }
		public string Extension			{ get { return m_extension			; } }
	}

	public sealed class OpenAssetSettings : ScriptableObject
	{
		[SerializeField] private OpenAssetSettingsData[] m_list = null;

		public IList<OpenAssetSettingsData> List { get { return m_list; } }
	}
}