using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass
{
    public class TrainingRangeManager : MonoBehaviour
    {
        public GameObject robot;
        public Bounds bounds;

        GameObject currentRobot;
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(currentRobot==null)
            {
                currentRobot= Instantiate(robot, transform);

                float x = Random.Range(bounds.min.x, bounds.max.x);
                float z = Random.Range(bounds.min.z, bounds.max.z);

                currentRobot.transform.localPosition = new Vector3(x, 0, z);
            }
        }
    }
}