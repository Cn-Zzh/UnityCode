using UnityEngine;
using System.Collections.Generic;

public class IdleAIState : IAIState 
{

	public IdleAIState()
	{
		
	}			


	public override void Update(IHero target)
	{
		if(Vector3.Distance(target.GetPosition(), m_CharacterAI.GetPosition()) <= 10)
        {
			m_CharacterAI.ChangeAIState(new ChaseAIState(target));
			return;
		}
		m_CharacterAI.Idle();
	}
}
