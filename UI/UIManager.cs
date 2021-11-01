/********************************************************************
	created:	2021/09/12
	filename: 	UIManager.cs
	author:	    孙赫飞
	
	purpose:	UIRoot下的UI管理器，创建不同的根节点来控制不同功能的UI
 *              删除UI禁止使用Destroy 必须使用DestroyUI，否则可能引起内存无线增长
*********************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
//using Games.GlobeDefine;//GameDefine_Globe 全局定义
//using GCGame.Table;//TabManager 读表
//using Module.Log;//Debug 日志

public interface IUIWidget
{
    void OnLoad(params object[] args);
}
/// <summary>
/// 挂载到UIRoot下
/// </summary>
public class UIManager : MonoBehaviour
{
    //private static int m_sCloseUICount = 0;

    public delegate void OnOpenUIDelegate(bool bSuccess, object param);
    public delegate void OnLoadUIItemDelegate(GameObject resItem, object param1);

    private Transform BaseUIRoot;      // 位于UI最底层，常驻场景，基础交互
    private Transform PopUIRoot;       // 位于UI上层，弹出式，互斥
    private Transform StoryUIRoot;     // 故事背景层
    private Transform TipUIRoot;       // 位于UI顶层，弹出重要提示信息等
    private Transform MenuPopUIRoot;    //菜单Pop窗口
    private Transform MessageUIRoot;    //跑马灯消息
    private Transform DeathUIRoot;      //死亡窗口

    private Dictionary<string, GameObject> m_dicTipUI = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> m_dicBaseUI = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> m_dicPopUI = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> m_dicStoryUI = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> m_dicMenuPopUI = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> m_dicMessageUI = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> m_dicDeathUI = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> m_dicCacheUI = new Dictionary<string, GameObject>();

    private Dictionary<string, int> m_dicWaitLoad = new Dictionary<string, int>();

    private static UIManager m_instance;
    public static UIManager Instance()
    {
        return m_instance;
    }
    //GC间隔
    private const int GCCollectTime = 1;

    void Awake()
    {
        m_dicTipUI.Clear();
        m_dicBaseUI.Clear();
        m_dicPopUI.Clear();
        m_dicStoryUI.Clear();
        m_dicMenuPopUI.Clear();
        m_dicMessageUI.Clear();
        m_dicDeathUI.Clear();
        m_dicCacheUI.Clear();
        m_instance = this;

        BaseUIRoot = transform.Find("BaseUIRoot");
        if (null == BaseUIRoot)
        {
            BaseUIRoot = AddObjToRoot("BaseUIRoot").transform;
        }

        PopUIRoot = transform.Find("PopUIRoot");
        if (null == PopUIRoot)
        {
            PopUIRoot = AddObjToRoot("PopUIRoot").transform;
        }

        StoryUIRoot = transform.Find("StoryUIRoot");
        if (null == StoryUIRoot)
        {
            StoryUIRoot = AddObjToRoot("StoryUIRoot").transform;
        }

        TipUIRoot = transform.Find("TipUIRoot");
        if (null == TipUIRoot)
        {
            TipUIRoot = AddObjToRoot("TipUIRoot").transform;
        }

        MenuPopUIRoot = transform.Find("MenuPopUIRoot");
        if (null == MenuPopUIRoot)
        {
            MenuPopUIRoot = AddObjToRoot("MenuPopUIRoot").transform;
        }

        MessageUIRoot = transform.Find("MessageUIRoot");
        if (null == MessageUIRoot)
        {
            MessageUIRoot = AddObjToRoot("MessageUIRoot").transform;
        }

        DeathUIRoot = transform.Find("DeathUIRoot");
        if (null == DeathUIRoot)
        {
            DeathUIRoot = AddObjToRoot("DeathUIRoot").transform;
        }

        BaseUIRoot.gameObject.SetActive(true);
        TipUIRoot.gameObject.SetActive(true);
        PopUIRoot.gameObject.SetActive(true);
        StoryUIRoot.gameObject.SetActive(true);
        MenuPopUIRoot.gameObject.SetActive(true);
        MessageUIRoot.gameObject.SetActive(true);
        DeathUIRoot.gameObject.SetActive(true);
    }


    //原定在物理帧里面进行GC
    //void FixedUpdate()
    //{
    //    if (m_GCTimerGo)
    //    {
    //        if (Time.fixedTime - m_GCWaitTime >= GCCollectTime)
    //        {
    //            Resources.UnloadUnusedAssets();
    //            GC.Collect();

    //            m_GCTimerGo = false;
    //            m_GCWaitTime = GlobeVar.INVALID_ID;
    //        }
    //    }
    //}

    void OnDestroy()
    {
        m_instance = null;
    }

    public static bool LoadItem(UIPathData pathData, OnLoadUIItemDelegate delLoadItem, object param = null)
    {
        if (null == m_instance)
        {
            Debug.LogError("game manager is not init");
            return false;
        }

        m_instance.LoadUIItem(pathData, delLoadItem, param);
        return true;
    }

    // 展示UI，根据类型不同，触发不同行为
    public static bool ShowUI(UIPathData pathData, OnOpenUIDelegate delOpenUI = null, object param = null)
    {
        if (null == m_instance)
        {
            Debug.LogError("game manager is not init");
            return false;
        }

        m_instance.AddLoadDicRefCount(pathData.name);


        //#if !UNITY_EDITOR && !UNITY_STANDALONE_WIN
        //		if(pathData.uiType == UIPathData.UIType.TYPE_POP || 
        //        pathData.uiType == UIPathData.UIType.TYPE_STORY || 
        //        pathData.uiType == UIPathData.UIType.TYPE_TIP ||
        //		pathData.uiType == UIPathData.UIType.TYPE_MENUPOP)
        //		{
        //			if(JoyStickLogic.Instance() != null)
        //			{
        //				ProcessInput.Instance().ReleaseTouch();
        //				JoyStickLogic.Instance().ReleaseJoyStick();
        //			}
        //		}
        //#endif
        Dictionary<string, GameObject> curDic = null;
        switch (pathData.uiType)
        {
            case UIPathData.UIType.TYPE_BASE:
                curDic = m_instance.m_dicBaseUI;
                break;
            case UIPathData.UIType.TYPE_POP:
                curDic = m_instance.m_dicPopUI;
                break;
            case UIPathData.UIType.TYPE_STORY:
                curDic = m_instance.m_dicStoryUI;
                break;
            case UIPathData.UIType.TYPE_TIP:
                curDic = m_instance.m_dicTipUI;
                break;
            case UIPathData.UIType.TYPE_MENUPOP:
                curDic = m_instance.m_dicMenuPopUI;
                break;
            case UIPathData.UIType.TYPE_MESSAGE:
                curDic = m_instance.m_dicMessageUI;
                break;
            case UIPathData.UIType.TYPE_DEATH:
                curDic = m_instance.m_dicDeathUI;

                break;
            default:
                return false;
        }

        if (null == curDic)
        {
            return false;
        }

        if (m_instance.m_dicCacheUI.ContainsKey(pathData.name))
        {
            if (!curDic.ContainsKey(pathData.name))
            {
                curDic.Add(pathData.name, m_instance.m_dicCacheUI[pathData.name]);
            }

            m_instance.m_dicCacheUI.Remove(pathData.name);
        }

        if (curDic.ContainsKey(pathData.name))
        {
            curDic[pathData.name].SetActive(true);
            m_instance.DoAddUI(pathData, curDic[pathData.name], delOpenUI, param);
            return true;
        }

        m_instance.LoadUI(pathData, delOpenUI, param);

        return true;
    }

    // 读表展示UI，
    public static bool ShowUIByID(int tableID, OnOpenUIDelegate delOpenUI = null, object param = null)
    {
        if (null == m_instance)
        {
            Debug.LogError("game manager is not init");
            return false;
        }

        //FixMe 这里可以读表、读本地文件获取到UI资源路径
        /*
        //读表并解析数据，传入数据加载
        //调用ShowUI，这里与ShowUI相比只是多提供了一个根据ID加载UI的方式
        return UIManager.ShowUI(curData, delOpenUI, param);
        */
        return true; //补充好内容后屏蔽掉
    }

    public static void CloseUIByID(int tableID)
    {
        if (null == m_instance)
        {
            Debug.LogError("game manager is not init");
            return;
        }


        //FixMe 这里可以读表、读本地文件获取到UI资源路径
        /*
        读表并解析数据，传入数据
        调用CloseUI，这里与ShowUI相比只是多提供了一个根据ID关闭UI的方式
        UIPathData curPathData = UIPathData.m_DicUIInfo[curTabPath.Path];
        CloseUI(curPathData);
        */



    }

    // 关闭UI，根据类型不同，触发不同行为
    public static void CloseUI(UIPathData pathData)
    {
        if (null == m_instance)
        {
            return;
        }

        Resources.UnloadUnusedAssets();
        m_instance.RemoveLoadDicRefCount(pathData.name);
        switch (pathData.uiType)
        {
            case UIPathData.UIType.TYPE_BASE:
                m_instance.CloseBaseUI(pathData.name);
                break;
            case UIPathData.UIType.TYPE_POP:
                m_instance.ClosePopUI(pathData.name);
                break;
            case UIPathData.UIType.TYPE_STORY:
                m_instance.CloseStoryUI(pathData.name);
                break;
            case UIPathData.UIType.TYPE_TIP:
                m_instance.CloseTipUI(pathData.name);
                break;
            case UIPathData.UIType.TYPE_MENUPOP:
                m_instance.CloseMenuPopUI(pathData.name);
                break;
            case UIPathData.UIType.TYPE_MESSAGE:
                m_instance.CloseMessageUI(pathData.name);
                break;
            case UIPathData.UIType.TYPE_DEATH:
                m_instance.CloseDeathUI(pathData.name);
                break;
            default:
                break;
        }

        if (pathData.uiGroupName != null && pathData.isMainAsset)
        {
            //FixMe
            //这里可以根据需求，做关闭UI的后续工作
        }
    }


    void DoAddUI(UIPathData uiData, GameObject curWindow, object fun, object param)
    {
  
        if (null != curWindow)
        {
            Transform parentRoot = null;
            Dictionary<string, GameObject> relativeDic = null;
            switch (uiData.uiType)
            {
                case UIPathData.UIType.TYPE_BASE:
                    parentRoot = BaseUIRoot;
                    relativeDic = m_dicBaseUI;
                    break;
                case UIPathData.UIType.TYPE_POP:
                    parentRoot = PopUIRoot;
                    relativeDic = m_dicPopUI;
                    break;
                case UIPathData.UIType.TYPE_STORY:
                    parentRoot = StoryUIRoot;
                    relativeDic = m_dicStoryUI;
                    break;
                case UIPathData.UIType.TYPE_TIP:
                    parentRoot = TipUIRoot;
                    relativeDic = m_dicTipUI;
                    break;
                case UIPathData.UIType.TYPE_MENUPOP:
                    parentRoot = MenuPopUIRoot;
                    relativeDic = m_dicMenuPopUI;
                    break;
                case UIPathData.UIType.TYPE_MESSAGE:
                    parentRoot = MessageUIRoot;
                    relativeDic = m_dicMessageUI;
                    break;
                case UIPathData.UIType.TYPE_DEATH:
                    parentRoot = DeathUIRoot;
                    relativeDic = m_dicDeathUI;
                    break;
                default:
                    break;

            }

            if (uiData.uiType == UIPathData.UIType.TYPE_POP || uiData.uiType == UIPathData.UIType.TYPE_MENUPOP)
            {
                OnLoadNewPopUI(m_dicPopUI, uiData.name);
                OnLoadNewPopUI(m_dicMenuPopUI, uiData.name);
            }
            if (null != relativeDic && relativeDic.ContainsKey(uiData.name))
            {
                relativeDic[uiData.name].SetActive(true);
            }

            else if (null != parentRoot && null != relativeDic)
            {
                GameObject newWindow = GameObject.Instantiate(curWindow) as GameObject;

                if (null != newWindow)
                {
                    Vector3 oldScale = newWindow.transform.localScale;
                    newWindow.transform.parent = parentRoot;
                    newWindow.transform.localPosition = Vector3.zero;
                    newWindow.transform.localScale = oldScale;
                    relativeDic.Add(uiData.name, newWindow);
                    if (uiData.uiType == UIPathData.UIType.TYPE_MENUPOP)
                    {
                        LoadMenuSubUIShield(newWindow);
                    }
                }
            }

            if (uiData.uiType == UIPathData.UIType.TYPE_STORY)
            {
                BaseUIRoot.gameObject.SetActive(false);
                TipUIRoot.gameObject.SetActive(false);
                PopUIRoot.gameObject.SetActive(false);
                MenuPopUIRoot.gameObject.SetActive(false);
                MessageUIRoot.gameObject.SetActive(false);
                StoryUIRoot.gameObject.SetActive(true);
            }
            else if (uiData.uiType == UIPathData.UIType.TYPE_MENUPOP)
            {
               
            }
            else if (uiData.uiType == UIPathData.UIType.TYPE_DEATH)
            {
                ReliveCloseOtherSubUI();
            }
            else if (uiData.uiType == UIPathData.UIType.TYPE_POP)
            {
                //FixMe
                //因为Pop是互斥面板，这里根据设计 关闭其他面板

                
            }
        }

        if (null != fun)
        {
            OnOpenUIDelegate delOpenUI = fun as OnOpenUIDelegate;
            delOpenUI(curWindow != null, param);
        }
    }

    public void HideAllUILayer()
    {
        BaseUIRoot.gameObject.SetActive(false);
        TipUIRoot.gameObject.SetActive(false);
        PopUIRoot.gameObject.SetActive(false);
        MenuPopUIRoot.gameObject.SetActive(false);
        MessageUIRoot.gameObject.SetActive(false);
        StoryUIRoot.gameObject.SetActive(false);
    }

    public void ShowAllUILayer()
    {
        BaseUIRoot.gameObject.SetActive(true);
        TipUIRoot.gameObject.SetActive(true);
        PopUIRoot.gameObject.SetActive(true);
        MenuPopUIRoot.gameObject.SetActive(true);
        MessageUIRoot.gameObject.SetActive(true);
        StoryUIRoot.gameObject.SetActive(true);
    }
    IEnumerator GCAfterOneSceond()
    {
        yield return new WaitForSeconds(1);

        // Resources.UnloadUnusedAssets();
        //System.GC.Collect();
    }

    void DoLoadUIItem(UIPathData uiData, GameObject curItem, object fun, object param)
    {
        if (null != fun)
        {
            OnLoadUIItemDelegate delLoadItem = fun as OnLoadUIItemDelegate;
            delLoadItem(curItem, param);
        }
    }
    void ClosePopUI(string name)
    {
        OnClosePopUI(m_dicPopUI, name);
    }

    void CloseStoryUI(string name)
    {
        if (TryDestroyUI(m_dicStoryUI, name))
        {
            BaseUIRoot.gameObject.SetActive(true);
            TipUIRoot.gameObject.SetActive(true);
            PopUIRoot.gameObject.SetActive(true);
            MenuPopUIRoot.gameObject.SetActive(true);
            MessageUIRoot.gameObject.SetActive(true);
            StoryUIRoot.gameObject.SetActive(true);
        }
    }

    void CloseBaseUI(string name)
    {
        if (m_dicBaseUI.ContainsKey(name))
        {
            m_dicBaseUI[name].SetActive(false);
        }
    }

    void CloseTipUI(string name)
    {
        TryDestroyUI(m_dicTipUI, name);
    }

    void CloseMenuPopUI(string name)
    {
        OnClosePopUI(m_dicMenuPopUI, name);
    }

    void CloseMessageUI(string name)
    {
        TryDestroyUI(m_dicMessageUI, name);
    }

    void CloseDeathUI(string name)
    {
        if (TryDestroyUI(m_dicDeathUI, name))
        {
            // 关闭复活界面时 恢复节点的显示
            m_instance.PopUIRoot.gameObject.SetActive(true);
            m_instance.MenuPopUIRoot.gameObject.SetActive(true);
            m_instance.TipUIRoot.gameObject.SetActive(true);
            m_instance.BaseUIRoot.gameObject.SetActive(true);
        }
    }

    void LoadUI(UIPathData uiData, OnOpenUIDelegate delOpenUI = null, object param1 = null)
    {
        //加载面板
        //DoAddUI
        //其他操作
    }

    void LoadUIItem(UIPathData uiData, OnLoadUIItemDelegate delLoadItem, object param = null)
    {
        //加载面板
        //DoAddUI
        //其他操作
    }

    static void LoadMenuSubUIShield(GameObject newWindow)
    {
        //加载子面板
        //认父级
        //参数初始化
        //其他操作
    }

    static void LoadPopUIShield(GameObject newWindow)
    {
        //加载子面板
        //认父级
        //参数初始化
        //其他操作
    }

    GameObject AddObjToRoot(string name)
    {
        GameObject obj = new GameObject();
        obj.transform.parent = transform;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        obj.name = name;
        return obj;
    }

    bool SubUIShow()
    {
        
        return false;
       
    }

    public static bool IsSubUIShow()
    {
        if (m_instance != null)
        {
            return m_instance.SubUIShow();
        }
        return false;
    }

    static void ReliveCloseOtherSubUI()
    {
        // 关闭所有PopUI
        List<string> uiKeyList = new List<string>();
        foreach (KeyValuePair<string, GameObject> pair in m_instance.m_dicPopUI)
        {
            uiKeyList.Add(pair.Key);
        }
        for (int i = 0; i < uiKeyList.Count; i++)
        {
            m_instance.ClosePopUI(uiKeyList[i]);
        }
        uiKeyList.Clear();
        // 关闭所有MenuPopUI
        foreach (KeyValuePair<string, GameObject> pair in m_instance.m_dicMenuPopUI)
        {
            uiKeyList.Add(pair.Key);
        }
        for (int i = 0; i < uiKeyList.Count; i++)
        {
            m_instance.CloseMenuPopUI(uiKeyList[i]);
        }
        uiKeyList.Clear();
        // 关闭所有TipUI
        foreach (KeyValuePair<string, GameObject> pair in m_instance.m_dicTipUI)
        {
            uiKeyList.Add(pair.Key);
        }
        for (int i = 0; i < uiKeyList.Count; i++)
        {
            m_instance.CloseTipUI(uiKeyList[i]);
        }
        uiKeyList.Clear();
        // 关闭所有除CentreNotice以外的MessageUI MessageUIRoot节点保留不隐藏
        foreach (KeyValuePair<string, GameObject> pair in m_instance.m_dicMessageUI)
        {
            if (!pair.Key.Contains("CentreNotice"))
            {
                uiKeyList.Add(pair.Key);
            }
        }
        for (int i = 0; i < uiKeyList.Count; i++)
        {
            m_instance.CloseMessageUI(uiKeyList[i]);
        }
        uiKeyList.Clear();

        //对话中断情况
        //关闭对应的对话面板

        // 隐藏二级UI节点
        m_instance.PopUIRoot.gameObject.SetActive(false);
        m_instance.MenuPopUIRoot.gameObject.SetActive(false);
        m_instance.TipUIRoot.gameObject.SetActive(false);
        m_instance.BaseUIRoot.gameObject.SetActive(false);
    }

    static public void NewPlayerGuideCloseSubUI()
    {
        // 关闭所有PopUI
        foreach (KeyValuePair<string, GameObject> pair in m_instance.m_dicPopUI)
        {
            m_instance.ClosePopUI(pair.Key);
            break;
        }
        // 关闭所有MenuPopUI
        foreach (KeyValuePair<string, GameObject> pair in m_instance.m_dicMenuPopUI)
        {
            m_instance.CloseMenuPopUI(pair.Key);
            break;
        }
        // 关闭所有TipUI
        foreach (KeyValuePair<string, GameObject> pair in m_instance.m_dicTipUI)
        {
            m_instance.CloseTipUI(pair.Key);
            break;
        }       
    }

    void AddLoadDicRefCount(string pathName)
    {
        if (m_dicWaitLoad.ContainsKey(pathName))
        {
            m_dicWaitLoad[pathName]++;
        }
        else
        {
            m_dicWaitLoad.Add(pathName, 1);
        }
    }

    bool RemoveLoadDicRefCount(string pathName)
    {
        if (!m_dicWaitLoad.ContainsKey(pathName))
        {
            return false;
        }

        m_dicWaitLoad[pathName]--;
        if (m_dicWaitLoad[pathName] <= 0)
        {
            m_dicWaitLoad.Remove(pathName);
        }

        return true;
    }

    void DestroyUI(string name, GameObject obj)
    {
        Destroy(obj);
        //可以写其他操作 如资源清理
    }

    private void OnLoadNewPopUI(Dictionary<string, GameObject> curList, string curName)
    {
        if (curList == null)
        {
            return;
        }

        List<string> objToRemove = new List<string>();

        if (curList.Count > 0)
        {
            objToRemove.Clear();
            foreach (KeyValuePair<string, GameObject> objs in curList)
            {
                if (curName == objs.Key)
                {
                    continue;
                }
                objs.Value.SetActive(false);
                objToRemove.Add(objs.Key);
                if (UIPathData.m_DicUIName.ContainsKey(objs.Key) && UIPathData.m_DicUIName[objs.Key].isDestroyOnUnload)
                {
                    DestroyUI(objs.Key, objs.Value);
                }
                else
                {
                    m_dicCacheUI.Add(objs.Key, objs.Value);
                }
            }

            for (int i = 0; i < objToRemove.Count; i++)
            {
                if (curList.ContainsKey(objToRemove[i]))
                {
                    curList.Remove(objToRemove[i]);
                }
            }
        }
    }
    private void OnClosePopUI(Dictionary<string, GameObject> curList, string curName)
    {
        if (TryDestroyUI(curList, curName))
        {
            // 关闭导航栏打开的二级UI时 收回导航栏
        }
    }

    private bool TryDestroyUI(Dictionary<string, GameObject> curList, string curName)
    {
        if (curList == null)
        {
            return false;
        }

        if (!curList.ContainsKey(curName))
        {
            return false;
        }

        //#if UNITY_ANDROID

        // < 768M UI不进行缓存
        if (SystemInfo.systemMemorySize < 768)
        {
            DestroyUI(curName, curList[curName]);
            curList.Remove(curName);

            Resources.UnloadUnusedAssets();
            GC.Collect();
            return true;
        }

        //#endif

        if (UIPathData.m_DicUIName.ContainsKey(curName) && !UIPathData.m_DicUIName[curName].isDestroyOnUnload)
        {
            curList[curName].SetActive(false);
            m_dicCacheUI.Add(curName, curList[curName]);
        }
        else
        {
            DestroyUI(curName, curList[curName]);
        }

        curList.Remove(curName);

        return true;
    }


#if UNITY_ANDROID 
    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            PlatformHelper.ClickEsc();
        }
    }

    
#endif
}

