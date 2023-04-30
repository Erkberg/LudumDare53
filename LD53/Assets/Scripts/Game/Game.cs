using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class Game : MonoBehaviour
    {
        public static Game inst;

        public GameInput input;
        public GameProgress progress;
        public GameRefs refs;
        public GameUI ui;
        public GameTexts texts;
        public GameStats stats;
        public GameTutorial tutorial;

        private void Awake()
        {
            inst = this;            
        }

        public void TriggerEnding()
        {
            StartCoroutine(EndingSequence());
        }

        private IEnumerator EndingSequence()
        {
            yield return null;
            ui.menu.SetState(Menu.State.End);
            ui.menu.Open();
        }
    }
}