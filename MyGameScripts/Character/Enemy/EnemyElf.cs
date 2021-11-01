using UnityEngine;
using System.Collections;

public class EnemyChest  : IEnemy
{
	public EnemyChest()
	{
		m_emEnemyType = ENUM_Enemy.Chest;
		m_AssetName = "Chest";
		m_AttrID   = 1;
	}
	
	public override void DoPlayHitSound()
	{
		
	}
	
	public override void DoShowHitEffect()
	{
		
	}

	public override void RunVisitor(ICharacterVisitor Visitor)
	{
		
	}
}
