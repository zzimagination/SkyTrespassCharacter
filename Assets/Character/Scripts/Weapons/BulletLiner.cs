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

            Vector3 mid = Vector3.Lerp(start, end, 0.25f);
            lineRenderer.SetPosition(1, mid);
             mid = Vector3.Lerp(start, end, 0.5f);
            lineRenderer.SetPosition(2, mid);
             mid = Vector3.Lerp(start, end, 0.75f);
            lineRenderer.SetPosition(3, mid);

            lineRenderer.SetPosition(4, end);
        }


        
    }
}