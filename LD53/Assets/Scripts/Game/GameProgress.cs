using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class GameProgress : MonoBehaviour
    {
        public List<GameLevel> levels;
        public int initiallyUnlocked = 3;
        public int currentLevel = 0;

        public void CheckNextLevel()
        {
            if (levels[currentLevel].IsFinished())
            {
                if(currentLevel + 1 < levels.Count)
                {
                    UnlockNextLevel();
                }                
            }
        }

        public void CheatUnlockNextLevel()
        {
            if (currentLevel + 1 < levels.Count)
            {
                UnlockNextLevel();
            }
            else
            {
                foreach(Unlocker unlocker in Game.inst.refs.unlockers)
                {
                    if(!unlocker.finished)
                    {
                        unlocker.Finish();
                        break;
                    }
                }
            }
        }

        private void UnlockNextLevel()
        {
            foreach(Cage cage in Game.inst.refs.GetNextLockedCages(levels[currentLevel].unlockedCagesHatches))
            {
                cage.Unlock();
            }

            foreach (Hatch hatch in Game.inst.refs.GetNextLockedHatches(levels[currentLevel].unlockedCagesHatches))
            {
                hatch.Unlock();
            }

            if(levels[currentLevel].increaseBasketSize)
            {
                Game.inst.refs.player.interaction.basket.maxCarryAmount++;
                Game.inst.refs.player.movement.moveSpeed += 0.5f;
            }

            Game.inst.ui.ShowStoryText(levels[currentLevel].textLine);
            levels[currentLevel].cageUnlocker?.SetActive();
            // TODO: cam shake

            currentLevel++;
        }

        public float GetProductionTime()
        {
            return levels[currentLevel].productionTime;
        }

        public float GetHatchTime()
        {
            return levels[currentLevel].hatchTime;
        }
    }
}
