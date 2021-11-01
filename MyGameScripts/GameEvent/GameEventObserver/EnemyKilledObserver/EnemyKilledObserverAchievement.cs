using UnityEngine;
using System.Collections;

// 成就觀測Enemey陣亡事件
public class EnemyKilledObserverAchievement : IGameEventObserver 
{
	private EnemyKilledSubject m_Subject = null;
	

	// 設定觀察的主題
	public override	void SetSubject( IGameEventSubject Subject )
	{
		m_Subject = Subject as EnemyKilledSubject;
	}
	
	// 通知Subject被更新
	public override void Update()
	{
		
	}
	
}