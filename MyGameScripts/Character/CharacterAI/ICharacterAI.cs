using UnityEngine;
using System.Collections.Generic;

public abstract class ICharacterAI 
{
	protected ICharacter m_Character = null;
	protected float		m_AttackRange = 3;
	protected IAIState  m_AIState = null;

	protected const float ATTACK_COOLD_DOWN = 1f; 
	protected float m_CoolDown = ATTACK_COOLD_DOWN;

	public ICharacterAI( ICharacter Character)
	{
		m_Character = Character;
	}

	public virtual void ChangeAIState( IAIState NewAIState)
	{
		m_AIState = NewAIState;
		m_AIState.SetCharacterAI( this );
	}
	public ICharacter GetCharacter()
    {
		return m_Character;
    }
	public virtual void Attack( ICharacter Target )
	{
		m_CoolDown -= Time.deltaTime;
		if( m_CoolDown >0)
			return ;
		m_CoolDown = ATTACK_COOLD_DOWN;

		Target.UnderAttack(m_Character);
		m_Character.Attack( Target );
	}
	public bool TargetInAttackRange( ICharacter Target )
	{
		float dist = Vector3.Distance( m_Character.GetPosition() , 
		                               Target.GetPosition() );
		return ( dist <= m_AttackRange );
	}

	public Vector3 GetPosition()
	{
		return m_Character.GetGameObject().transform.position;
	}
	public void Idle()
    {
		m_Character.Idle();
    }
	public void MoveTo( Vector3 Position )
	{
		m_Character.MoveTo( Position );
	}

	public void StopMove()
	{
		m_Character.StopMove();
	}

	public void Killed()
	{
		m_Character.Killed();
	}

	public bool IsKilled()
	{
		return m_Character.IsKilled(); 
	}

	public void RemoveAITarget( ICharacter Target ) 
	{
		m_AIState.RemoveTarget( Target);  
	}

	public void Update(IHero hero)
	{
		m_AIState.Update( hero );
	}




}
