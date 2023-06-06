using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Animator animator;
    public RuntimeAnimatorController runtimeAnimatorController;
    // Start is called before the first frame update
    private string clipName = "play";
    void Start()
    {
        var anim = new AnimatorOverrideController();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(0,0,150f,50f),"Pose01"))
            UpdateAnimator("Animator/Pose01");
        if(GUI.Button(new Rect(60,0,150f,50f),"Pose02"))
            UpdateAnimator("Animator/Pose02");
        if(GUI.Button(new Rect(120,0,150f,50f),"Pose03"))
            UpdateAnimator("Animator/Pose03");
    }

    void UpdateAnimator(string ainmName)
    {
        var overrideController = new AnimatorOverrideController();
        overrideController.runtimeAnimatorController = runtimeAnimatorController;

        var clips = Resources.LoadAll<AnimationClip>(ainmName);
        overrideController[clipName] = clips[0];
        animator.runtimeAnimatorController = null;
        animator.runtimeAnimatorController = overrideController;
        animator.Play(clipName,0,0);
        Resources.UnloadUnusedAssets();
    }
}
