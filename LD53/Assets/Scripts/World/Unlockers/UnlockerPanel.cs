using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class UnlockerPanel : Interactable
    {
        public Unlocker unlocker;
        public int id;
        public Transform notesHolder;
        public Animator animator;

        private UnlockerNote currentNote;
        private bool justHit;

        private const string WrongNoteTrigger = "wrong";
        private const string RightNoteTrigger = "right";

        private void Update()
        {
            justHit = false;
        }

        public void Activate()
        {
            animator.gameObject.SetActive(true);
        }

        public bool OnInteraction()
        {
            if(currentNote)
            {
                animator.SetTrigger(RightNoteTrigger);
                unlocker.OnHitNote();
                currentNote.Dissolve();
                currentNote = null;
                justHit = true;
                return true;
            }
            else
            {
                animator.SetTrigger(WrongNoteTrigger);
                unlocker.OnMissNote();
                return false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            UnlockerNote note = other.GetComponent<UnlockerNote>();
            if(note)
            {
                currentNote = note;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            UnlockerNote note = other.GetComponent<UnlockerNote>();
            if (note && note == currentNote)
            {
                if(justHit)
                {
                    justHit = false;
                }
                else
                {
                    currentNote.Dissolve();
                    unlocker.OnMissNote();
                }
                
                currentNote = null;
            }
        }
    }
}
