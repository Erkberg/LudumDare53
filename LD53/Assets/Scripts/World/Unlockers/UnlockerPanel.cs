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

        private UnlockerNote currentNote;
        private bool justHit;

        private void Update()
        {
            justHit = false;
        }

        public bool OnInteraction()
        {
            if(currentNote)
            {
                unlocker.OnHitNote();
                currentNote.Dissolve();
                currentNote = null;
                justHit = true;
                return true;
            }
            else
            {
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
