using UnityEngine;
using System.Collections;

public class TrainSoldierCommand : ITrainCommand
{	
	ENUM_Enemy 	m_Enemy;	
		
	public TrainSoldierCommand(ENUM_Enemy enemy)
	{
		m_Enemy = enemy;
	}

	public override void Execute()
	{
		ICharacterFactory Factory = PBDFactory.GetCharacterFactory();
		IEnemy enemy = Factory.CreateEnemy(m_Enemy);
		
		
		IAttrFactory AttrFactory = PBDFactory.GetAttrFactory();
		EnemyAttr PreAttr = AttrFactory.GetEnemyAttr(enemy.GetAttrID());
		enemy.SetCharacterAttr(PreAttr);
	}
}