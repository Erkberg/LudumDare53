using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class UnlockerPanel : Interactable
    {
        public Unlocker unlocker;
        public UnlockerNote.Type type;
        public Transform notesHolder;
        public Animator animator;
        public GameObject tutorial;
        public bool currentlyInteracting;

        private UnlockerNote currentNote;
        private bool justHit;
        private GameInput input;

        private const string WrongNoteTrigger = "wrong";
        private const string RightNoteTrigger = "right";

        private void Awake()
        {
            input = Game.inst.input;
        }

        private void Update()
        {
            justHit = false;

            if(IsActive() && unlocker.isRunning)
            {
                CheckInput();
            }
        }

        public void Activate()
        {
            animator.gameObject.SetActive(true);
            tutorial.SetActive(true);
        }

        public bool IsActive()
        {
            return animator.gameObject.activeSelf;
        }

        public void Finish()
        {
            tutorial.SetActive(false);
        }

        private void CheckInput()
        {
            switch (type)
            {
                case UnlockerNote.Type.Cymbal:
                    if(input.GetPanelCymbalButtonDown())
                    {
                        OnInteraction();
                    }
                    break;

                case UnlockerNote.Type.Kick:
                    if (input.GetPanelKickButtonDown())
                    {
                        OnInteraction();
                    }
                    break;

                case UnlockerNote.Type.Snare:
                    if (input.GetPanelSnareButtonDown())
                    {
                        OnInteraction();
                    }
                    break;
            }
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
