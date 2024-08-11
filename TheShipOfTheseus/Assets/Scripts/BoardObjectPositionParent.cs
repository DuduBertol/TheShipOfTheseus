using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardObjectPositionParent : MonoBehaviour
{
    [SerializeField] private BoardObjectSO boardObjectSO;

    public BoardObjectSO GetBoardObjectSO()
    {
        return boardObjectSO;
    }
}
