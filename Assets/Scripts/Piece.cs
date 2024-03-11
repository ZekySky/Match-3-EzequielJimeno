using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Piece : MonoBehaviour
{

    public int x;
    public int y;
    public Board board;

    public enum type
    {
        elephant,
        giraffe,
        hippo,
        monkey,
        panda,
        parrot,
        penguin,
        pig,
        rabbit,
        snake
    };

    public type pieceType;

    private void Start()
    {
        
    }
    public void Setup(int x_, int y_, Board board_)
    {
        this.x = x_;
        this.y = y_;
        this.board = board_;
    }

    public void Move(int desx, int desy)
    {
        transform.DOMove(new Vector3(desx, desy, -5f), 0.25f).
            SetEase(Ease.InOutCubic).onComplete = () =>{
                x = desx;
                y = desy;
        };
    }

    [ContextMenu("Test Move")]
    public void MoveTest()
    {
        Move(0,0);
    }
    private void Update()
    {
        CheckMatchesAndDestroy();
    }
    void CheckMatchesAndDestroy()
    {
        // Comprobación en el eje X
        if (IsPieceInLine(x, y, 1, 0) && IsPieceInLine(x, y, -1, 0))
        {
            // Destruir las piezas
            DestroyPieces(x + 1, y, x - 1, y);
        }

        // Comprobación en el eje Y
        if (IsPieceInLine(x, y, 0, 1) && IsPieceInLine(x, y, 0, -1))
        {
            // Destruir las piezas
            DestroyPieces(x, y + 1, x, y - 1);
        }
    }

    bool IsPieceInLine(int x, int y, int xOffset, int yOffset)
    {
        return board.GetPiece(x + xOffset, y + yOffset) != null &&
               board.GetPiece(x + xOffset, y + yOffset).pieceType == pieceType;
    }

    void DestroyPieces(int x1, int y1, int x2, int y2)
    {
        Destroy(board.GetPiece(x1, y1).gameObject);
        Destroy(board.GetPiece(x2, y2).gameObject);
        Destroy(gameObject);
    }
}
