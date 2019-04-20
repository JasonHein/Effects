/*
 * By Jason Hein
 * 
 */


using System.Collections.Generic;
using UnityEngine;
using Effects;

/// <summary>
/// The play and stop functions of this controller will set off the play and stop functions off all the effects of child game objects.
/// </summary>
public class EffectController : Effect
{
    // Load all the effects of child game objects.
    List<Effect> m_Effects = new List<Effect>();
    private void Awake()
    {
        m_Effects.AddRange(GetComponentsInChildren<Effect>(true));
        m_Effects.Remove(this);
    }

    /// <summary>
    /// Play all the effects in child game objects.
    /// </summary>
    public override void Play()
    {
        if (CanPlay)
        {
            for (int i = 0; i < m_Effects.Count; ++i)
            {
                m_Effects[i].Play();
            }
        }
    }

    /// <summary>
    /// Stop all the effects in child game objects.
    /// </summary>
    public override void Stop()
    {
        for (int i = 0; i < m_Effects.Count; ++i)
        {
            m_Effects[i].Stop();
        }
    }

    /// <summary>
    /// Returns false if the effects of child game objects are playing, otherwise it returns true.
    /// This is used to determine if the effects can play, and for if the component is available to be reused in an object pool.
    /// </summary>
    public override bool IsAvailable()
    {
        for (int i = 0; i < m_Effects.Count; ++i)
        {
            if (!m_Effects[i].IsAvailable())
            {
                return false;
            }
        }
        return true;
    }

#if UNITY_EDITOR

    // Messages to be sent when you change values in the editor.
    private void OnValidate()
    {
        if (GetComponents<EffectController>().Length > 1)
        {
            Debug.LogError("You cannot have more than one effect controller on a gameobject (it causes infinite recursion). Remove the effect controller you added on " + gameObject.name + ".");
        }
    }

#endif
}
