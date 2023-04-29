using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class PlayerInteraction : MonoBehaviour
    {
        public PlayerController pc;
        public PlayerBasket basket;
        public Transform camMuzzle;
        public Animator crosshairAnimator;        
        public float range = 4f;

        private Interactable focusedInteractable;

        private const string CrosshairHighlightedBool = "highlighted";
        private const string CrosshairWrongTrigger = "wrong";

        private void Update()
        {
            if(pc.IsControllable())
            {
                CheckInteractable();
                CheckInteraction();
            }
        }

        private void CheckInteractable()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(camMuzzle.position, camMuzzle.forward, out hit, range);
            if(hasHit)
            {
                focusedInteractable = hit.collider.GetComponent<Interactable>();                
            }
            else
            {
                focusedInteractable = null;                
            }

            crosshairAnimator.SetBool(CrosshairHighlightedBool, focusedInteractable != null);
        }

        private void CheckInteraction()
        {
            if(focusedInteractable != null)
            {
                if(Game.inst.input.GetInteractButtonDown())
                {
                    Interact();
                }
            }
        }

        private void Interact()
        {
            bool success = false;

            if(focusedInteractable.GetType() == typeof(Cage))
            {
                Cage cage = (Cage)focusedInteractable;
                Creature carriedCreature = basket.GetCarriedCreature();
                if (carriedCreature && !cage.creature)
                {
                    basket.RemoveFromCarriedInteractables(carriedCreature, focusedInteractable.targetPosition);
                    cage.OnPutCreatureIntoCage(carriedCreature);
                    success = true;
                    Game.inst.progress.CheckNextLevel();
                }
            }
            else if (focusedInteractable.GetType() == typeof(Creature))
            {
                Creature creature = (Creature)focusedInteractable;
                success = basket.AddToCarriedInteractables(focusedInteractable);
                if(success)
                {
                    creature.OnTakeIntoBasket(basket);
                }                
            }
            else if (focusedInteractable.GetType() == typeof(Egg))
            {
                Egg egg = (Egg)focusedInteractable;    
                success = basket.AddToCarriedInteractables(focusedInteractable);
                if(success)
                {
                    egg.OnTakeIntoBasket(basket);
                }
            }
            else if (focusedInteractable.GetType() == typeof(Hatch))
            {
                Hatch hatch = (Hatch)focusedInteractable;
                Egg carriedEgg = basket.GetCarriedEgg();
                if(carriedEgg)
                {
                    basket.RemoveFromCarriedInteractables(carriedEgg, focusedInteractable.targetPosition);
                    carriedEgg.OnPutIntoHatch(hatch);
                    hatch.SetColliderEnabled(false);
                    success = true;
                }
            }

            if(!success)
            {
                crosshairAnimator.SetTrigger(CrosshairWrongTrigger);
            }
        }
    }
}