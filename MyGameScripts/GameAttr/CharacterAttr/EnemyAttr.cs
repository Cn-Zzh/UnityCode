using UnityEngine;
using System.Collections;

public class EnemyAttr : ICharacterAttr
{
	protected Vector3 pos = Vector3.zero; 

	public EnemyAttr()
	{}

	public void SetEnemyAttr(EnemyBaseAttr EnemyBaseAttr)
	{
		base.SetBaseAttr( EnemyBaseAttr );

		pos = new Vector3(Random.Range(-100, 100), 50, Random.Range(-100, 100));
	}
	

}
