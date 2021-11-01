using UnityEngine;
using System.Collections.Generic;

public abstract class ICamp 
{
	protected ENUM_Enemy m_Enemy = ENUM_Enemy.Null;

	protected List<ITrainCommand> m_TrainCommands = new List<ITrainCommand>();
	protected float m_CommandTimer = 0; 
	protected float m_TrainCoolDown = 5;

	protected PBaseDefenseGame m_PBDGame = null;


	public ICamp()
	{
	}

	public void SetPBaseDefenseGame(PBaseDefenseGame PBDGame) 
	{
		m_PBDGame = PBDGame;
	}

	protected void AddTrainCommand(ITrainCommand Command)
	{
		m_TrainCommands.Add(Command);
	}
	public void RunCommand()
	{
		if (m_TrainCommands.Count == 0)
			return;

		m_CommandTimer -= Time.deltaTime;
		if (m_CommandTimer > 0)
			return;
		m_CommandTimer = m_TrainCoolDown;

        for(int i = m_TrainCommands.Count - 1;i >= 0; i--)
        {
			m_TrainCommands[i].Execute();
			m_TrainCommands.RemoveAt(i);
        }
	}

	public abstract void Train();
}
