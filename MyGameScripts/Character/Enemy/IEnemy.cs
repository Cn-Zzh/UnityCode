using UnityEngine;
using System.Collections;


public enum ENUM_Enemy
{
	Null 	= 0,
	Chest	= 1,// 宝箱怪
	Chushou	= 2,// 触手怪
	Slime	= 3,// 史莱姆
	Tortoise = 4,// 乌龟
	Dog = 5, //Boss狗
	Max,
}

// Enemy角色界面
public abstract class IEnemy : ICharacter
{
	protected ENUM_Enemy m_emEnemyType = ENUM_Enemy.Null;

	public IEnemy()
	{}

	public ENUM_Enemy GetEnemyType() 
	{
		return m_emEnemyType;
	}
	
	public override void UnderAttack( ICharacter Attacker)
	{
		m_Attribute.UnderAttack(Attacker);
		Debug.Log(m_AssetName + "剩余血量" + m_Attribute.GetNowHP());
		m_UI.Change(m_Attribute.GetNowHP(), m_Attribute.GetMaxHP());
		if ( m_Attribute.GetNowHP() <= 0 )		
		{
			Killed();
		}
	}

	public override void RunVisitor(ICharacterVisitor Visitor)
	{
		Visitor.VisitEnemy(this);
	}

	public abstract void DoPlayHitSound();
	
	public abstract void DoShowHitEffect();


}
