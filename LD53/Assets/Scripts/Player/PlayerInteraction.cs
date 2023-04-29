using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class PlayerInteraction : MonoBehaviour
    {
        public PlayerController pc;
        public Transform camMuzzle;
        public Animator crosshairAnimator;
        public float range = 4f;
        public int maxCarryAmount = 1;

        private Interactable focusedInteractable;
        private List<Interactable> carriedInteractables = new List<Interactable>();

        private const string CrosshairHighlightedBool = "highlighted";

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
            focusedInteractable.OnInteract();

            if(focusedInteractable.GetType() == typeof(Cage))
            {

            }
            else if (focusedInteractable.GetType() == typeof(Creature))
            {

            }
            else if (focusedInteractable.GetType() == typeof(Egg))
            {

            }
            else if (focusedInteractable.GetType() == typeof(Hatch))
            {

            }
        }
    }
}