using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrespass.Character
{
    public class BulletLiner : MonoBehaviour
    {
        public LineRenderer lineRenderer;
        public float timeDispear;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(DestoryMyself());
        }


        IEnumerator DestoryMyself()
        {
            yield return new WaitForSeconds(timeDispear);
            Destroy(gameObject);
        }


        public void SetPoint(Vector3 start,Vector3 end)
        {
            lineRenderer.SetPosition(0, start);

            Vector3 mid1 = Vector3.Lerp(start, end, 4f / 20f);
            lineRenderer.SetPosition(1, mid1);
            Vector3 mid2 = Vector3.Lerp(start, end, 19f / 20f);
            lineRenderer.SetPosition(2, mid2);

            lineRenderer.SetPosition(3, end);
        }


        
    }
}