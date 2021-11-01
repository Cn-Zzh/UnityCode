using UnityEngine;
using System.Collections;

public class EnemySlime  : IEnemy
{
	public EnemySlime()
	{
		m_emEnemyType = ENUM_Enemy.Slime;
		m_AssetName = "Slime";
		m_AttrID   = 3;
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
