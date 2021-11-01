using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECS_Main : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform content;
    void Start()
    {
        for (int i = 1; i <= 4; i++)
        {
            UIItem item = new UIItem("Item", content);
            item.SetItem(Item.CreateEntityByID(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
