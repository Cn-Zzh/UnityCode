using UnityEngine;
using System.Collections.Generic;

public abstract class IAIState 
{
	protected ICharacterAI m_CharacterAI = null;
	
	public IAIState()
	{}

	public void SetCharacterAI(ICharacterAI CharacterAI)
	{
		m_CharacterAI = CharacterAI;
	}			

	

	public abstract void Update(IHero hero);

	public virtual void RemoveTarget(ICharacter Target)
	{}

}
