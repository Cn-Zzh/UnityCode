using UnityEngine;
using System.Collections;

public class EnemyTortoise : IEnemy
{
	public EnemyTortoise()
	{
		m_emEnemyType = ENUM_Enemy.Tortoise;
		m_AssetName = "Tortoise";
		m_AttrID = 4;
	}

	// 播放音效
	public override void DoPlayHitSound()
	{
		//Debug.Log ("EnemyElf.PlayHitSound");
	}

	public override void DoShowHitEffect()
	{

	}

	public override void RunVisitor(ICharacterVisitor Visitor)
	{
		
	}
}
