using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LD53
{
    public class GameRefs : MonoBehaviour
    {
        public PlayerController player;
        public List<Cage> cages;
        public List<Hatch> hatches;

        public List<Cage> GetNextLockedCages(int amount)
        {
            List<Cage> nextLockedCages = new List<Cage>();
            foreach(Cage cage in cages)
            {
                if(!cage.unlocked)
                {
                    nextLockedCages.Add(cage);
                    if(nextLockedCages.Count >= amount)
                    {
                        break;
                    }
                }
            }

            return nextLockedCages;
        }

        public List<Hatch> GetNextLockedHatches(int amount)
        {
            List<Hatch> nextLockedHatches = new List<Hatch>();
            foreach (Hatch hatch in hatches)
            {
                if (!hatch.unlocked)
                {
                    nextLockedHatches.Add(hatch);
                    if (nextLockedHatches.Count >= amount)
                    {
                        break;
                    }
                }
            }

            return nextLockedHatches;
        }
    }
}
