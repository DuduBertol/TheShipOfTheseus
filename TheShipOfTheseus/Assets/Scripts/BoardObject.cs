using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardObject: MonoBehaviour
{
    [SerializeField] private BoardObjectSO boardObjectSO;
    [SerializeField] private bool isMainObject;
    [SerializeField] private bool isOnBoard;
    private bool canDropOnBoard;

    private void OnTriggerEnter(Collider collider) 
    {
        if(collider.gameObject.CompareTag("boardDrop"))
        {
            // Debug.Log("Cubo bateu no quadro.E pode ser colocado.");
            canDropOnBoard = true;
            if(isMainObject)
            {
                SoundManager.Instance.PlayPaperSound(transform.position, 1);
            }
            else
            {
                SoundManager.Instance.PlayPinSound(transform.position, 1);
            }
        }
        if(collider.gameObject.CompareTag("Ground"))
        {
            SoundManager.Instance.PlayPaperSound(transform.position, 1);
        }
    }
    
    private void OnTriggerExit(Collider collider) 
    {
        if(collider.gameObject.CompareTag("boardDrop"))
        {
            // Debug.Log("Cubo Vermelho saiu do quadro. E não pode ser colocado.");
            canDropOnBoard = false;

            if(isMainObject)
            {
                SoundManager.Instance.PlayPaperSound(transform.position, 1);
            }
            else
            {
                SoundManager.Instance.PlayPinSound(transform.position, 1);
            }
        }
    }

    public BoardObjectSO GetBoardObjectSO()
    {
        return boardObjectSO;
    }

    public bool GetCanDropOnBoard()
    {
        return canDropOnBoard;
    }
    public bool GetIsMainObject()
    {
        return isMainObject;
    }
    public bool GetIsOnBoard()
    {
        return isOnBoard;
    }
    public void SetIsOnBoard(bool value)
    {
        isOnBoard = value;
    }

    /* private void SetBoardAsParent(GameObject gameObject)
    {
        gameObject.transform.position = boardObjectSO.parentOnBoard.transform.position;
    }  */

}
