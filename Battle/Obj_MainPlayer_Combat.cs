/********************************************************************
	文件名: 	\Main\Project\Client\Assets\MLDJ\Script\Obj\Obj_Character_Combat.cs
	创建时间:	2021/09/13
	全路径:	\Main\Project\Client\Assets\MLDJ\Script\Obj
	创建人:		孙赫飞
	功能说明:	主角战斗相关
	修改记录:
*********************************************************************/

using System.Collections.Generic;
using Games.ImpactModle;
using UnityEngine;

namespace Games.LogicObj
{
	public partial class Obj_MainPlayer
	{
		GameObject m_selectTarget;
		public void UseSkillOpt(int nSkillId, GameObject skillBtn)
		{

			if (CheckBeforUseSkill(nSkillId) == false)//各种条件的检查
			{
				return;
			}

			GameObject TargetObjChar = null; //攻击目标  这里目标可以自己写一个实体
			//FixMe 这里通过技能索引 进行查找表，找到技能信息
			
			
			//所有的查询都是基于ID
			if (m_selectTarget != null)// 当前有选择目标且不是自己 就不重新选择了
			{
				TargetObjChar = m_selectTarget;

#if UNITY_ANDROID && !UNITY_EDITOR
				
				if (!m_selectTarget.gameObject.activeSelf)
				{
					TargetObjChar = null;
				}
#endif
			}
			//          
			//需要重新选择目标 攻击目标的各种判断
			if (TargetObjChar == null/* ||TargetObjChar.CurObjAnimState==GameDefine_Globe.OBJ_ANIMSTATE.STATE_ATTACKFLY */)
			{
				//判断技能实体是否存在（自己写技能实体类）
				//判断满足条件后,发包给服务器
				OnSelectTarget(null, false);

			}

			OnEnterCombat(TargetObjChar);

		}
		bool CheckBeforUseSkill(int nSkillId)
		{
			//各种条件的判断 什么情况不能使用技能
			//技能是否在冷却中
			//读取技能信息
			//检测消耗类型
			return true;
		}
		public  void OnEnterCombat(GameObject Target)
		{
			//无技能 默认使用普攻
			

			

		}
		public void OnSelectTarget(GameObject targetObj, bool isMoveAgainSelect = true)
		{
			//FixMe
			//如果targetObj为空，则进行取消选择逻辑
			//如果之前已经选择，则移动过去
			//判断是否有目标,是否需要向目标移动
			//如果选择的目标在播放技能范围的特效 切换目标时得 修改特效播放的对象
			
			//发包给服务器，走网络框架的流程
			
		}

	}
		
}
