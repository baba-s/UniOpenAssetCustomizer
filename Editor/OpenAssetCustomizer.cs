using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Kogane.Internal
{
	internal static class OpenAssetCustomizer
	{
		[OnOpenAsset]
		private static bool OnOpenAsset( int instanceID, int line )
		{
			var settings = OpenAssetSettings.Instance;

			if ( settings == null ) return false;

			var assetPath = AssetDatabase.GetAssetPath( instanceID );

			if ( string.IsNullOrWhiteSpace( assetPath ) ) return false;

			var assetExtension = Path
					.GetExtension( assetPath )
					.Replace( ".", string.Empty )
				;

			var list = settings.List;
			var data = list.FirstOrDefault( c => c.Extension == assetExtension );

			if ( data == null ) return false;

			Process.Start( data.ApplicationPath, assetPath );
			return true;
		}
	}
}