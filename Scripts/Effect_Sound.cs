/*
 * By Jason Hein
 * 
 */


using UnityEngine;

namespace Effects
{
    /// <summary>
    /// Effect for a sound source. This is essentially an audio source that can be used with Effect Controllers and object pooling.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class Effect_Sound : Effect
    {
        /// <summary>
        /// The volume of the audio source.
        /// </summary>
        public float volume
        {
            get
            {
                return m_Source.volume;
            }
            set
            {
                m_Source.volume = value;
            }
        }

        /// <summary>
        /// The pitch of the audio source.
        /// </summary>
        public float pitch
        {
            get
            {
                return m_Source.pitch;
            }
            set
            {
                m_Source.pitch = value;
            }
        }

        /// <summary>
        /// The stereo pan of the audio source.
        /// </summary>
        public float stereoPan
        {
            get
            {
                return m_Source.panStereo;
            }
            set
            {
                m_Source.panStereo = value;
            }
        }

        /// <summary>
        /// Audio source used for this effect.
        /// </summary>
        public AudioSource source
        {
            get
            {
                return m_Source;
            }
        }

        // Load a reference to the audio source.
        AudioSource m_Source;
        private void Awake()
        {
            m_Source = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Plays the audio clip in the attached audio source.
        /// </summary>
        public override void Play()
        {
            if (CanPlay)
            {
                m_Source.Play();
            }
        }

        /// <summary>
        /// Plays the audio clip in the attached audio source.
        /// A volume can be specified to changes the audio sources volume if the clip is played.
        /// </summary>
        public void Play(float aVolume)
        {
            if (CanPlay)
            {
                volume = aVolume;
                m_Source.Play();
            }
        }

        /// <summary>
        /// Stops the audio source.
        /// </summary>
        public override void Stop()
        {
            m_Source.Stop();
        }

        /// <summary>
        /// Returns false if the audio source is playing.
        /// </summary>
        public override bool IsAvailable()
        {
            return !m_Source.isPlaying;
        }
    }
}