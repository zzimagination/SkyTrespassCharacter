using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFfff : MonoBehaviour
{
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("In");
            obj.SetActive(false);
            Debug.Log("ex");
        }   
    }
}
