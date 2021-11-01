using UnityEngine;
using System.Collections;

public abstract class IAssetFactory
{
	public abstract GameObject Load( string AssetName );

	
	public abstract Sprite	   LoadSprite(string SpriteName);
	
}
