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
        public Unlocker unlocker;
        public CreatureEyes eyes;
        public Egg eggPrefab;        
        public int eggsProduced;        

        private float productionTimePassed;
        private float maxEggsProduced = 3;
        private float moveSpeed = 2f;

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
            else if(unlocker)
            {
                Vector3 dir = unlocker.entryPoint.creaturePosition.position - transform.position;
                transform.position += dir.normalized * moveSpeed * Time.deltaTime;
                transform.forward = -dir;
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

            if(unlocker)
            {
                unlocker = null;
            }
        }

        public void Escape(Unlocker unlocker)
        {
            basket = null;
            this.unlocker = unlocker;
            transform.SetPositionY(0.33f);
        }
    }
}
