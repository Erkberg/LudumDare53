using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class GameTexts : MonoBehaviour
    {
        [TextArea]
        public string stayInsideText;
        [TextArea]
        public List<string> unlockerTexts;

        [Header("Tutorials")]
        public string eggPickupTutorialText;
        public string eggDeliverTutorialText;
        public string hatchWaitTutorialText;
        public string creaturePickupTutorialText;
        public string creatureDeliverTutorialText;
        public string repeatTutorialText;
    }
}
