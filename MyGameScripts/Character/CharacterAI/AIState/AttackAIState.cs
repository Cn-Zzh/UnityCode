using UnityEngine;
using System.Collections.Generic;

public class AttackAIState : IAIState 
{

	private ICharacter m_AttackTarget = null;

	public AttackAIState( ICharacter AttackTarget )
	{
		m_AttackTarget = AttackTarget;
	}			

	public override void Update(IHero target)
	{
		if(m_CharacterAI.TargetInAttackRange(target) == false)
        {
			m_CharacterAI.ChangeAIState(new ChaseAIState(target));
			target.RemoveEnemy(m_CharacterAI.GetCharacter());
			return;
		}
		if (m_AttackTarget == null || m_AttackTarget.IsKilled() || Vector3.Distance(target.GetPosition(), m_CharacterAI.GetPosition()) > 10)
		{
			m_CharacterAI.ChangeAIState(new IdleAIState());
			target.RemoveEnemy(m_CharacterAI.GetCharacter());
			return;
		}
		m_CharacterAI.Attack( m_AttackTarget );
		
	}
	public override void RemoveTarget(ICharacter Target)
	{
		if (m_AttackTarget.GetGameObject().name == Target.GetGameObject().name)
			m_AttackTarget = null;
	}

}
