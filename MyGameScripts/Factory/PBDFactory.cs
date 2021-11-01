using UnityEngine;
using System.Collections;

public static class PBDFactory
{
	private static bool   		 m_bLoadFromResource = true;
	private static ICharacterFactory m_CharacterFactory = null;
	private static IAssetFactory 	 m_AssetFactory = null;
	private static IAttrFactory      m_AttrFactory = null;
	

	public static IAssetFactory GetAssetFactory()
	{
		if( m_AssetFactory == null)
		{
			if( m_bLoadFromResource)
				m_AssetFactory = new ResourceAssetProxyFactory(); 
			
		}
		return m_AssetFactory;
	}

	public static ICharacterFactory GetCharacterFactory()
	{
		if( m_CharacterFactory == null)		
			m_CharacterFactory = new CharacterFactory();
		return m_CharacterFactory;
	}

	public static IAttrFactory GetAttrFactory()
	{
		if( m_AttrFactory == null)		
			m_AttrFactory = new AttrFactory();
		return m_AttrFactory;
	}	
}
