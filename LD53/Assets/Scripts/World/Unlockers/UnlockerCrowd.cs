using ErksUnityLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class UnlockerCrowd : MonoBehaviour
    {
        public List<UnlockerCrowdPerson> crowdPersons;
        public UnlockerCrowdButton button;

        public void OnHit()
        {
            GetRandomPerson().Cheer();
        }

        public void OnMiss()
        {
            GetRandomPerson().Boo();
        }

        public void OnFinish()
        {
            foreach(UnlockerCrowdPerson person in crowdPersons)
            {
                person.Finish();
            }
        }

        public UnlockerCrowdPerson GetRandomPerson()
        {
            return crowdPersons.GetRandomItem();
        }
    }
}
