using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LD53
{
    public class GameUI : MonoBehaviour
    {
        public TextMeshProUGUI storyText;
        public float storyTextDuration = 6.67f;

        public void ShowStoryText(string text, bool disappearAfter = true)
        {
            storyText.text = text;
            StopAllCoroutines();

            if (disappearAfter )
            {
                StartCoroutine(ShowStoryTextSequence());
            }
        }

        public void HideStoryText()
        {
            storyText.text = string.Empty;
        }

        private IEnumerator ShowStoryTextSequence()
        {
            yield return new WaitForSeconds(storyTextDuration);
            HideStoryText();
        }
    }
}
