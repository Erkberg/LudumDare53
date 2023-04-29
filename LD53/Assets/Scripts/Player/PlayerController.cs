using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerMovement movement;
        public PlayerBody body;
        public PlayerInteraction interaction;

        public bool IsControllable()
        {
            return Time.timeScale != 0f;
        }
    }
}