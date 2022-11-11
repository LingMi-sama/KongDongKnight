using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem fallTrail;
    [SerializeField] ParticleSystem burstRocks;
    [SerializeField] ParticleSystem lowHealth;
    [SerializeField] ParticleSystem hitL;
    [SerializeField] ParticleSystem hitR;
    [SerializeField] ParticleSystem dustJump;

    public enum EffectType
    {
        FallTrail, BurstRocks, LowHealth, HitL, HitR, DustJump
    }
    public void DoEffect(EffectType effectType, bool enabled)
    {
        switch (effectType)
        {
            case EffectType.FallTrail:
                if (enabled)
                    fallTrail.Play();
                else
                    fallTrail.Stop();
                break;
            case EffectType.BurstRocks:
                if (enabled)
                    burstRocks.Play();
                else
                    burstRocks.Stop();
                break;
            case EffectType.LowHealth:
                if (enabled)
                    lowHealth.Play();
                else
                    lowHealth.Stop();
                break;
            case EffectType.HitL:
                if (enabled)
                    hitL.Play();
                else
                    hitL.Stop();
                break;
            case EffectType.HitR:
                if (enabled)
                    hitR.Play();
                else
                    hitR.Stop();
                break;
            case EffectType.DustJump:
                if (enabled)
                    dustJump.Play();
                else
                    dustJump.Stop();
                break;
            default:
                break;


        }

    }
}
