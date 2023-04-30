using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class Unlocker : MonoBehaviour
    {
        public int id;
        public UnlockerEntryPoint entryPoint;
        public UnlockerTrack track;
        public UnlockerNote notePrefab;
        public UnlockerCrowd crowd;
        public Transform noteDir;
        public GameObject onFinish;
        public List<UnlockerPanel> panels;
        public int score;
        public bool finished;
        public Vector3 noteRot;

        private bool isRunning;
        private float startTime;
        private int currentNote;
        private bool active;

        public void SetActive()
        {
            entryPoint.Activate();
            foreach(UnlockerPanel panel in panels)
            {
                panel.Activate();
            }
            active = true;
        }

        public bool IsActive()
        { 
            return active;
        }

        public void StartGame()
        {
            Game.inst.refs.player.movement.SetPosition(entryPoint.playerPosition.position);
            Game.inst.refs.player.movement.movementEnabled = false;

            score = 0;
            currentNote = 0;
            startTime = Time.time;
            isRunning = true;
        }

        private void Update()
        {
            if(isRunning)
            {
                CheckNextNote();
            }
        }

        private void CheckNextNote()
        {
            if(currentNote >= track.notes.Count)
            {
                // end song with delay
                isRunning = false;
                currentNote = 0;                
                Game.inst.refs.player.movement.movementEnabled = true;

                if(score > 0)
                {
                    Finish();
                }
            }
            else
            {
                UnlockerNoteData nextNote = track.notes[currentNote];
                if (Time.time - startTime >= nextNote.time)
                {
                    SpawnNextNote(nextNote);
                }
            }            
        }

        public void Finish()
        {
            Debug.Log($"finish unlocker {id}");
            finished = true;
            onFinish.SetActive(true);
            entryPoint.Finish();
            Game.inst.stats.OnUnlockerFinished();
        }

        private void SpawnNextNote(UnlockerNoteData noteData)
        {
            UnlockerPanel panel = panels[noteData.value];
            UnlockerNote note = Instantiate(notePrefab, panel.transform);
            note.transform.position = panel.targetPosition.position - noteDir.forward * 2;
            note.moveDir = noteDir.forward;
            note.speed = track.speed;
            note.transform.localEulerAngles = noteRot;

            currentNote++;
        }

        public void OnHitNote()
        {
            score++;
        }

        public void OnMissNote()
        {
            score--;
        }
    }
}
