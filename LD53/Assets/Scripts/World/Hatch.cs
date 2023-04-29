using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class Hatch : Interactable
    {
        public State state;

        public enum State
        {
            Empty,
            Hatching,
            Hatched
        }
    }
}
