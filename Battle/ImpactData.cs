using UnityEngine;
using System.Collections;

namespace Games.ImpactModle
{
    public enum BUFFICON
    {
        MAX_BUFFICONUM = 4, //主角头像显示的最大buff 图标位
    }

    public enum BUFFTYPE
    {
        DEBUFF = 0, //减益BUFF
        BUFF = 1,//增益BUFF
    }
    public struct ClientImpactInfo
    {
        public void CleanUp()
        {
            m_nImpactId = -1;//Buff Id
            m_nImpactLogicId = -1;//Buff 逻辑ID
            m_nIsForever = false;//Buff 是否永久
            m_nRemainTime = 0;//Buff 剩余时间(单位s)
        }

        public bool IsVaild()
        {
            return (m_nImpactId != -1);
        }
        private int m_nImpactId;//Buff Id
        public int ImpactId
        {
            get { return m_nImpactId; }
            set { m_nImpactId = value; }
        }
        private int m_nImpactLogicId;//Buff 逻辑ID
        public int ImpactLogicId
        {
            get { return m_nImpactLogicId; }
            set { m_nImpactLogicId = value; }
        }
        private bool m_nIsForever;//Buff 是否永久
        public bool IsForever
        {
            get { return m_nIsForever; }
            set { m_nIsForever = value; }
        }
        private float m_nRemainTime;//Buff 剩余时间(单位s)
        public float RemainTime
        {
            get { return m_nRemainTime; }
            set { m_nRemainTime = value; }
        }
    }
}
