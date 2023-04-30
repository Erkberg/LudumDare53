using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class UnlockerNote : MonoBehaviour
    {
        public Collider coll;
        public int value;
        public Vector3 moveDir;
        public float speed;

        public enum Type
        {
            Cymbal,
            Kick,
            Snare
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            transform.position += moveDir * speed * Time.deltaTime;
        }

        public void Dissolve()
        {
            Destroy(gameObject);
        }
    }
}
