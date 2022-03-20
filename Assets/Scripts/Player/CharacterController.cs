using UnityEngine;

namespace Player
{
    public class CharacterController : MonoBehaviour
    {
        public float speed;

        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }


        private void Update()
        {
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

            GetComponent<Rigidbody2D>().velocity = speed * dir;
        }
    }
}