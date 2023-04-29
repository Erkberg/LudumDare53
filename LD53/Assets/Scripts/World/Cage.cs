using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class Cage : Interactable
    {
        public State state;

        public enum State
        {
            Empty,
            Filled
        }
    }
}
