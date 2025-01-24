using UnityEngine;
using UnityEngine.Serialization;

public class RopeSimulation : MonoBehaviour
{
    public PlayerHook playerHook;
    
    public int quality = 200;
    
    public float damper = 14;
    public float strength = 800;
    public float velocity = 15;
    
    public float waveCount = 3;
    
    public float waveHeight = 1;
    
    public AnimationCurve affectCurve;
    
    private SpringSimulation spring;
    
    private LineRenderer lr;
    
    private Vector3 currentGrapplePosition;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        spring = new SpringSimulation();
        spring.SetTarget(0);
    }
    
    private void LateUpdate()
    {
        DrawRope();
    }

    void DrawRope()
    {
        if (!playerHook.grappling)
        {
            currentGrapplePosition = playerHook.hand.position;
            
            spring.Reset();
            
            if (lr.positionCount > 0)
                lr.positionCount = 0;

            return;
        }
        
        if (lr.positionCount == 0)
        {
            spring.SetVelocity(velocity);
            lr.positionCount = quality + 1;
        }
        
        spring.SetDamper(damper);
        spring.SetStrength(strength);
        spring.Update(Time.deltaTime);


        Vector3 grapplePoint = playerHook.activePoint.transform.position;
        Vector3 gunTipPosition = playerHook.hand.position;


        Vector3 up = Quaternion.LookRotation((grapplePoint - gunTipPosition).normalized) * Vector3.up;


        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);
        
        for (int i = 0; i < quality + 1; i++)
        {
            float delta = i / (float)quality;
            Vector3 offset = up * waveHeight * Mathf.Sin(delta * waveCount * Mathf.PI) * spring.Value *
                             affectCurve.Evaluate(delta);
            
            lr.SetPosition(i, Vector3.Lerp(gunTipPosition, currentGrapplePosition, delta) + offset);
        }
    }
}

public class SpringSimulation
{
    private float strength;
    private float damper;
    private float target;
    private float velocity;
    
    private float value;
    
    public void Update(float deltaTime)
    {
        var direction = target - value >= 0 ? 1f : -1f;
        var force = Mathf.Abs(target - value) * strength;
        velocity += (force * direction - velocity * damper) * deltaTime;
        value += velocity * deltaTime;
    }
    public void Reset()
    {
        // reset values
        velocity = 0f;
        value = 0f;
    }
    #region Setters
    
    public void SetValue(float value)
    {
        this.value = value;
    }
    
    public void SetTarget(float target)
    {
        this.target = target;
    }
    
    public void SetDamper(float damper)
    {
        this.damper = damper;
    }
    
    public void SetStrength(float strength)
    {
        this.strength = strength;
    }
    
    public void SetVelocity(float velocity)
    {
        this.velocity = velocity;
    }
    
    public float Value => value;

    #endregion
}