using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public int x;
    public int y;
    public Board board;

    public void Setup(int x_, int y_, Board board_)
    {
        this.x = x_;
        this.y = y_;
        this.board = board_;
    }

    private void OnMouseDown()
    {
        Debug.Log("Deteccion");
        board.TileDown(this);
    }

    private void OnMouseEnter()
    {
        Debug.Log("Deteccion");
        board.TileOver(this);
    }

    private void OnMouseUp()
    {
        Debug.Log("Deteccion");
        board.TileUp(this);
    }
}
