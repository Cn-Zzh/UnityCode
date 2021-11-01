using UnityEngine;
using System.Collections.Generic;

public class ResourceAssetProxyFactory : IAssetFactory
{
	private ResourceAssetFactory m_RealFactory = null; 
	private Dictionary<string,UnityEngine.Object> m_Objects = null;
	private Dictionary<string,Sprite> 			  m_Sprites = null;
	
	public ResourceAssetProxyFactory()
	{
		m_RealFactory =  new ResourceAssetFactory();
		m_Objects = new Dictionary<string,UnityEngine.Object>();
		m_Sprites = new Dictionary<string,Sprite>();
	}
	
	public override GameObject Load( string AssetName )
	{
		if(m_Objects.ContainsKey( AssetName )==false)
		{
			UnityEngine.Object res = m_RealFactory.LoadGameObjectFromResourcePath(AssetName );
			m_Objects.Add ( AssetName, res);
		}
		return  UnityEngine.Object.Instantiate(m_Objects[AssetName] ) as GameObject;
	}
	
	public override Sprite LoadSprite(string SpriteName)
	{
		if( m_Sprites.ContainsKey( SpriteName )==false)
		{
			Sprite res = m_RealFactory.LoadSprite( SpriteName );
			m_Sprites.Add ( SpriteName, res );
		}
		return m_Sprites[SpriteName];
	}
}
