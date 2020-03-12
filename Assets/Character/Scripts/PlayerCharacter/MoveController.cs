using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SkyTrespass
{
    public class MoveController : MonoBehaviour
    {

        public float physics_MoveSpeed;

        public bool isIdle;
        public bool isFall;

        Vector2 inputDelt;
        Rigidbody _rigidbody;
        CapsuleCollider _collider;
        Vector3 lastPosition;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<CapsuleCollider>();
        }
        // Start is called before the first frame update
        void Start()
        {

        }
        // Update is called once per frame
        void Update()
        {
            if(_rigidbody.velocity.y<-2)
            {
                isFall = true;
            }else
            {
                isFall = false;
            }
            if(transform.position.Equals(lastPosition)==true)
            {
                isIdle = true;
            }else
            {
                isIdle = false;
            }

            lastPosition = transform.position;

        }

        public void MoveDelt(float x, float y)
        {
            if (x == 0 && y == 0)
                  return;
            isIdle = false;
            float f = physics_MoveSpeed * Time.fixedDeltaTime;
            Vector3 dir = new Vector3(x, 0, y);

            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(_rigidbody.position, dir, out hitInfo, _collider.radius / 2))
            {
                float angle = Vector3.Angle(hitInfo.normal, -dir);
                Vector3 axis = Vector3.Cross(hitInfo.normal, dir).normalized;
                Vector3 newDir = Quaternion.AngleAxis(-angle, axis) * dir;
                _rigidbody.MovePosition(newDir.normalized * f + _rigidbody.position);
            }
            else
            {
                Vector3 nextPos = _rigidbody.position + dir * f + new Vector3(0, 0.01f, 0);
                if (Physics.Raycast(nextPos, Vector3.down, out hitInfo, _collider.height / 2))
                {
                    if (hitInfo.distance > 0.01f)
                    {
                        float angle = 90 - Vector3.Angle(hitInfo.normal, dir);
                        Vector3 axis = Vector3.Cross(hitInfo.normal, dir).normalized;
                        Vector3 newDir = Quaternion.AngleAxis(angle, axis) * dir;

                        _rigidbody.MovePosition(newDir.normalized * f + _rigidbody.position);
                    }
                    else
                    {
                        nextPos.y -= 0.01f;
                        _rigidbody.MovePosition(nextPos);
                    }
                }
                else
                {
                    nextPos.y -= 0.01f;
                    _rigidbody.MovePosition(nextPos);
                }
            }
        }

        public void RotateDelt(float x, float y)
        {
            if (x == 0 && y == 0)
                return;
            Vector3 rotateDir = new Vector3(x, 0, y);
            Vector3 nowDir = transform.forward;
            Quaternion qdis = Quaternion.LookRotation(rotateDir);
            var q = Quaternion.Lerp(transform.rotation, qdis, 0.2f);
            _rigidbody.MoveRotation(q);
        }



        public void OnMove(InputValue value)
        {
            inputDelt = value.Get<Vector2>();
        }
    }
}