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
        public bool isRunning;

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

            Game.inst.audio.PlayMusic(track.music);

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
                currentNote = 0;
                startTime = 10000f;
                StartCoroutine(EndSequence());
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

        private IEnumerator EndSequence()
        {
            yield return new WaitForSeconds(3f);
            isRunning = false;            
            Game.inst.refs.player.movement.movementEnabled = true;

            if (score > 0)
            {
                Finish();
            }
        }

        public void Finish()
        {
            Debug.Log($"finish unlocker {id}");
            foreach (UnlockerPanel panel in panels)
            {
                panel.Finish();
            }
            finished = true;
            onFinish.SetActive(true);
            entryPoint.Finish();
            Game.inst.audio.PlayMusic(Game.inst.refs.music);
            Game.inst.stats.OnUnlockerFinished();
        }

        private void SpawnNextNote(UnlockerNoteData noteData)
        {
            UnlockerPanel panel = GetPanelByType(noteData.type);
            UnlockerNote note = Instantiate(notePrefab, panel.transform);
            note.transform.position = panel.targetPosition.position - noteDir.forward * 2;
            note.moveDir = noteDir.forward;
            note.speed = track.speed;
            note.transform.localEulerAngles = noteRot;

            currentNote++;
        }

        private UnlockerPanel GetPanelByType(UnlockerNote.Type type)
        {
            return panels.Find(x => x.type == type);
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
