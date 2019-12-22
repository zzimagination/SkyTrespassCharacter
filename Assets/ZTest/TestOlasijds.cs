using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOlasijds : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("awake");
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
    }

    private void OnEnable()
    {
        Debug.Log("Enable");

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        Debug.Log("disable");
    }
}
