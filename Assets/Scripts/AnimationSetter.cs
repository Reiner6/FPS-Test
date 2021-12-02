using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationSetter : MonoBehaviour
{
    #region Variables
    public Animator animator;
    readonly float numberOfAnimations = 3;
    #endregion
    #region MonoBehavior
    private void Start()
    {
        SetAnimation((int)(PlayerPrefs.GetFloat("Blend", 0) * (numberOfAnimations - 1)));
    }
    #endregion
    #region Methods
    /// <summary>
    /// Call to set animation
    /// </summary>
    /// <param name="value"></param>
    public void SetAnimation(int value)
    {
        animator.SetFloat("Blend", value / (numberOfAnimations - 1));
    }
    /// <summary>
    /// Set animation value and load next scene
    /// </summary>
    public void NextScene()
    {
        PlayerPrefs.SetFloat("Blend", animator.GetFloat("Blend"));
        SceneManager.LoadScene("GunRange");
    }
    #endregion
}