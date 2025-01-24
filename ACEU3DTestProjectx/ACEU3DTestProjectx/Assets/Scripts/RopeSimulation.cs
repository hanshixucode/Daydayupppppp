using UnityEngine;
using UnityEngine.Serialization;

public class RopeSimulation : MonoBehaviour
{
    public PlayerHook playerHook;
    
    // 绳子被分割成的段数，数值越大绳子越平滑
    public int quality = 200;

    // 阻尼值，减缓模拟速度，使绳子各部分受影响程度不同
    public float damper = 14;

    // 弹簧模拟的强度，数值越大越努力趋近目标点
    public float strength = 800;

    // 动画的速度
    public float velocity = 15;

    // 模拟的波浪数量
    public float waveCount = 3;

    // 波浪的高度
    public float waveHeight = 1;

    // 影响曲线，用于控制绳子各段受动画影响的程度
    public AnimationCurve affectCurve;

    // 自定义的弹簧模拟脚本，用于返回动画所需的值
    private SpringSimulation spring;

    // 线渲染器组件，用于绘制绳子
    private LineRenderer lr;

    // 当前抓钩的位置
    private Vector3 currentGrapplePosition;

    private void Awake()
    {
        // 获取 LineRenderer 组件引用
        lr = GetComponent<LineRenderer>();
        // 创建 Spring_MLab 实例
        spring = new SpringSimulation();
        // 设置弹簧模拟的目标值为 0
        spring.SetTarget(0);
    }

    // 在 Update 方法之后调用
    private void LateUpdate()
    {
        // 调用绘制绳子的方法
        DrawRope();
    }

    void DrawRope()
    {
        // 如果没有进行抓钩操作，不绘制绳子
        if (!playerHook.grappling)
        {
            // 将当前抓钩位置设置为枪尖位置
            currentGrapplePosition = playerHook.gunTip.position;

            // 重置弹簧模拟
            spring.Reset();

            // 如果线渲染器已有位置点，将位置点数量置为 0
            if (lr.positionCount > 0)
                lr.positionCount = 0;

            return;
        }

        // 如果线渲染器位置点数量为 0
        if (lr.positionCount == 0)
        {
            // 设置弹簧模拟的初始速度
            spring.SetVelocity(velocity);

            // 根据绳子质量设置线渲染器的位置点数量
            lr.positionCount = quality + 1;
        }

        // 设置弹簧模拟的阻尼和强度，并更新模拟状态
        spring.SetDamper(damper);
        spring.SetStrength(strength);
        spring.Update(Time.deltaTime);

        // 获取抓钩点和枪尖的位置
        Vector3 grapplePoint = playerHook.activePoint.position;
        Vector3 gunTipPosition = playerHook.gunTip.position;

        // 找到相对于绳子的向上方向
        Vector3 up = Quaternion.LookRotation((grapplePoint - gunTipPosition).normalized) * Vector3.up;

        // 将当前抓钩位置逐渐向抓钩点移动
        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        // 遍历绳子的所有段并进行动画处理
        for (int i = 0; i < quality + 1; i++)
        {
            // 计算当前段的比例
            float delta = i / (float)quality;
            // 计算当前绳子段的偏移量
            Vector3 offset = up * waveHeight * Mathf.Sin(delta * waveCount * Mathf.PI) * spring.Value *
                             affectCurve.Evaluate(delta);

            // 设置线渲染器当前位置点，从枪尖位置向当前抓钩位置插值并加上偏移量
            lr.SetPosition(i, Vector3.Lerp(gunTipPosition, currentGrapplePosition, delta) + offset);
        }
    }
}

public class SpringSimulation
{
    // values explained in the GrapplingRope_MLab script
    // 弹簧的弹性强度，数值越大弹簧的弹性力越大
    private float strength;

    // 阻尼系数，用于控制弹簧运动时的能量损耗，使弹簧运动逐渐停止
    private float damper;

    // 弹簧系统试图趋近的目标值
    private float target;

    // 弹簧当前的运动速度
    private float velocity;

    // 弹簧当前的位置值
    private float value;

    // 根据传入的时间间隔更新弹簧系统的状态
    public void Update(float deltaTime)
    {
        // calculate the animation values using some formulas I don't understand :D
        // 计算弹簧从当前位置到目标位置的方向，大于等于 0 为正向（1f），小于 0 为负向（-1f）
        var direction = target - value >= 0 ? 1f : -1f;
        // 计算弹簧的弹性力，大小为当前位置与目标位置的距离乘以弹性强度
        var force = Mathf.Abs(target - value) * strength;
        // 更新弹簧的速度，速度的变化量由弹性力减去阻尼力后乘以时间间隔得到
        velocity += (force * direction - velocity * damper) * deltaTime;
        // 更新弹簧的当前位置，位置的变化量为速度乘以时间间隔
        value += velocity * deltaTime;
    }

    // 重置弹簧系统的状态，将速度和当前位置都设为 0
    public void Reset()
    {
        // reset values
        velocity = 0f;
        value = 0f;
    }

    /// here you'll find all functions used to set the variables of the simulation

    #region Setters

    // 设置弹簧的当前位置值
    public void SetValue(float value)
    {
        this.value = value;
    }

    // 设置弹簧系统要趋近的目标值
    public void SetTarget(float target)
    {
        this.target = target;
    }

    // 设置弹簧运动的阻尼系数
    public void SetDamper(float damper)
    {
        this.damper = damper;
    }

    // 设置弹簧的弹性强度
    public void SetStrength(float strength)
    {
        this.strength = strength;
    }

    // 设置弹簧的初始速度
    public void SetVelocity(float velocity)
    {
        this.velocity = velocity;
    }

    // 只读属性，用于获取弹簧的当前位置值
    public float Value => value;

    #endregion
}