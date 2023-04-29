using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class Cage : Interactable
    {
        public Transform eggSpawnPosition;
        public Creature creature;

        public void OnPutCreatureIntoCage(Creature creature)
        {
            this.creature = creature;
            creature.cage = this;
        }
    }
}
