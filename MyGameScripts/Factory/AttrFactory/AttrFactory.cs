using UnityEngine;
using System.Collections.Generic;

public class AttrFactory : IAttrFactory
{
	private CharacterBaseAttr Hero;
	private Dictionary<int,EnemyBaseAttr> 	m_EnemyAttrDB 	= null;
	private Dictionary<int, WeaponAttr> m_WeaponAttrDB = null;

	public AttrFactory()
	{
		InitSoldierAttr();
		InitEnemyAttr();
		InitWeaponAttr();
	}

	private void InitSoldierAttr()
	{
		Hero = new CharacterBaseAttr(100, 10, "Hero");
	}

	private void InitEnemyAttr()
	{
		m_EnemyAttrDB 	= new Dictionary<int,EnemyBaseAttr>();
		m_EnemyAttrDB.Add (  1, new EnemyBaseAttr(100, 2,"Chest") );
		m_EnemyAttrDB.Add (  2, new EnemyBaseAttr(150,3,"Chushou") ); 
		m_EnemyAttrDB.Add (  3, new EnemyBaseAttr(200,4,"Slime") );
		m_EnemyAttrDB.Add (  4, new EnemyBaseAttr(250,5,"Tortoise") );
	}

	private void InitWeaponAttr()
	{
		m_WeaponAttrDB = new Dictionary<int, WeaponAttr>();
		m_WeaponAttrDB.Add(1, new WeaponAttr(10, "Spear"));
		m_WeaponAttrDB.Add(2, new WeaponAttr(40, "Knife"));
		m_WeaponAttrDB.Add(3, new WeaponAttr(999, "Sword"));
	}


	public override HeroAttr GetSoldierAttr( )
	{
		HeroAttr NewAttr = new HeroAttr();
        NewAttr.SetSoldierAttr(Hero);
        return NewAttr;
	}


	public override EnemyAttr GetEnemyAttr( int AttrID )
	{
		if( m_EnemyAttrDB.ContainsKey( AttrID )==false)
		{
			Debug.LogWarning("GetEnemyAttr:AttrID["+AttrID+"]數值不存在");
			return null;
		}
		
		EnemyAttr NewAttr = new EnemyAttr();
		NewAttr.SetEnemyAttr( m_EnemyAttrDB[AttrID]);		
		return NewAttr;
	}

	public override WeaponAttr GetWeaponAttr(int AttrID)
	{
		if (m_WeaponAttrDB.ContainsKey(AttrID) == false)
		{
			Debug.LogWarning("GetWeaponAttr:AttrID[" + AttrID + "]數值不存在");
			return null;
		}
		return m_WeaponAttrDB[AttrID];
	}


}