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

        [Header("Sounds")]
        public AudioClip wrongClip;
        public AudioClip takeClip;
        public AudioClip putClip;

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
                    Game.inst.audio.PlaySoundRandomVolumePitch(putClip, 0.5f);
                    Game.inst.progress.CheckNextLevel();
                    Game.inst.stats.OnCreatureDelivered();
                }
            }
            else if (focusedInteractable.GetType() == typeof(Creature))
            {
                Creature creature = (Creature)focusedInteractable;
                success = basket.AddToCarriedInteractables(focusedInteractable);
                if(success)
                {
                    creature.OnTakeIntoBasket(basket);
                    Game.inst.audio.PlaySoundRandomVolumePitch(takeClip, 0.33f);
                    Game.inst.stats.OnCreaturePickedUp();
                }                
            }
            else if (focusedInteractable.GetType() == typeof(Egg))
            {
                Egg egg = (Egg)focusedInteractable;    
                success = basket.AddToCarriedInteractables(focusedInteractable);
                if(success)
                {
                    egg.OnTakeIntoBasket(basket);
                    Game.inst.audio.PlaySoundRandomVolumePitch(takeClip, 0.33f);
                    Game.inst.stats.OnEggPickedUp();
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
                    Game.inst.audio.PlaySoundRandomVolumePitch(putClip, 0.5f);
                    Game.inst.stats.OnEggDeliverd();
                }
            }
            else if (focusedInteractable.GetType() == typeof(UnlockerPanel))
            {
                UnlockerPanel panel = (UnlockerPanel)focusedInteractable;
                success = panel.OnInteraction();
            }

            if (!success)
            {
                Game.inst.audio.PlaySoundRandomVolumePitch(wrongClip);
                crosshairAnimator.SetTrigger(CrosshairWrongTrigger);
            }
        }
    }
}