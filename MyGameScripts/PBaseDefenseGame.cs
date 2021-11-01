using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PBaseDefenseGame
{
	private static PBaseDefenseGame _instance;
	public static PBaseDefenseGame Instance
	{
		get
		{
			if (_instance == null)			
				_instance = new PBaseDefenseGame();
			return _instance;
		}
	}

	private bool m_bGameOver = false;
	
	private GameEventSystem m_GameEventSystem = null;	 
	private CampSystem m_CampSystem	 = null; 			 
	private CharacterSystem m_CharacterSystem = null;   
	private List<IWeapon> weapons = new List<IWeapon>();
	private bool flag = true;
	private float CoolDown = 1.5f;
		
	private PBaseDefenseGame()
	{}

	public void Initinal()
	{
		m_bGameOver = false;
		m_GameEventSystem = new GameEventSystem(this);
		m_CampSystem = new CampSystem(this);			
		m_CharacterSystem = new CharacterSystem(this);
		m_CharacterSystem.HeroBuild();
		m_CampSystem.Init();
		weapons.Add(null);
		weapons.Add(new WeaponSpear());
		weapons.Add(new WeaponKnife());
		weapons.Add(new WeaponSword());
		for(int i = 1;i < 4; i++)
        {
			weapons[i].SetWeaponAttr(PBDFactory.GetAttrFactory().GetWeaponAttr(i));
        }
		m_CharacterSystem.GetHero().SetWeapon(weapons[1]);
		ResigerGameEvent();
	}
	private void ResigerGameEvent()
	{
		m_GameEventSystem.RegisterObserver( ENUM_GameEvent.EnemyKilled, new EnemyKilledObserverUI(this));

	}

	public void Release()
	{
		m_GameEventSystem.Release();
		m_CampSystem.Release();
		m_CharacterSystem.Release();
		UITool.ReleaseCanvas();
	}

	public void Update()
	{
		InputProcess();

		m_GameEventSystem.Update();
		m_CampSystem.Update();
		m_CharacterSystem.Update();
	}

	private void InputProcess()
	{
        if (flag)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
				m_CharacterSystem.GetHero().SetWeapon(weapons[1]);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				m_CharacterSystem.GetHero().SetWeapon(weapons[2]);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				m_CharacterSystem.GetHero().SetWeapon(weapons[3]);
			}
			if (Input.GetMouseButtonDown(0))
			{
				m_CharacterSystem.GetHero().HeroAttack();
				flag = false;
				CoolDown = 1.5f;
				return;
			}
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");
			if (h != 0 || v != 0)
			{
				Vector3 target = m_CharacterSystem.GetHero().GetPosition() + Camera.main.transform.forward * v + Camera.main.transform.right * h;
				m_CharacterSystem.GetHero().MoveTo(target);
				m_CharacterSystem.GetHero().GetGameObject().transform.forward = target - m_CharacterSystem.GetHero().GetGameObject().transform.position - new Vector3(0, target.y - m_CharacterSystem.GetHero().GetGameObject().transform.position.y, 0);
				return;
			}
			m_CharacterSystem.GetHero().Idle();
		}
        else
        {
			CoolDown -= Time.deltaTime;
			if(CoolDown <= 0)
            {
				flag = true;
            }
        }
	}
	
	public bool ThisGameIsOver()
	{
		return m_bGameOver;
	}

	public void ChangeToMainMenu()
	{
		m_bGameOver = true;
	}

	
	public void SetHero(IHero hero)
    {
		if (m_CharacterSystem != null)
			m_CharacterSystem.SetHero(hero);
	}
	public void AddEnemy( IEnemy theEnemy)
	{
		if( m_CharacterSystem !=null)
			m_CharacterSystem.AddEnemy( theEnemy );
	}

	public void RemoveEnemy( IEnemy theEnemy)
	{
		if( m_CharacterSystem !=null)
			m_CharacterSystem.RemoveEnemy( theEnemy );
	}

	public int GetEnemyCount()
	{
		if( m_CharacterSystem !=null)
			return m_CharacterSystem.GetEnemyCount();
		return 0;
	}


	public void RunCharacterVisitor(ICharacterVisitor Visitor)
	{
		m_CharacterSystem.RunVisitor( Visitor );
	}

	public void RegisterGameEvent( ENUM_GameEvent emGameEvent, IGameEventObserver Observer)
	{
		m_GameEventSystem.RegisterObserver( emGameEvent , Observer );
	}

	public void NotifyGameEvent( ENUM_GameEvent emGameEvent, System.Object Param )
	{
		m_GameEventSystem.NotifySubject( emGameEvent, Param);
	}

	


}
