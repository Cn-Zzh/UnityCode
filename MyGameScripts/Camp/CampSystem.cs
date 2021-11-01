using UnityEngine;
using System.Collections.Generic;

// 兵營系統
public class CampSystem : IGameSystem
{
	private Dictionary<ENUM_Enemy, ICamp> m_EnemyCamps = new Dictionary<ENUM_Enemy,ICamp>();

	public CampSystem(PBaseDefenseGame PBDGame):base(PBDGame)
	{
		Initialize();
	}
	public void Init()
    {
		foreach (var item in m_EnemyCamps.Values)
		{
			for (int i = 0; i < 10; i++)
			{
				item.Train();
			}
		}
	}
	public override void Initialize()
	{
		m_EnemyCamps.Add (ENUM_Enemy.Chest, SoldierCampFactory(ENUM_Enemy.Chest));
		m_EnemyCamps.Add (ENUM_Enemy.Chushou, SoldierCampFactory( ENUM_Enemy.Chushou));
		m_EnemyCamps.Add (ENUM_Enemy.Slime, SoldierCampFactory(ENUM_Enemy.Slime));
		m_EnemyCamps.Add (ENUM_Enemy.Tortoise, SoldierCampFactory(ENUM_Enemy.Tortoise));
        

	}
	private EnemyCamp SoldierCampFactory(ENUM_Enemy enemy)
	{

		EnemyCamp NewCamp = new EnemyCamp(enemy);
		NewCamp.SetPBaseDefenseGame(m_PBDGame);


		return NewCamp;
	}

	public override void Update()
	{
		foreach (EnemyCamp Camp in m_EnemyCamps.Values)
			Camp.RunCommand();
	}
	
	
}
