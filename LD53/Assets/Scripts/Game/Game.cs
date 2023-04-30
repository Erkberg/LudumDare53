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
    }
}