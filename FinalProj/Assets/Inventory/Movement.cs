//Written by Sam Arutyunyan for Design Patterns Project Spring 2015
using UnityEngine;
using System.Collections;
namespace inventory
{
    public class Movement : MonoBehaviour
    {
        public float moveSpeed = 5;
        public float rotateSpeed = 100;

        private Transform myTransform;
        private CharacterController controller;

        bool cam1 = false;

        Vector3 moveVector = Vector3.zero;

        public void Awake()
        {
            myTransform = transform;
            controller = GetComponent<CharacterController>();
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))//toggle cam
            {
                cam1 = !cam1;
            }

            moveVector = Vector3.zero;

            if (cam1)
            {
                if (!controller.isGrounded)
                {
                    controller.Move(Vector3.down * Time.deltaTime);
                }

                run();
                controller.Move(moveVector * Time.deltaTime * moveSpeed);
                myTransform.LookAt(myTransform.position + moveVector);
            }
            else
            {
                GetComponent<Animation>().CrossFade("idle");
                if (Input.GetKey(KeyCode.D))
                {
                    transform.Rotate(0, Time.deltaTime * rotateSpeed, 0);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    transform.Rotate(0, Time.deltaTime * -rotateSpeed, 0);
                }

                if (Input.GetKey(KeyCode.W))
                {
                    GetComponent<Animation>().CrossFade("run");
                    controller.Move(transform.forward * moveSpeed * Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    GetComponent<Animation>().CrossFade("run");
                    controller.Move(transform.forward * -moveSpeed * Time.deltaTime);
                }

            }

        }


        private void run()
        {
            int x = 0, z = 0;
            GetComponent<Animation>().CrossFade("idle");

            if (Input.GetKey(KeyCode.W))
            {
                GetComponent<Animation>().CrossFade("run");
                z = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                GetComponent<Animation>().CrossFade("run");
                z = -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                GetComponent<Animation>().CrossFade("run");
                x = 1;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                GetComponent<Animation>().CrossFade("run");
                x = -1;
            }

            moveVector = new Vector3(x, 0, z);
        }

    }
}