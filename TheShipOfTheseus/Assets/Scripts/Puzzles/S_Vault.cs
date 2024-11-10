using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Vault : MonoBehaviour
{
    public bool isOpen;

    [SerializeField] private string password;
    [SerializeField] private string tempPassword;

    // [SerializeField] private Animator animator;
    [SerializeField] private List<S_SingleVaultLocker> lockersList;
    [SerializeField] private Animator animator;

    private void OpenDrawer()
    {
        Debug.Log("COFRE Aberto!");

        isOpen = true;
        animator.SetTrigger("OpenDoor");

        // SoundManager.Instance.PlayLockerSound(transform.position, 0.4f);
        SoundManager.Instance.PlayOpenDoorSound(transform.position, 0.4f);
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
