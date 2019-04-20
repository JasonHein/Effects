using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class Effect_SoundShuffle : Effect_Sound
    {
        [SerializeField] protected List<AudioClip> m_Clips;

        /// <summary>
        /// Plays a random audio clip in the list.
        /// </summary>
        public override void Play()
        {
            if (CanPlay)
            {
#if UNITY_EDITOR
                if (m_Clips.Count == 0)
                {
                    Debug.Log(gameObject.name + ": Effect_SoundRandom has no sounds in the sound list.");
                    return;
                }
#endif
                source.clip = m_Clips[Random.Range(0, m_Clips.Count - 1)];
                source.Play();
            }
        }
    }
}
