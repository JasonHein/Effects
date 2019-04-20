/*
 * By Jason Hein
 * 
 */


using UnityEngine;

namespace Effects
{
    /// <summary>
    /// Effect for particle effects. This is a particle system that can be used with effect controllers and object pools.
    /// </summary>
    [RequireComponent(typeof(ParticleSystem))]
    public class Effect_Particles : Effect
    {
        // Load the particle system
        ParticleSystem m_System;
        private void Awake()
        {
            m_System = GetComponent<ParticleSystem>();
        }

        /// <summary>
        /// The main module of the particle system.
        /// </summary>
        public ParticleSystem.MainModule mainModule
        {
            get
            {
                return m_System.main;
            }
        }

        /// <summary>
        /// The emmission module of the particle system.
        /// </summary>
        public ParticleSystem.EmissionModule emissionModule
        {
            get
            {
                return m_System.emission;
            }
        }

        /// <summary>
        /// Play the particle system.
        /// </summary>
        public override void Play()
        {
            if (CanPlay)
            {
                m_System.Play();
            }
        }

        /// <summary>
        /// Stop the particle system.
        /// </summary>
        public override void Stop()
        {
            m_System.Stop();
        }

        /// <summary>
        /// Returns false of the particle system is playing.
        /// </summary>
        public override bool IsAvailable()
        {
            return !m_System.isPlaying;
        }

        /// <summary>
        /// Override can play, to check if the system is emitting, instead of playing.
        /// </summary>
        public override bool CanPlay
        {
            get
            {
                return (!m_System.isEmitting) && enabled;
            }
        }
    }
}
