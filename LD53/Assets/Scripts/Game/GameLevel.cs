using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    [System.Serializable]
    public class GameLevel
    {
        public int cagesToProgress;
        public int unlockedCagesHatches;
        public float productionTime = 6f;
        public float hatchTime = 8f;
        public CageUnlocker cageUnlocker;
        public bool increaseBasketSize;
        public string textLine;

        public bool IsFinished()
        {
            int cagesFinished = 0;

            foreach(Cage cage in Game.inst.refs.cages)
            {
                if(cage.creature)
                {
                    cagesFinished++;
                }
            }

            return cagesFinished >= cagesToProgress;
        }
    }
}
