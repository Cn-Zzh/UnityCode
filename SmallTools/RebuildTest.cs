using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class RebuildTest : MonoBehaviour
{
    IList<ICanvasElement> m_LayoutRebuildQueue;
    IList<ICanvasElement> m_GraphicRebuildQueue;
    // Start is called before the first frame update
    private void Awake()
    {
        System.Type type = typeof(CanvasUpdateRegistry);
        FieldInfo field = type.GetField("m_LayoutRebuildQueue", BindingFlags.NonPublic | BindingFlags.Instance);
        m_LayoutRebuildQueue = (IList<ICanvasElement>)field.GetValue(CanvasUpdateRegistry.instance);
        field = type.GetField("m_GraphicRebuildQueue", BindingFlags.NonPublic | BindingFlags.Instance);
        m_GraphicRebuildQueue = (IList<ICanvasElement>)field.GetValue(CanvasUpdateRegistry.instance);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int j = 0;j < m_LayoutRebuildQueue.Count; j++)
        {
            var rebuild = m_LayoutRebuildQueue[j];
            if (ObjectValidForUpdate(rebuild))
            {
                Debug.LogFormat("{0}引起网格重建", rebuild.transform.name);
            }
        }

        for (int j = 0; j < m_GraphicRebuildQueue.Count; j++)
        {
            var element = m_GraphicRebuildQueue[j];
            if (ObjectValidForUpdate(element))
            {
                Debug.LogFormat("{0}引起{1}网格重建", element.transform.name,element.transform.GetComponent<Graphic>().canvas.name);
            }
        }
    }
    private bool ObjectValidForUpdate(ICanvasElement element)
    {
        var valid = element != null;

        var isUnityObject = element is Object;

        if (isUnityObject)
            valid = (element as Object) != null;

        return valid;
    }
}
