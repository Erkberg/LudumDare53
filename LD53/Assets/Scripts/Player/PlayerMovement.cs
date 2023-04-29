using ErksUnityLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LD53
{
    public class PlayerMovement : MonoBehaviour
    {
        public PlayerController pc;
        public CharacterController cc;
        public Transform cam;
        public bool movementEnabled = true;

        public float moveSpeed = 4f;
        public float lookSpeed = 33f;
        public float lookSensitivity = 1f;
        public float maxVertAngle = 80f;
        public float posY = 1f;

        private GameInput input;
        private float vertRot = 0f;
        private float horRot = 0f;

        private void Awake()
        {
            input = Game.inst.input;
        }

        private void Update()
        {
            if (pc.IsControllable())
            {
                if(movementEnabled)
                {
                    Move();
                }
                
                Look();
            }
        }

        private void Move()
        {
            Vector3 movement = cam.forward * input.GetMove().y + cam.right * input.GetMove().x;
            movement *= moveSpeed;
            
            if (cc)
            {
                movement.y = 0f;
                cc.Move(movement * Time.deltaTime);
                transform.SetPositionY(posY);
            }
        }

        private void Look()
        {
            horRot += input.GetLook().x * GetSensitivity();
            vertRot -= input.GetLook().y * GetSensitivity();
            vertRot = Mathf.Clamp(vertRot, -maxVertAngle, maxVertAngle);

            //cam.localRotation = Quaternion.Slerp(cam.localRotation, Quaternion.Euler(vertRot, horRot, 0f), lookSpeed * Time.deltaTime);
            cam.localRotation = Quaternion.Euler(vertRot, horRot, 0f);
        }

        public void LookAtTransform(Transform t)
        {
            cam.localRotation = Quaternion.LookRotation(t.position - cam.transform.position);
        }

        private float GetSensitivity()
        {
            return lookSensitivity;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}