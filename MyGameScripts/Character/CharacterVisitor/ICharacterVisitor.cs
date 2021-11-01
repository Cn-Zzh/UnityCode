using UnityEngine;
using System.Collections;

// 角色Visitor介面
public abstract class ICharacterVisitor
{
	public virtual void VisitCharacter(ICharacter Character)
	{}

	public virtual void VisitSoldier(IHero Soldier)
	{
		VisitCharacter( Soldier );
	}

	
	public virtual void VisitEnemy(IEnemy Enemy)
	{
		VisitCharacter( Enemy );
	}

	public virtual void VisitEnemyElf(EnemyChest Elf)
	{
		VisitEnemy( Elf );
	}

	public virtual void VisitEnemyTroll(EnemyChushou Troll)
	{
		VisitEnemy( Troll );
	}

	public virtual void VisitEnemyOgre(EnemySlime Ogre)
	{
		VisitEnemy( Ogre );
	}

	public virtual void VisitEnemyTor(EnemyTortoise Ogre)
	{
		VisitEnemy(Ogre);
	}
}
