using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class SkillEditorWindow : EditorWindow
{
    class PlayerEditor
    {
        public int _characterIndex = 0;
        public int _folderIndex = 0;
        public string characterName = string.Empty;
        public string folderName = string.Empty;
        public string characterFilter = string.Empty;
        public List<string> characteList = new List<string>();
        public Player player = null;
    }

    PlayerEditor m_player = new PlayerEditor();

    /// <summary>
    /// 文件名
    /// </summary>
    List<string> m_folderList = new List<string>();
    /// <summary>
    /// 所有预制体名
    /// </summary>
    List<string> m__characterList = new List<string>();

    /// <summary>
    /// 按文件名储存 预制体
    /// </summary>
    Dictionary<string, List<string>> m_folderPrefabs = new Dictionary<string, List<string>>();


    /// <summary>
    /// 技能详情窗口
    /// </summary>
    SkillWindow skillWindow;

    /// <summary>
    ///  储存 创建新的技能 的名字
    /// </summary>
    string newSkillName = string.Empty;


    /// <summary>
    /// 窗口初始化  （打开窗口）
    /// </summary>
    [MenuItem("Tools/技能编译器")]
    public static void Init()
    {

        //只有运行的时候 才会打开
        if (Application.isPlaying)
        {
            SkillEditorWindow window = EditorWindow.GetWindow<SkillEditorWindow>("SkillEditor");
            if (window != null)
            {
                window.Show();
            }
        }

    }
    private void OnEnable()
    {
        DoSearchFolder();
        DoSearchCharacter();
    }

    /// <summary>
    /// 索引 所有 文件夹
    /// </summary>
    void DoSearchFolder()
    {
        m_folderList.Clear();
        m_folderList.Add("all");
        string[] folders = Directory.GetDirectories(GetCharacterPath());
        foreach (var item in folders)
        {
            m_folderList.Add(Path.GetFileName(item));
        }
    }

    /// <summary>
    /// 索引所有的 预制
    /// </summary>
    void DoSearchCharacter()
    {
        string[] files = Directory.GetFiles(GetCharacterPath(), "*.prefab", SearchOption.AllDirectories);
        m__characterList.Clear();
        foreach (var item in files)
        {
            m__characterList.Add(Path.GetFileNameWithoutExtension(item));
        }
        m__characterList.Sort();
        m__characterList.Insert(0, "null");
        m_player.characteList.AddRange(m__characterList);

    }
    string GetCharacterPath()
    {
        return Application.dataPath + "/GameDate/Model";
    }
    private void OnGUI()
    {
        int folderIndex = EditorGUILayout.Popup(m_player._folderIndex, m_folderList.ToArray());
        if (folderIndex != m_player._folderIndex)
        {
            m_player._folderIndex = folderIndex;
            m_player._characterIndex = -1;
            string folderName = m_folderList[m_player._folderIndex];
            List<string> list;
            if (folderName.Equals("all"))
            {
                list = m__characterList;
            }
            else
            {
                if (!m_folderPrefabs.TryGetValue(folderName, out list))
                {
                    list = new List<string>();
                    string[] files = Directory.GetFiles(GetCharacterPath() + "/" + folderName, "*.prefab", SearchOption.AllDirectories);
                    foreach (var item in files)
                    {
                        list.Add(Path.GetFileNameWithoutExtension(item));
                    }
                    m_folderPrefabs[folderName] = list;
                }
                //SetPlayerCharacters(m_player, list);
            }
            m_player.characteList.Clear();
            m_player.characteList.AddRange(list);
        }
        int characterIndex = EditorGUILayout.Popup(m_player._characterIndex, m_player.characteList.ToArray());
        if (characterIndex != m_player._characterIndex)
        {
            m_player._characterIndex = characterIndex;
            if (m_player.characterName != m_player.characteList[m_player._characterIndex])
            {
                m_player.characterName = m_player.characteList[m_player._characterIndex];
                if (!string.IsNullOrEmpty(m_player.characterName))
                {
                    if (m_player.player != null)
                    {
                        m_player.player.Destroy();
                    }
                    m_player.player = Player.Init(m_player.characterName);
                }
            }

        }
        /*float speed = EditorGUILayout.Slider((m_player.player == null ? 0f : m_player.player.AnimSpeed), 0.1f, 5);
        if (m_player.player != null && m_player.player.AnimSpeed != speed)
        {
            m_player.player.AnimSpeed = speed;
        }*/
        newSkillName = GUILayout.TextField(newSkillName);
        if (GUILayout.Button("创建新的技能"))
        {
            if (!string.IsNullOrEmpty(newSkillName) && m_player.player != null)
            {
                List<SkillBase> skills = m_player.player.AddNewSkill(newSkillName);
                OpenSkillWindow(newSkillName, skills);

                newSkillName = "";
            }
        }

        if (m_player.player != null)
        {

            ScrollViewPos = GUILayout.BeginScrollView(ScrollViewPos, false, true);
            foreach (var item in m_player.player.skillsList)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(item.Key))
                {
                    List<SkillBase> skillComponents = m_player.player.GetSkill(item.Key);
                    foreach (var ite in skillComponents)
                    {
                        ite.Init();
                    }
                    OpenSkillWindow(item.Key, skillComponents);
                }

                GUILayoutOption[] option = new GUILayoutOption[] {
                GUILayout.Width(60),
                GUILayout.Height(19)
                };

                if (GUILayout.Button("删除技能", option))
                {
                    m_player.player.RevSkill(item.Key);
                    break;
                }
                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();
        }


    }
    Vector2 ScrollViewPos = new Vector2(0, 0);
    void OpenSkillWindow(string newSkillName, List<SkillBase> skillComponents)
    {
        if (skillComponents != null)
        {
            if (skillWindow == null)
            {
                skillWindow = EditorWindow.GetWindow<SkillWindow>("");
            }
            skillWindow.titleContent = new GUIContent(newSkillName);

            skillWindow.SetInitSkill(skillComponents, m_player.player);
            skillWindow.Show();
            skillWindow.Repaint();
        }

    }


    /* void SetPlayerCharacters(PlayerEditor player, List<string> list)
     {
         player.characteList.Clear();

     }*/
}
