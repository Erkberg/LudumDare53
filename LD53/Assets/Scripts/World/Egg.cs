using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class Egg : Interactable
    {
        public Creature creature;
        public Hatch hatch;
        public PlayerBasket basket;

        public void OnTakeFromCage()
        {
            if (creature)
            {
                creature.egg = null;
                creature = null;
            }
        }

        public void OnPutIntoHatch(Hatch hatch)
        {
            this.hatch = hatch;
            hatch.egg = this;
        }

        public void OnTakeIntoBasket(PlayerBasket basket)
        {
            this.basket = basket;

            if(creature)
            {
                creature.egg = null;
                creature = null;
            }
            
            if(hatch)
            {
                hatch.ResetState();
                hatch = null;
            }            
        }
    }
}
