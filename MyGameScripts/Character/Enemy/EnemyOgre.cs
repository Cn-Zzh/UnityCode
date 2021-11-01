using UnityEngine;
using System.Collections;

public class EnemyChushou  : IEnemy
{
	public EnemyChushou()
	{
		m_emEnemyType = ENUM_Enemy.Chushou;
		m_AssetName = "Chushou";
		m_AttrID   = 2;
	}
	
	public override void DoPlayHitSound()
	{
		//Debug.Log ("EnemyOgre.PlayHitSound");
	}
	
	public override void DoShowHitEffect()
	{
		
	}

	public override void RunVisitor(ICharacterVisitor Visitor)
	{
		
	}
}
