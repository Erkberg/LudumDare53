using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class Creature : Interactable
    {
        public State state;
        public int eggsProduced;

        public enum State
        {
            Producing,
            Carried,
            Hatched,
            Escaping
        }

        
    }
}
