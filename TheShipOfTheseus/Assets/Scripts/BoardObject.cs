using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardObject: MonoBehaviour
{
    [SerializeField] private BoardObjectSO boardObjectSO;
    private bool canDropOnBoard;

    private void OnTriggerEnter(Collider collider) 
    {
        if(collider.gameObject.CompareTag("boardDrop"))
        {
            Debug.Log("Cubo bateu no quadro.E pode ser colocado.");
            canDropOnBoard = true;

            /* if(collider.gameObject.GetComponent<BoardObjectPositionParent>().GetBoardObjectSO() == boardObjectSO)
            {
                Debug.Log("Cubo Vermelho bateu no quadro certo. Pode ser solto no quadro.");
                canDropOnBoard = true;
            }
            else
            {
                Debug.Log("Este não é o quadro.");
            } */
        }
    }
    
    private void OnTriggerExit(Collider collider) 
    {
        if(collider.gameObject.CompareTag("boardDrop"))
        {
            Debug.Log("Cubo Vermelho saiu do quadro. E não pode ser colocado.");
            canDropOnBoard = false;

            /* if(collider.gameObject.GetComponent<BoardObjectPositionParent>().GetBoardObjectSO() == boardObjectSO)
            {
                Debug.Log("Cubo Vermelho saiu da área do quadro certo. Não pode ser solto no quadro.");
                canDropOnBoard = false;
            }
            else
            {
                Debug.Log("Este não era o quadro certo.");
            } */
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

    /* private void SetBoardAsParent(GameObject gameObject)
    {
        gameObject.transform.position = boardObjectSO.parentOnBoard.transform.position;
    }  */

}
