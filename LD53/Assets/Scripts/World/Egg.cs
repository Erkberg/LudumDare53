using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class Egg : Interactable
    {
        public State state;

        public enum State
        {
            Produced,
            Carried,
            Hatching
        }
    }
}
