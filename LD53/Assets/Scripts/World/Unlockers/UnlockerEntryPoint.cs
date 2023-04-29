using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class UnlockerEntryPoint : MonoBehaviour
    {
        public Unlocker unlocker;
        public Collider coll;
        public GameObject highlight;
        public Transform playerPosition;

        public void Activate()
        {
            coll.enabled = true;
            highlight.SetActive(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerBody playerBody = other.GetComponent<PlayerBody>();
            if(playerBody)
            {
                unlocker.StartGame();
            }
        }
    }
}
