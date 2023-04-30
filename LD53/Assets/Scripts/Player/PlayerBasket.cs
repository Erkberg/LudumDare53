using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

namespace LD53
{
    public class PlayerBasket : MonoBehaviour
    {
        public int maxCarryAmount = 1;
        public List<PlayerSubBasket> subBaskets;

        private List<Interactable> carriedInteractables = new List<Interactable>();

        private void Update()
        {
            CheckCreatureEscape();
        }

        private void CheckCreatureEscape()
        {
            if (Random.value < 0.001f && Game.inst.refs.GetActivatedUnlockerAmount() > 1)
            {
                Creature creature = GetCarriedCreature();
                if (creature)
                {
                    Unlocker unlocker = Game.inst.refs.GetActivatedUnfinishedUnlocker();
                    if(unlocker)
                    {
                        GetCurrentSubBasket().RemoveInteractable(creature);
                        carriedInteractables.Remove(creature);
                        creature.transform.parent = null;
                        creature.Escape(unlocker);
                    }
                }
            }                
        }

        public bool AddToCarriedInteractables(Interactable interactable)
        {
            if (carriedInteractables.Count < maxCarryAmount)
            {
                carriedInteractables.Add(interactable);
                PlayerSubBasketPosition subBasketPosition = GetCurrentSubBasket().GetNextFreePosition();
                if(subBasketPosition)
                {
                    subBasketPosition.interactable = interactable;
                    interactable.transform.parent = subBasketPosition.transform;
                    interactable.transform.localPosition = Vector3.zero;
                    return true;
                }
                return false;
            }

            return false;
        }

        public bool RemoveFromCarriedInteractables(Interactable interactable, Transform targetTransform)
        {
            if (carriedInteractables.Contains(interactable))
            {
                GetCurrentSubBasket().RemoveInteractable(interactable);
                carriedInteractables.Remove(interactable);
                interactable.transform.parent = targetTransform;
                interactable.transform.localPosition = Vector3.zero;
                interactable.transform.rotation = targetTransform.rotation;
                return true;
            }

            return false;
        }

        public Creature GetCarriedCreature()
        {
            return carriedInteractables.Find(x => x.GetType() == typeof(Creature)) as Creature;
        }

        public Egg GetCarriedEgg()
        {
            return carriedInteractables.Find(x => x.GetType() == typeof(Egg)) as Egg;
        }

        private PlayerSubBasket GetCurrentSubBasket()
        {
            return subBaskets[maxCarryAmount - 1];
        }
    }
}
