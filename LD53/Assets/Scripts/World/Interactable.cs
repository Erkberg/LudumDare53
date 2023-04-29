using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class Interactable : MonoBehaviour
    {
        public Collider coll;
        public Transform targetPosition;

        public void SetColliderEnabled(bool enabled)
        {
            coll.enabled = enabled;
        }
    }
}
