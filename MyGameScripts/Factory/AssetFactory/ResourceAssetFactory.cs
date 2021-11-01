using UnityEngine;
using System.Collections;

public class ResourceAssetFactory : IAssetFactory 
{
	

	public override GameObject Load( string AssetName )
	{	
		return InstantiateGameObject( AssetName );
	}


	public override Sprite LoadSprite(string SpriteName)
	{
		return Resources.Load( SpriteName,typeof(Sprite)) as Sprite;
	}

	private GameObject InstantiateGameObject( string AssetName )
	{
		UnityEngine.Object res = LoadGameObjectFromResourcePath( AssetName );
		if(res==null)
			return null;
		return  UnityEngine.Object.Instantiate(res) as GameObject;
	}

	public UnityEngine.Object LoadGameObjectFromResourcePath( string AssetPath)
	{
		UnityEngine.Object res = Resources.Load(AssetPath);
		if( res == null)
		{
			Debug.LogWarning("無法載入路徑["+AssetPath+"]上的Asset");
			return null;
		}		
		return res;
	}
}
