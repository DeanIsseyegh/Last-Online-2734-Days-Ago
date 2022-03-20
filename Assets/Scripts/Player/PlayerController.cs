using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public float speed;
        public float fallBackSpeed = 10f;

        private Animator animator;

        private bool isFallingBack;
        private Vector2 fallBackPoint;
        private Rigidbody2D _rb;

        private void Start()
        {
            animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
        }


        private void Update()
        {
            if (isFallingBack)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, fallBackPoint, fallBackSpeed * Time.deltaTime);
                float distanceToFallBackPoint = Vector2.Distance(gameObject.transform.position, fallBackPoint);
                Debug.Log("Distance to fall back point is : " + distanceToFallBackPoint);
                if (Vector2.Distance(gameObject.transform.position, fallBackPoint) <= 0.1f)
                {
                    isFallingBack = false;
                }
                return;
            }


            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
            }

            if (Input.GetKey(KeyCode.W))
            {
                dir.y = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
            }



            dir.Normalize();
            animator.SetFloat("Horizontal", dir.x);
            animator.SetFloat("Vertical", dir.y);
            animator.SetBool("IsMoving", dir.magnitude > 0);

            _rb.velocity = speed * dir;
        }

        public void MoveBackTo(Vector3 moveTo)
        {
            fallBackPoint = moveTo;
            isFallingBack = true;
        }
    }
}