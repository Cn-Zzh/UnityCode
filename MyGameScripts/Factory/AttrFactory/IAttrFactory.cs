using UnityEngine;
using System.Collections;

public abstract class IAttrFactory
{
	public abstract HeroAttr GetSoldierAttr();

	    
	public abstract EnemyAttr GetEnemyAttr(int AttrID);


	public abstract WeaponAttr GetWeaponAttr(int AttrID);
}
