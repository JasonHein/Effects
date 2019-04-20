/*
 * By Jason Hein
 * 
 */


using UnityEngine;
namespace Effects
{
    /// <summary>
    /// Abstract class for effects to inherit from to be usable by effect controllers and object pools.
    /// </summary>
    public abstract class Effect : MonoBehaviour, Poolable
    {
        /// <summary>
        /// Plays the effect.
        /// </summary>
        public abstract void Play();

        /// <summary>
        /// Stops the effect.
        /// </summary>
        public abstract void Stop();

        /// <summary>
        /// Returns false if the effect is busy.
        /// </summary>
        public abstract bool IsAvailable();

        /// <summary>
        /// Plays the effect at a given 2D position.
        /// </summary>
        public void Play(Vector2 pos)
        {
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
            transform.parent = null;
            Play();
        }

        /// <summary>
        /// Plays the effect at a given position.
        /// </summary>
        public void Play(Vector3 pos)
        {
            transform.position = pos;
            transform.parent = null;
            Play();
        }

        /// <summary>
        /// Plays the effect at a given 2D position, and causes it to follow a given transform.
        /// </summary>
        public void Play(Vector2 pos, Transform parent)
        {
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
            transform.parent = parent;
            Play();
        }

        /// <summary>
        /// Plays the effect at a given position, and causes it to follow a given transform.
        /// </summary>
        public void Play(Vector3 pos, Transform parent)
        {
            transform.position = pos;
            transform.parent = parent;
            Play();
        }

        /// <summary>
        /// Plays the effect at a given 2D position, facing a given direction, and causes it to follow a given transform.
        /// </summary>
        public void Play(Vector2 pos, Transform parent, Vector2 direction)
        {
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
            transform.parent = parent;
            transform.LookAt(transform.position + new Vector3(direction.x, direction.y, 0f));
            Play();
        }

        /// <summary>
        /// Plays the effect at a given position, facing a given direction, and causes it to follow a given transform.
        /// </summary>
        public void Play(Vector3 pos, Transform parent, Vector3 direction)
        {
            transform.position = pos;
            transform.parent = parent;
            transform.LookAt(transform.position + direction);
            Play();
        }

        /// <summary>
        /// Returns false if the effect can't play right now.
        /// </summary>
        public virtual bool CanPlay
        {
            get
            {
                return IsAvailable() && enabled;
            }
        }

        /// <summary>
        /// When the effect is disabled, it stops.
        /// </summary>
        private void OnDisable()
        {
            Stop();
        }
    }
}