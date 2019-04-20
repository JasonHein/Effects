/*
 * By Jason Hein
 * 
 */


using System.Collections;
using UnityEngine;

namespace Effects
{
    /// <summary>
    /// The animation must have "Idle" as it's default starting state for the animation to reset properly.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Effect_SpriteAnim : Effect
    {
        // How long the animation lasts until is is hidden, and if to play on awake.
        [SerializeField] float m_LifeTime = 0f;
        [SerializeField] bool m_PlayOnAwake = true;

        // Saved references
        SpriteRenderer m_Renderer;
        Animator m_Animator;

        /// <summary>
        /// Normalized float between 0 and 1 for where in time the animation begins.
        /// </summary>
        public float timeOffset = 0f;

        // Load references and play the animation if PlayOnAwake is true.
        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_Renderer = GetComponent<SpriteRenderer>();
            if (m_PlayOnAwake)
            {
                Play();
            }
            else
            {
                HideAnimation();
            }
        }

        /// <summary>
        /// Shows and starts the animation, then starts a lifetime timer.
        /// When the timer completes, the animation stops and becomes hidden.
        /// </summary>
        public override void Play()
        {
            if (CanPlay)
            {
                m_Renderer.enabled = true;
                m_Animator.Play(ConstAnim.IDLE, 0, timeOffset);
                if (m_LifeTime > 0f)
                {
                    StartCoroutine(StopAfterDelay());
                }
            }
        }

        /// <summary>
        /// Immediately stops the animation and hides the sprite.
        /// </summary>
        public override void Stop()
        {
            StopCoroutine(StopAfterDelay());
            HideAnimation();
        }

        /// <summary>
        /// Stops the animation and hides the sprite.
        /// </summary>
        void HideAnimation()
        {
            m_Renderer.enabled = false;
            m_Animator.StopPlayback();
        }

        /// <summary>
        /// Returns true if the effect is not playing.
        /// </summary>
        public override bool IsAvailable()
        {
            return !m_Renderer.enabled;
        }

        /// <summary>
        /// Stops the animation after a delay. The delay is based on this effect's lifeTime.
        /// </summary>
        private IEnumerator StopAfterDelay()
        {
            yield return new WaitForSeconds(m_LifeTime);
            HideAnimation();
        }
    }
}
