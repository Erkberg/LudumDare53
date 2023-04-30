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
        private int scoreTreshold = 1;

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
            crowd.gameObject.SetActive(true);

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

        public void CheatEnd()
        {
            currentNote = 0;
            score = scoreTreshold + 1;
            startTime = 10000f;
            StartCoroutine(EndSequence());
        }

        private IEnumerator EndSequence()
        {            
            yield return new WaitForSeconds(3f);            
            isRunning = false;    

            if (score > scoreTreshold)
            {
                crowd.OnFinish();
                Game.inst.refs.unlockerCam.SetActive(true);
                yield return new WaitForSeconds(1f);
                Finish();
                yield return new WaitForSeconds(3.33f);
                Game.inst.ui.ShowStoryText(Game.inst.texts.unlockerTexts[id]);
                if (Game.inst.stats.unlockersFinished >= Game.inst.refs.unlockers.Count)
                {
                    yield return new WaitForSeconds(9f);
                    Game.inst.TriggerEnding();
                }
                else
                {
                    Game.inst.refs.player.movement.movementEnabled = true;
                    Game.inst.refs.unlockerCam.SetActive(false);
                }
            }
            else
            {
                Game.inst.refs.player.movement.movementEnabled = true;
                Game.inst.audio.PlayMusic(Game.inst.refs.music);
                crowd.gameObject.SetActive(false);
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
            crowd.OnHit();
            score++;
        }

        public void OnMissNote()
        {
            crowd.OnMiss();
            score--;
        }
    }
}
