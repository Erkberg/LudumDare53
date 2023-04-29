using ErksUnityLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    public class Hatch : Interactable
    {
        public Egg egg;
        public Creature creature;
        public Creature creaturePrefab;

        private float hatchTimePassed;
        private float hatchTime = 6f;

        private void Update()
        {
            CheckState();
        }

        private void CheckState()
        {
            if(egg)
            {
                Timing.AddTimeAndCheckMax(ref hatchTimePassed, hatchTime, Time.deltaTime, HatchEgg);
            }
        }

        private void HatchEgg()
        {
            Destroy(egg.gameObject);
            egg = null;

            Creature creature = Instantiate(creaturePrefab, targetPosition);
            creature.transform.localPosition = Vector3.zero;
            creature.transform.localRotation = Quaternion.identity;
            creature.hatch = this;
            this.creature = creature;
        }

        public void ResetState()
        {
            hatchTimePassed = 0f;
            egg = null;
            creature = null;
            SetColliderEnabled(true);
        }
    }
}
