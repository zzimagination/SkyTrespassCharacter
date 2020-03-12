using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SelectionBase]
[DisallowMultipleComponent]
public class TestAttribute : MonoBehaviour
{
    [Multiline]

    public string NewField="default";
    [Space(100)]
    [ContextMenuItem("ResetValue", "ResetValue")]
    public int a;

    [TextArea]
    public string H;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [ContextMenu("nihao")]
    void DoSomething()
    {
        Debug.Log("1");
    }


    void ResetValue()
    {
        NewField = "";
    }
    [RuntimeInitializeOnLoadMethod]
    static void Hello()
    {
        Debug.Log("hello");
    }
}
