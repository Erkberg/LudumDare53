using ErksUnityLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class UnlockableInteractable : Interactable
    {
        public bool unlocked;

        private float unlockedPosY = 0f;
        private float unlockSpeed = 1f;

        public void Unlock()
        {
            StartCoroutine(UnlockSequence());
        }

        public IEnumerator UnlockSequence()
        {
            while (transform.position.y < unlockedPosY)
            {
                transform.position += Vector3.up * unlockSpeed * Time.deltaTime;
                yield return null;
            }

            transform.SetPositionY(unlockedPosY);
            SetColliderEnabled(true);
        }
    }
}
