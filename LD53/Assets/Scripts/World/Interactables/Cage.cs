using ErksUnityLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class Cage : UnlockableInteractable
    {
        public Transform eggSpawnPosition;
        public Creature creature;
        public List<CageBars> cageBars;

        public void OnPutCreatureIntoCage(Creature creature)
        {
            this.creature = creature;
            creature.cage = this;
        }

        public void OnUnlockerFinished(int id)
        {
            cageBars[id].RemoveBars();
        }
    }
}
