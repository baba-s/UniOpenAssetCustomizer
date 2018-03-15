using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;

namespace KoganeEditorLib
{
	public static class OpenAssetCustomizer
	{
		[OnOpenAsset]
		private static bool OnOpenAsset( int instanceID, int line )
		{
			var assetPath = AssetDatabase.GetAssetPath( instanceID );
			var assetExtension = Path
				.GetExtension( assetPath )
				.Replace( ".", string.Empty )
			;

			var scriptPath = AssetDatabase
				.GetAllAssetPaths()
				.FirstOrDefault( c => c.EndsWith( "OpenAssetCustomizer.cs" ) )
			;
			var dir = Path.GetDirectoryName( scriptPath );
			var path = string.Format( "{0}/Settings.asset", dir );
			var settings = AssetDatabase.LoadAssetAtPath<OpenAssetSettings>( path );
			var list = settings.List;
			var data = list.FirstOrDefault( c => c.Extension == assetExtension );

			if ( data != null )
			{
				var process = Process.Start( data.ApplicationPath, assetPath );
				return true;
			}

			return false;
		}
	}
}