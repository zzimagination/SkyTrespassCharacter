using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRay : MonoBehaviour
{
    RaycastHit[] raycastHits = new RaycastHit[1];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics.RaycastNonAlloc(transform.position,Vector3.down, raycastHits);
        int t = 0;
        foreach (var item in raycastHits)
        {
            Debug.Log(item.collider.name + t);
            t++;
        }
    }
}
