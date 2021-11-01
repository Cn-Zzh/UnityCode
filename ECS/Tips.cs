using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tips
{
    static Tips tip;
    static Transform tips;
    static Transform content;
    static Button Close;
    public static Tips Get()
    {
        if(tip == null)
        {
            tip = new Tips();
            tips = GameObject.Instantiate(Resources.Load<Transform>("ECS/Hint"), GameObject.Find("Canvas").transform, false);
            content = tips.Find("Scroll View/Viewport/Content");
            Close = tips.Find("Close").GetComponent<Button>();
            Close.onClick.AddListener(() =>
            {
                foreach (Transform item in content)
                {
                    GameObject.Destroy(item.gameObject);
                }
                tips.gameObject.SetActive(false);
            });
        }
        return tip;
    }
    public void Show(Item item)
    {
        tips.gameObject.SetActive(true);
        foreach (var value in item.dic)
        {
            GameObject button = GameObject.Instantiate(Resources.Load<GameObject>("ECS/btnItem"), content, false);
            button.transform.GetComponentInChildren<Text>().text = value.Value.GetButName();
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                value.Value.Do();
                if(value.Value.GetButName() == "使用" || value.Value.GetButName() == "出售")
                {
                    item.data.cif.num -= 1;
                }
            });
        }
    }
}
