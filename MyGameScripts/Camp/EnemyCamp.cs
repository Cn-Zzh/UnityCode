using UnityEngine;
using System.Collections;

public class EnemyCamp : ICamp
{
	

	public EnemyCamp(ENUM_Enemy enemy)
	{
		m_Enemy = enemy;
	}

	public override void Train()
	{
		TrainSoldierCommand NewCommand = new TrainSoldierCommand(m_Enemy);
		AddTrainCommand(NewCommand);
	}

}
