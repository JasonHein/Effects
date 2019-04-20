/*
 * By Jason Hein
 * 
 */


using System.Collections;
using UnityEngine;

namespace Effects
{
    /// <summary>
    /// Effect for playing sprite animations. This shows and plays an animation for a set time, then hides.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class Effect_Animation : Effect
    {
        [SerializeField] float m_LifeTime = 0f;
        [SerializeField] bool m_PlayOnAwake = true;
        Animator m_Animator;
        GameObject[] m_Children;
        bool m_Hidden = true;

        /// <summary>
        /// Normalized between 0 and 1.
        /// </summary>
        public float timeOffset = 0f;

        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_Children = new GameObject[transform.childCount];
            for (int i = 0; i < m_Children.Length; ++i)
            {
                m_Children[i] = transform.GetChild(i).gameObject;
            }
            m_Hidden = !m_PlayOnAwake;
            if (m_PlayOnAwake)
            {
                if (m_LifeTime > 0f)
                {
                    StartCoroutine(StopAfterDelay());
                }
            }
            else
            {
                HideAnimation();
            }
        }

        public override void Play()
        {
            if (CanPlay)
            {
                m_Hidden = false;
                m_Animator.Play(ConstAnim.IDLE, 0, timeOffset);
                for (int i = 0; i < m_Children.Length; ++i)
                {
                    m_Children[i].SetActive(true);
                }
                if (m_LifeTime > 0f)
                {
                    StartCoroutine(StopAfterDelay());
                }
            }
        }

        public override void Stop()
        {
            StopCoroutine(StopAfterDelay());
            HideAnimation();
        }

        void HideAnimation ()
        {
            m_Hidden = true;
            m_Animator.StopPlayback();
            for (int i = 0; i < m_Children.Length; ++i)
            {
                m_Children[i].SetActive(false);
            }
        }

        public override bool IsAvailable()
        {
            return m_Hidden;
        }

        private IEnumerator StopAfterDelay()
        {
            yield return new WaitForSeconds(m_LifeTime);
            HideAnimation();
        }
    }
}
