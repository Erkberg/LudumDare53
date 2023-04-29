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
        public Transform noteDir;
        public List<UnlockerPanel> panels;
        public int score;

        private bool isRunning;
        private float startTime;
        private int currentNote;

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
                Debug.Log("end song");
                isRunning = false;
                currentNote = 0;                
                Game.inst.refs.player.movement.movementEnabled = true;
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

        private void SpawnNextNote(UnlockerNoteData noteData)
        {
            UnlockerPanel panel = panels[noteData.value];
            UnlockerNote note = Instantiate(notePrefab, panel.transform);
            note.transform.position = panel.targetPosition.position - noteDir.forward * 2;
            note.moveDir = noteDir.forward;
            note.speed = track.speed;

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
