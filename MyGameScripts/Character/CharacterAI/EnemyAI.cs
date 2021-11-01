using UnityEngine;
using System.Collections.Generic;

public class EnemyAI : ICharacterAI 
{

	

	public EnemyAI(ICharacter Character):base(Character)
	{

		ChangeAIState(new IdleAIState());
	}

	public override void ChangeAIState( IAIState NewAIState)
	{
		base.ChangeAIState( NewAIState);
	}
	
	
}
