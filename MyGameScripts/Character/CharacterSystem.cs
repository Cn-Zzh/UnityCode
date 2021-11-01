using UnityEngine;
using System.Collections.Generic;

// 管理創建出來的角色
public class CharacterSystem : IGameSystem
{
	private IHero Hero = new IHero();
	private List<ICharacter> m_Enemys = new List<ICharacter>();

	public CharacterSystem(PBaseDefenseGame PBDGame):base(PBDGame)
	{
		Initialize();

	}

	#region 角色管理
	public void HeroBuild()
    {
		Hero.Init();
	}
	public void SetHero(IHero hero)
    {
		Hero = hero;
    }

	public IHero GetHero()
    {
		return Hero;
    }
	// 增加Enemy
	public void AddEnemy( IEnemy theEnemy)
	{
		m_Enemys.Add( theEnemy );
	}
	
	// 移除Enemy
	public void RemoveEnemy( IEnemy theEnemy)
	{
		m_Enemys.Remove( theEnemy );
	}

	UnitCountVisitor m_UnitCountVisitor =  new UnitCountVisitor();
	public int GetEnemyCount()
	{
		m_UnitCountVisitor.Reset();
		RunVisitor( m_UnitCountVisitor );
		return m_UnitCountVisitor.EnemyCount;

	}

	public void RunVisitor(ICharacterVisitor Visitor)
	{
		foreach( ICharacter Character in m_Enemys)
			Character.RunVisitor( Visitor);
	}
	#endregion

	#region 更新
	// 更新
	public override void Update()	
	{
		UpdateCharacter();
		UpdateAI(); // 更新AI
		UpdateUI();
		RemoveCharacter();
	}
	public void RemoveCharacter()
	{
		RemoveCharacter(Hero, m_Enemys, ENUM_GameEvent.SoldierKilled);
		RemoveCharacter(m_Enemys, Hero, ENUM_GameEvent.EnemyKilled);
	}
	public void RemoveCharacter(ICharacter Character, List<ICharacter> Opponents, ENUM_GameEvent emEvent)
	{
		if (Character.IsKilled() == false)
			return;

		m_PBDGame.ChangeToMainMenu();
	}
	public void RemoveCharacter(List<ICharacter> Characters, ICharacter Opponent, ENUM_GameEvent emEvent)
	{
		List<ICharacter> CanRemoves = new List<ICharacter>();
		foreach (ICharacter Character in Characters)
		{
			if (Character.IsKilled() == false)
				continue;


			else
				CanRemoves.Add(Character);
		}

		foreach (ICharacter CanRemove in CanRemoves)
		{
			Hero.RemoveEnemy(CanRemove);
			Hero.LevelUp();
			CanRemove.Release();
			Characters.Remove(CanRemove);
		}
	}

	private void UpdateCharacter()
	{
		Hero.Update();
		foreach( ICharacter Character in m_Enemys)
			Character.Update();
	}
	private void UpdateUI()
    {
		UpdateUI(m_Enemys, Hero);
    }

	private void UpdateUI(List<ICharacter> Characters, IHero hero)
	{
		foreach (ICharacter Character in Characters)
        {
			Character.UpdateUI();
        }
		hero.UpdateUI();
	}
	private void UpdateAI()
	{
		UpdateAI(m_Enemys, Hero );
	}
	
	private void UpdateAI( List<ICharacter> Characters, IHero hero)
	{
        foreach (ICharacter Character in Characters)
        {
			Character.UpdateAI(hero);
			//
		}
            
    }
	
	#endregion



}
