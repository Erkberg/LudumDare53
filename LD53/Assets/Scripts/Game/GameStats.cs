using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class GameStats : MonoBehaviour
    {
        public int eggsProduced;
        public int eggsPickedUp;
        public int eggsDelivered;
        public int creaturesHatched;
        public int creaturesPickedUp;
        public int creaturesDelivered;
        public int creaturesDied;
        public int unlockersFinished;

        public void OnEggProduced()
        {
            eggsProduced++;
        }

        public void OnEggPickedUp()
        {
            eggsPickedUp++;

            if(eggsPickedUp == 1)
            {
                Game.inst.tutorial.OnFirstEggPickup();
            }
        }

        public void OnEggDeliverd()
        {
            eggsDelivered++;

            if (eggsDelivered == 1)
            {
                Game.inst.tutorial.OnFirstEggDelivered();
            }
        }

        public void OnCreatureHatched()
        {
            creaturesHatched++;

            if (creaturesHatched == 1)
            {
                Game.inst.tutorial.OnFirstEggHatched();
            }
        }

        public void OnCreaturePickedUp()
        {
            creaturesPickedUp++;

            if (creaturesPickedUp == 1)
            {
                Game.inst.tutorial.OnFirstCreaturePickup();
            }
        }

        public void OnCreatureDelivered()
        {
            creaturesDelivered++;

            if (creaturesDelivered == 1)
            {
                Game.inst.tutorial.OnFirstCreatureDelivered();
            }
        }

        public void OnCreatureDied()
        {
            creaturesDied++;
        }

        public void OnUnlockerFinished()
        {
            foreach(Cage cage in Game.inst.refs.cages)
            {
                cage.OnUnlockerFinished(unlockersFinished);
            }

            unlockersFinished++;
            if(unlockersFinished >= Game.inst.refs.unlockers.Count)
            {
                Game.inst.TriggerEnding();
            }
        }
    }
}
