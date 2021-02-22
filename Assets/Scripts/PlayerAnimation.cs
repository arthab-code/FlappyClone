using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator flyAnimation;

    private void Start()
    {
        flyAnimation = GetComponent<Animator>();
    }

    public void PlayFlyAnimation()
    {
        flyAnimation.Play(0);
    }

}
