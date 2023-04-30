using ErksUnityLibrary;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LD53
{
    public class UnlockerCrowdPerson : MonoBehaviour
    {
        public TextMeshProUGUI text;
        public List<string> cheerLines;
        public List<string> booLines;
        public List<string> finishLines;

        public void Cheer()
        {
            ShowText(cheerLines.GetRandomItem(), true);
        }

        public void Boo()
        {
            ShowText(booLines.GetRandomItem(), true);
        }

        public void Finish()
        {
            Debug.Log("finish");
            ShowText(finishLines.GetRandomItem(), false);
        }

        public void ShowText(string line, bool disappear)
        {
            StopAllCoroutines();
            text.text = line;
            if(disappear)
            {
                StartCoroutine(TextDisappearSequence());
            }            
        }

        private IEnumerator TextDisappearSequence()
        {
            yield return new WaitForSeconds(3.33f);
            text.text = string.Empty;
        }
    }
}
