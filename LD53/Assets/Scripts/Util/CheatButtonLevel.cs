using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace LD53
{
    public class CheatButtonLevel : MonoBehaviour
    {
        private GameInput input;

        private void Awake()
        {
            input = Game.inst.input;
        }

        private void Update()
        {
            if (input.GetCheatLevelButtonDown())
            {
                Game.inst.progress.CheatUnlockNextLevel();
            }            
        }
    }
}
