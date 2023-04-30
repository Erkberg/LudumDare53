using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD53
{
    [CreateAssetMenu]
    public class UnlockerTrack : ScriptableObject
    {
        public AudioClip music;
        public List<UnlockerNoteData> notes;
        public float speed = 1f;
    }
}
