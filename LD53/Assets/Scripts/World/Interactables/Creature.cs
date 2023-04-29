using ErksUnityLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class Creature : Interactable
    {
        public Cage cage;
        public Hatch hatch;
        public PlayerBasket basket;
        public Egg egg;
        public CreatureEyes eyes;
        public Egg eggPrefab;        
        public int eggsProduced;        

        private float productionTimePassed;
        private float maxEggsProduced = 3;

        private void Update()
        {
            CheckState();
        }

        private void CheckState()
        {
            if(cage != null)
            {
                if (!egg)
                {
                    Timing.AddTimeAndCheckMax(ref productionTimePassed, Game.inst.progress.GetProductionTime(), Time.deltaTime, ProduceEgg);
                }
            }
        }

        private void ProduceEgg()
        {
            eggsProduced++;

            if (eggsProduced >= maxEggsProduced)
            {
                //egg.creature = null;
                cage.creature = null;
                Destroy(gameObject);
            }
            else
            {
                Egg egg = Instantiate(eggPrefab, cage.eggSpawnPosition);
                egg.transform.localPosition = Vector3.zero;
                egg.transform.localRotation = Quaternion.identity;
                egg.creature = this;
                this.egg = egg;
                eyes.SetEyesScale(1f - eggsProduced / maxEggsProduced);
            }
        }

        public void OnTakeIntoBasket(PlayerBasket basket)
        {
            this.basket = basket;

            if(cage)
            {
                cage = null;
            }            

            if (hatch)
            {
                hatch.ResetState();
                hatch = null;
            }
        }
    }
}
