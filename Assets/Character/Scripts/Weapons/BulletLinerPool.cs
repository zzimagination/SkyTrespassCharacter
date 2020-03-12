using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLinerPool : MonoBehaviour
{
    public GameObject bulletliner;

    Queue<BulletLiner> active;
    Queue<BulletLiner> inactive;
    private void Awake()
    {
        active = new Queue<BulletLiner>();
        inactive = new Queue<BulletLiner>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public BulletLiner CreatLiner(Vector3 start,Vector3 end, Quaternion rotation)
    {
       

        if (inactive.Count > 0)
        {
            var t = inactive.Dequeue();
            t.SetLine(start, end,rotation);
            active.Enqueue(t);
            return t;
        }else
        {
            GameObject obj = Instantiate(bulletliner,transform);
            var t= obj.GetComponent<BulletLiner>();
            t.pool = this;
            t.SetLine(start, end,rotation);
            active.Enqueue(t);
            return t;
        }
        
    }

    public void InActiveLiner(BulletLiner bulletLiner)
    {
        inactive.Enqueue(bulletLiner);
    }

    
}
