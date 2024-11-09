using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SecretButton : MonoBehaviour
{
    [SerializeField] private Transform wallToOpen;

    public void Interact()
    {
        wallToOpen.gameObject.GetComponent<Animator>().SetTrigger("OpenDoor");
        SoundManager.Instance.PlayBookshelftSlideSound(wallToOpen.position, 0.4f);


        GetComponent<Animator>().SetTrigger("Click");
        SoundManager.Instance.PlayButtonSound(transform.position, 0.5f);        
    }
}
