using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class UpDownAnimator : MonoBehaviour
    {
        public Animator animator;
        public float initialSpeed = 1f;
        public bool randomizeSpeedOnAwake = true;
        public bool moveOnAwake = true;

        private const string MovingBool = "moving";

        private void Awake()
        {
            SetSpeed(initialSpeed);

            if(randomizeSpeedOnAwake)
            {
                RandomizeSpeed();
            }

            if(moveOnAwake)
            {
                SetMoving(true);
            }
        }

        public void SetMoving(bool moving)
        {
            animator.SetBool(MovingBool, moving);
        }

        public void RandomizeSpeed()
        {
            animator.speed = Random.Range(0.8f, 1.2f);
        }

        public void SetSpeed(float value)
        {
            animator.speed = value;
        }

        private void Reset()
        {
            animator = GetComponent<Animator>();
        }
    }
}
