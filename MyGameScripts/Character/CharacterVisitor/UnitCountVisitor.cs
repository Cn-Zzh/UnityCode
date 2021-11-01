using UnityEngine;
using System.Collections;

// 各單位計數訪問者
public class UnitCountVisitor : ICharacterVisitor 
{
	// 所有單位的計數器
	public int CharacterCount = 0;
	public int SoldierCount = 0;
	public int EnemyCount = 0;
	public int EnemyElfCount = 0;
	public int EnemyTrollCount = 0;
	public int EnemyOgreCount = 0;
	public int EnemyTorCount = 0;
		
	public override void VisitCharacter		(ICharacter Character)
	{
		base.VisitCharacter(Character);
		CharacterCount++;
	}
	
	public override void VisitSoldier		(IHero Soldier)
	{
		base.VisitSoldier(Soldier);
		SoldierCount++;
	}
	
	
	public override void VisitEnemy			(IEnemy Enemy)
	{
		base.VisitEnemy(Enemy);
		EnemyCount++;
	}
	
	public override void VisitEnemyElf		(EnemyChest Elf)
	{
		base.VisitEnemyElf(Elf);
		EnemyElfCount++;
	}
	
	public override void VisitEnemyTroll		(EnemyChushou Troll)
	{
		base.VisitEnemyTroll(Troll);
		EnemyTrollCount++;
	}
	
	public override void VisitEnemyOgre		(EnemySlime Ogre)
	{
		base.VisitEnemyOgre(Ogre);
		EnemyOgreCount++;
	}

    public override void VisitEnemyTor(EnemyTortoise Ogre)
    {
        base.VisitEnemyTor(Ogre);
		EnemyTorCount++;
    }

    public void Reset()
	{
		CharacterCount = 0;
		SoldierCount = 0;
		EnemyCount = 0;
		EnemyElfCount = 0;
		EnemyTrollCount = 0;
		EnemyOgreCount = 0;			
		EnemyTorCount = 0;
	}

	
	
	// 取得Enemy兵種的數量
	public int GetUnitCount( ENUM_Enemy emEnemy)
	{
		switch( emEnemy)
		{
		case ENUM_Enemy.Null:
			return EnemyCount;
		case ENUM_Enemy.Chest:
			return EnemyElfCount;
		case ENUM_Enemy.Chushou:
			return EnemyTrollCount;
		case ENUM_Enemy.Slime:
			return EnemyOgreCount;
		case ENUM_Enemy.Tortoise:
			return EnemyTorCount;
		default: 
		Debug.LogWarning("GetUnitCount:沒有["+emEnemy+"]可以對映的計算方式");
		break;
		}
		return 0;
	}

}
