using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_DrawerCredits : MonoBehaviour
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

        SoundManager.Instance.PlayDrawerSound(transform.position, 0.3f);
    }
}
