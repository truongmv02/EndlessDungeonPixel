using UnityEngine;

public class ProjectileHitEffect : MonoBehaviour, ISetInfo
{
    ProjectileHitEffectSO Info { set; get; }
    ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    public void SetInfo(object info)
    {
        Info = (ProjectileHitEffectSO)info;
        if (particle == null)
        {
            particle = GetComponent<ParticleSystem>();
        }

        gameObject.SetActive(false);
        particle.textureSheetAnimation.SetSprite(0, Info.sprite);

        SetParticleColor(Info.color);

        SetParticleStartValue(Info.duration, Info.startParticleSize, Info.startParticleSpeed,
            Info.startLifetime, Info.effectGravity, Info.maxParticleNumber);

        SetParticleEmission(Info.emissionRate, Info.burstParticleNumber);
        SetParticleVelocityOverLifetime(Info.velocityOverLifetimeMin, Info.velocityOverLifetimeMax);
        gameObject.SetActive(true);
    }

    private void SetParticleColor(Gradient color)
    {
        var colorOverLifetimeModule = particle.colorOverLifetime;
        colorOverLifetimeModule.color = color;

    }

    private void SetParticleStartValue(float duration, float startSize, float startSpeed, float startLifetime, float gravity, int maxParticleNumber)
    {
        var mainModul = particle.main;
        mainModul.duration = duration;
        mainModul.startSize = startSize;
        mainModul.startSpeed = startSpeed;
        mainModul.startLifetime = startLifetime;
        mainModul.gravityModifier = gravity;
        mainModul.maxParticles = maxParticleNumber;
    }

    private void SetParticleEmission(int emissionRate, int burstParticleNumber)
    {
        var emissionModul = particle.emission;
        var burst = new ParticleSystem.Burst(0f, burstParticleNumber);

        emissionModul.SetBurst(0, burst);
        emissionModul.rateOverTime = emissionRate;
    }

    private void SetParticleVelocityOverLifetime(Vector3 minVelocity, Vector3 maxVelocity)
    {
        var velocityOverLifetimeModule = particle.velocityOverLifetime;

        var minMaxCurveX = new ParticleSystem.MinMaxCurve();
        minMaxCurveX.mode = ParticleSystemCurveMode.TwoConstants;
        minMaxCurveX.constantMin = minVelocity.x;
        minMaxCurveX.constantMax = maxVelocity.x;
        velocityOverLifetimeModule.x = minMaxCurveX;

        var minMaxCurveY = new ParticleSystem.MinMaxCurve();
        minMaxCurveY.mode = ParticleSystemCurveMode.TwoConstants;
        minMaxCurveY.constantMin = minVelocity.y;
        minMaxCurveY.constantMax = maxVelocity.y;
        velocityOverLifetimeModule.y = minMaxCurveY;

        var minMaxCurveZ = new ParticleSystem.MinMaxCurve();
        minMaxCurveZ.mode = ParticleSystemCurveMode.TwoConstants;
        minMaxCurveZ.constantMin = minVelocity.z;
        minMaxCurveZ.constantMax = maxVelocity.z;
        velocityOverLifetimeModule.z = minMaxCurveZ;

    }

}
