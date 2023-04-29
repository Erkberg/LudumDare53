using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

namespace LD53
{
    public class PlayerBasket : MonoBehaviour
    {
        public int maxCarryAmount = 1;

        private List<Interactable> carriedInteractables = new List<Interactable>();

        public bool AddToCarriedInteractables(Interactable interactable)
        {
            if (carriedInteractables.Count < maxCarryAmount)
            {
                carriedInteractables.Add(interactable);
                interactable.transform.parent = transform;
                interactable.transform.localPosition = Vector3.zero;
                return true;
            }

            return false;
        }

        public bool RemoveFromCarriedInteractables(Interactable interactable, Transform targetTransform)
        {
            if (carriedInteractables.Contains(interactable))
            {
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
    }
}
