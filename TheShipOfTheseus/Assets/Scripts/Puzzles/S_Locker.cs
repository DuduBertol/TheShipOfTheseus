using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Locker : MonoBehaviour
{
    public bool isOpen;

    [SerializeField] private string password;
    [SerializeField] private string tempPassword;

    // [SerializeField] private Animator animator;
    [SerializeField] private List<S_SingleLocker> lockersList;
    [SerializeField] private Animator animator;

    private void OpenDrawer()
    {
        Debug.Log("Gaveta Aberta!");

        isOpen = true;
        animator.SetTrigger("OpenDrawer");

        SoundManager.Instance.PlayLockerSound(transform.position, 0.3f);
        SoundManager.Instance.PlayDrawerSound(transform.position, 0.3f);

    }

    public void CheckPassword()
    {   
        GetPassword();

        if(tempPassword == password)
        {
            Debug.Log("Senha Correta!");

            OpenDrawer();
        }
    }

    private void GetPassword()
    {
        tempPassword = "";

        for (int i = 0; i < lockersList.Count; i++)
        {
            tempPassword += lockersList[i].activeValue.ToString();
        }
    }
}
