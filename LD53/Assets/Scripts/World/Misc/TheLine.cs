using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class TheLine : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            PlayerBody playerBody = other.GetComponent<PlayerBody>();
            if(playerBody)
            {
                Game.inst.ui.ShowStoryText(Game.inst.texts.stayInsideText);
            }
        }
    }
}
