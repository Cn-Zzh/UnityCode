using UnityEngine;
using System.Collections;

public abstract class ICharacterFactory
{
	public abstract IHero CreateHero(int Lv);
	
	public abstract IEnemy CreateEnemy( ENUM_Enemy emEnemy);
	
}

