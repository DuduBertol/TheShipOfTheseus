using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_DrawerOptions : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform myText;

    public void MyTextActivated(bool value)
    {
        myText.gameObject.SetActive(value);
    }

    public void PlayAnim()
    {
        animator.SetTrigger("OpenDrawer");
    }
}