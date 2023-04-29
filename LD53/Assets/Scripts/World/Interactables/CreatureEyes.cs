using ErksUnityLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class CreatureEyes : MonoBehaviour
    {
        public Transform eyeLeft;
        public Transform eyeRight;

        private float initialScaleY;

        private void Awake()
        {
            initialScaleY = eyeLeft.localScale.y;
        }

        public void SetEyesScale(float value)
        {
            eyeLeft.SetScaleY(initialScaleY * value);
            eyeRight.SetScaleY(initialScaleY * value);
        }
    }
}
