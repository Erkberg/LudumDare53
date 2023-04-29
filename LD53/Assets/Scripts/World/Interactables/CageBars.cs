using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class CageBars : MonoBehaviour
    {
        public List<Transform> bars;

        private const string ShrinkTrigger = "shrink";

        public void RemoveBars()
        {
            foreach(Transform bar in bars)
            {
                bar.GetComponent<Animator>().SetTrigger(ShrinkTrigger);
            }
        }
    }
}
