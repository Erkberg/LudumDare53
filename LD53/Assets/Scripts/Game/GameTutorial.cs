using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class GameTutorial : MonoBehaviour
    {
        public Transform tutorialHighlight;

        public Transform eggPosition;
        public Transform hatchPosition;
        public Transform cagePosition;

        private GameTexts texts;

        private void Awake()
        {
            texts = Game.inst.texts;
        }

        public void OnGameStart()
        {
            tutorialHighlight.position = eggPosition.position;
            tutorialHighlight.gameObject.SetActive(true);
            Game.inst.ui.ShowStoryText(texts.eggPickupTutorialText, false);
        }

        public void OnFirstEggPickup()
        {
            tutorialHighlight.position = hatchPosition.position;
            Game.inst.ui.ShowStoryText(texts.eggDeliverTutorialText, false);
        }

        public void OnFirstEggDelivered()
        {
            tutorialHighlight.gameObject.SetActive(false);
            Game.inst.ui.ShowStoryText(texts.hatchWaitTutorialText, false);
        }

        public void OnFirstEggHatched()
        {
            tutorialHighlight.gameObject.SetActive(true);
            Game.inst.ui.ShowStoryText(texts.creaturePickupTutorialText, false);
        }

        public void OnFirstCreaturePickup()
        {
            tutorialHighlight.position = cagePosition.position;
            Game.inst.ui.ShowStoryText(texts.creatureDeliverTutorialText, false);
        }

        public void OnFirstCreatureDelivered()
        {
            tutorialHighlight.gameObject.SetActive(false);
            Game.inst.ui.ShowStoryText(texts.repeatTutorialText);
        }
    }
}
