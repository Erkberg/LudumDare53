using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LD53
{
    public class Menu : MonoBehaviour
    {
        public static bool justRestarted;

        [Header("State")]
        public State state;

        [Header("Refs")]
        public GameObject holder;
        public TextMeshProUGUI startContinueRestartButtonText;
        public TextMeshProUGUI subtitle;
        public TextMeshProUGUI tutorialStatsText;
        public GameObject quitButton;

        [Header("Config")]
        public bool openOnStart = true;
        public bool pauseTimeWhileOpen = true;
        public string legacyInputSystemMenuButton;
        [Space]
        public string startText = "Start";
        public string continueText = "Continue";
        public string restartText = "Restart";
        public string endingText = "Thank you so much for playing! <3";

        public enum State
        {
            Start,
            Pause,
            End
        }

        private void Start()
        {
            if(justRestarted)
            {
                Close();
                justRestarted = false;
                state = State.Pause;
            }
            else if (openOnStart)
            {
                Open();
            }

#if UNITY_WEBGL
        quitButton.SetActive(false);
#endif
        }

        private void Update()
        {
            CheckMenuButton();
        }

        private void CheckMenuButton()
        {
            if (Game.inst.input.GetMenuButtonDown())
            {
                OnMenuButtonDown();
            }
        }

        private void OnMenuButtonDown()
        {
            if (IsOpen())
            {
                if (state == State.Pause)
                {
                    Close();
                }
            }
            else
            {
                Open();
            }
        }

        public void SetState(State state)
        {
            this.state = state;
        }

        public void Open()
        {
            AdjustToState();
            holder.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Game.inst.ui.storyText.gameObject.SetActive(false);

            if (pauseTimeWhileOpen)
            {
                Time.timeScale = 0f;
            }
        }

        public void Close()
        {
            holder.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Game.inst.ui.storyText.gameObject.SetActive(true);

            if (pauseTimeWhileOpen)
            {
                Time.timeScale = 1f;
            }
        }

        private void AdjustToState()
        {
            switch (state)
            {
                case State.Start:
                    startContinueRestartButtonText.text = startText;
                    break;

                case State.Pause:
                    startContinueRestartButtonText.text = continueText;
                    break;

                case State.End:
                    startContinueRestartButtonText.text = restartText;
                    subtitle.text = endingText;
                    tutorialStatsText.text = GetStatsText();
                    break;
            }
        }

        public void OnStartContinueRestartButtonClicked()
        {
            switch (state)
            {
                case State.Start:
                    Close();
                    Game.inst.tutorial.OnGameStart();
                    state = State.Pause;
                    break;

                case State.Pause:
                    Close();
                    break;

                case State.End:
                    justRestarted = true;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    break;
            }
        }

        public bool IsOpen()
        {
            return holder.activeSelf;
        }

        public void OnQuitButtonClicked()
        {
            Application.Quit();
        }

        private string GetStatsText()
        {
            string s = string.Empty;

            s += $"Eggs produced: {Game.inst.stats.eggsProduced}\n";
            s += $"Creatures hatched: {Game.inst.stats.creaturesHatched}\n";
            s += $"Creatures died: {Game.inst.stats.creaturesDied}\n";

            return s;
        }
    }
}
