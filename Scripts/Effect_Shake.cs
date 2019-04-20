/*
 * By Jason Hein
 * 
 */


using GameEye2D.Behaviour;

namespace Effects
{
    /// <summary>
    /// Effect for camera shake. Causes camera shake when played for a given length and power.
    /// This effect is mostly for when you are creating camera shake effect's at runtime.
    /// If the effect is created earlier in the scene, then there are more optimal ways to get a reference to the camera shake object.
    /// Look at Shake.ShakeCam () for more camera shake options.
    /// </summary>
    public class Effect_Shake : Effect
    {
        // Camera shake is actually a randomized spin. You can specify the amount and length.
        public float spin = 8f;
        public float length = 0.35f;

        // Find a camera shake object.
        // This is quite un optimal, but if you are creating this object at runtime you don't have a lot of options.
        Shake m_Shake;
        private void Start()
        {
            m_Shake = FindObjectOfType<Shake>();
        }

        /// <summary>
        /// Causes the camera to shake.
        /// </summary>
        public override void Play()
        {
            if (CanPlay)
            {
                if (m_Shake)
                {
                    m_Shake.ShakeCam(spin, length);
                }
            }
        }

        /// <summary>
        /// Stops the camera from shaking.
        /// </summary>
        public override void Stop()
        {
            if (m_Shake)
            {
                m_Shake.Stop();
            }
        }

        /// <summary>
        /// Returns false if the camera is shaking.
        /// </summary>
        public override bool IsAvailable()
        {
            if (m_Shake)
            {
                return !m_Shake.isShaking;
            }
            return false;
        }
    }
}
