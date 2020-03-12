using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLiner : MonoBehaviour
{
    public BulletLinerPool linerPool;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnGUI()
    {
        if(GUILayout.Button("Shoot"))
        {
            linerPool.CreatLiner(new Vector3(0, 0, 0), new Vector3(0, 0, 10),transform.rotation);
        }
    }
}
