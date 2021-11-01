using UnityEngine;
using System.Collections.Generic;


public abstract class ICharacter
{
	protected string m_Name = "";				
	
	protected GameObject m_GameObject = null;	
	protected Transform m_Transform = null;	
	protected UnityEngine.AI.NavMeshAgent m_NavAgent = null;
	protected Animator animator = null;

	protected string 	m_AssetName = "";		
	protected int		m_AttrID   = 0;			
		
	protected bool m_bKilled = false;			
	protected bool m_bCheckKilled = false;		
	protected float m_RemoveTimer = 1.5f;		
	protected bool m_bCanRemove = false;		

	protected ICharacterAttr m_Attribute = null;
	protected ICharacterAI m_AI = null;
	protected HPUI m_UI = null;
	private IWeapon m_Weapon = null;
	private GameObject WeaponObj = null;



	public ICharacter(){}

	public void SetGameObject( GameObject theGameObject )
	{
		m_GameObject = theGameObject ;
		m_NavAgent = m_GameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
		animator = m_GameObject.GetComponent<Animator>();
		if(m_GameObject.name == "Hero")
        {
			m_Transform = m_GameObject.transform.Find("Bip001").Find("Bip001 Prop1");
        }
	}

	public GameObject GetGameObject()
	{
		return m_GameObject;
	}

	public void Release()
	{
		if( m_GameObject != null)
			GameObject.Destroy( m_GameObject);
	}

	public string GetName()
	{
		return m_Name;
	}

	public string GetAssetName()
	{
		return m_AssetName;
	}

	public ICharacterAI GetAI()
    {
		return m_AI;
    }
	public void SetWeapon(IWeapon Weapon)
	{
		m_Weapon = Weapon;
		Debug.Log("当前持有武器为：" + m_Weapon.GetWeaponAttr().AttrName + "武器加成攻击力为：" + m_Weapon.GetWeaponAttr().Atk);
		m_Weapon.SetOwner(this);
		if(WeaponObj != null)
        {
			GameObject.Destroy(WeaponObj);
        }
		WeaponObj = GameObject.Instantiate(m_Weapon.GetGameObject(), m_Transform, false);
	}
	public void SetUI(HPUI hpUI)
    {
		m_UI = hpUI;
    }

	public int GetAttrID()
	{
		return m_AttrID;
	}

	public ICharacterAttr GetCharacterAttr()
	{
		return m_Attribute;
	}
		
	public string GetCharacterName()
	{
		return m_Name;	
	}
	public void Update()
	{
		if( m_bKilled)
		{
			m_RemoveTimer -= Time.deltaTime;
			if( m_RemoveTimer <=0 )
				m_bCanRemove = true;
		}
		
	}

	#region 移動及位置	
	public void MoveTo( Vector3 Position )
	{
		m_NavAgent.isStopped = false;
		animator.SetFloat("Blend", 1);
		m_NavAgent.SetDestination( Position );
	}
	public void Idle()
    {
		animator.SetFloat("Blend", 0);
	}
	public void StopMove()
	{		
		m_NavAgent.isStopped = true;
	}

	public Vector3 GetPosition()
	{
		return m_GameObject.transform.position;
	}
	#endregion



	public void SetAI(ICharacterAI CharacterAI)
	{
		m_AI = CharacterAI;
	}
	public int GetAtkValue()
	{
		if(m_Weapon != null)
		return m_Weapon.GetAtkValue() + m_Attribute.GetMoveSpeed();
		return m_Attribute.GetMoveSpeed();
	}
	public void UpdateAI(IHero Targets)
	{
		m_AI.Update(Targets);
	}
	public void UpdateUI()
    {
		m_UI.Update();
    }

	#region 攻擊
	public void Attack( ICharacter Target)
	{

		animator.SetFloat("Blend", 2);

		m_GameObject.transform.forward = Target.GetPosition() - GetPosition();
	}
	public void Attack()
	{

		animator.SetFloat("Blend", 2);

	}
	public abstract void UnderAttack( ICharacter Attacker);
	#endregion

	

	#region 陣亡及移除
	public void Killed()
	{
		if( m_bKilled == true)
			return;
		m_bKilled = true;
		m_bCheckKilled = false;
	}

	public bool IsKilled()
	{
		return m_bKilled; 
	}

	public bool CheckKilledEvent()
	{
		if( m_bCheckKilled)
			return true;
		m_bCheckKilled = true;
		return false;
	}

	public void RemoveAITarget(ICharacter Targets)
	{
		m_AI.RemoveAITarget(Targets);
	}

	public bool CanRemove()
	{
		return m_bCanRemove;
	}
	#endregion

	#region 角色數值
	public virtual void SetCharacterAttr( ICharacterAttr CharacterAttr)
	{
		m_Attribute = CharacterAttr;
		m_Attribute.InitAttr ();
		m_NavAgent.speed = m_Attribute.GetMoveSpeed();
		m_Name = m_Attribute.GetAttrName();
	}
	#endregion

	
	public virtual void RunVisitor(ICharacterVisitor Visitor)
	{
		Visitor.VisitCharacter(this);
	}
			

} 




