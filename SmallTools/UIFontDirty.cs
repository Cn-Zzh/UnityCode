using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFontDirty : MonoBehaviour
{
    bool isDirty = false;
    Font dirtyFont = null;
    // Start is called before the first frame update
    private void Awake()
    {
        Font.textureRebuilt += delegate (Font font1)
        {
            isDirty = true;
            dirtyFont = font1;
        };
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        if (isDirty)
        {
            isDirty = false;
            foreach (Text text in GameObject.FindObjectsOfType<Text>())
            {
                if(text.font == dirtyFont)
                {
                    text.FontTextureChanged();
                }
            }
            print("textureRebuilt" + dirtyFont.name);
            dirtyFont = null;
        }
    }
}
