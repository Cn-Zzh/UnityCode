using UnityEngine;
using System.Collections;
using System.Collections.Generic;



// Soldier角色界面
public class IHero : ICharacter
{
	public int Lv = 1;
	public int newHp = 0;
	public List<ICharacter> enemys = new List<ICharacter>();
	public IHero()
	{
		m_AssetName = "Hero";
	}
	public void Init()
    {

		TrainHeroCommand NewCommand = new TrainHeroCommand();
		NewCommand.Execute();
	}

	public void HeroAttack()
	{
		animator.Play("Attack");
        foreach (var item in enemys)
        {
			item.UnderAttack(m_AI.GetCharacter());
        }
	}

	public void AddEnemy(ICharacter Enemy)
    {
		enemys.Add(Enemy);
    }
	public void LevelUp()
    {
		Lv++;
		newHp += 30;
		HeroAttr Attribute = m_Attribute as HeroAttr;
		Attribute.SetSoldierLv(Lv);
		Attribute.AddMaxHP(newHp);
		Attribute.AddMoveSpeed(5);
		m_Attribute.FullNowHP();
		m_UI.Change(m_Attribute.GetNowHP(),m_Attribute.GetMaxHP());
	}
	public void RemoveEnemy(ICharacter Enemy)
    {
		enemys.Remove(Enemy);
    }
	// 取得目前的角色值
	public HeroAttr GetSoldierValue()
	{
		return m_Attribute as HeroAttr;
	}
	
	// 被武器攻擊
	public override void UnderAttack( ICharacter Attacker )
	{

		m_Attribute.UnderAttack(Attacker);
		Debug.Log(m_AssetName + "剩余血量" + m_Attribute.GetNowHP());
		m_UI.Change(m_Attribute.GetNowHP(), m_Attribute.GetMaxHP());
		// 是否陣亡
		if ( m_Attribute.GetNowHP() <= 0 )
		{
			Killed();			// 陣亡
		}
	}

}