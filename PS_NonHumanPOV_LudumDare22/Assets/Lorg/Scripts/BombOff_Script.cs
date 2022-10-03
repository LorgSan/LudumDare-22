using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BombOff_Script : GenericSingletonClass<BombOff_Script>
{
    [SerializeField] Transform fly;
    [SerializeField] Transform putler;
    Animator animator;
    [SerializeField] float stepSpeed = 1f;
    [HideInInspector] public static bool bombWentOff = false;
    Quaternion targetRotation;
    [SerializeField] float buttonTime;
    float idleTime;
    float step;
    float textStep;
    [SerializeField] Color fadeColor;
    [SerializeField] Color textColor;
    Color startColor;
    Color startTextColor;
    [SerializeField] Image UIFade;
    bool fading;
    Text endText;
    
    void Start()
    {
        animator = putler.parent.GetComponent<Animator>();
        endText = UIFade.transform.GetChild(0).GetComponent<Text>();
        startColor = UIFade.color;
        startTextColor = endText.color;
        UpdateAnimClipTimes();
    }

    void Update()
    {
        if (bombWentOff == true)
        {
            Vector3 targetDirection = putler.position - fly.position;
            float singleStep = stepSpeed * Time.deltaTime;
            Quaternion targetRotaion = Quaternion.LookRotation(targetDirection); // gets quanterian vector dir
            fly.transform.rotation = Quaternion.Lerp(fly.transform.rotation, targetRotaion, singleStep);
            if ( Quaternion.Angle(fly.transform.rotation, targetRotaion) < 10f)
                {
                    animator.SetBool("BombWentOff", true);
                    StartCoroutine("AnimDelay");
                    //Debug.Log(animator.GetBool("BombWentOff"));
                }
        }

        if (fading == true)
        {
            step += Time.deltaTime;
            Color lerpColor = Color.Lerp(startColor, fadeColor, step);
            UIFade.color = lerpColor;
            if (UIFade.color == fadeColor)
            {
                textStep += Time.deltaTime;
                Color textLerp = Color.Lerp(startTextColor, textColor, textStep);
                endText.color = textLerp;
                if (endText.color == textLerp)
                {
                    if (Input.anyKey)
                    {
                        SceneManager.LoadScene("StartScene");
                    }
                }
            }
        }
    }

    IEnumerator AnimDelay()
    {
        yield return new WaitForSeconds(buttonTime);
        fading = true;
    }

    public void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips)
        {
            switch(clip.name)
            {
                case "ButtonPush":
                    buttonTime = clip.length;
                    break;
                case "Idle":
                    idleTime = clip.length;
                    break;
            }
        }
    }
}
