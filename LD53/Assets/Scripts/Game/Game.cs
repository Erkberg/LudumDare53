using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class Game : MonoBehaviour
    {
        public static Game inst;

        public GameInput input;

        private void Awake()
        {
            inst = this;
        }
    }
}