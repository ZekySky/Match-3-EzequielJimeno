using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject tileObject;

    public float cameraSizeOffset;
    public float cameraVerticalOffset;

    public GameObject[] avalaiblePieces;
    private int[,] pieceTypes;

    Tile[,] Tiles;
    Piece[,] Pieces;

    Tile startTile;
    Tile endTile;

    // Start is called before the first frame update
    void Start()
    {
        Tiles = new Tile[width, height];    
        Pieces = new Piece[width, height];  
        SetupBoard();
        PositionCamera();
        SetupPieces();
    }
    private void Update() {
        FallDown();
        spawnPiece();
    }

    private void SetupPieces()
    {
        GenerateUniquePieceTypes();

        // Coloca las piezas según los tipos generados
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var selectedPiece = avalaiblePieces[pieceTypes[x, y]];
                var o = Instantiate(selectedPiece, new Vector3(x, y, -5), Quaternion.identity);
                o.transform.parent = transform;
                Pieces[x, y] = o.GetComponent<Piece>();
                Pieces[x, y]?.Setup(x, y, this);
            }
        }
    }

    private void PositionCamera()
    {
        float newPosX = (float)width / 2f;
        float newPosY = (float)height / 2f;
        Camera.main.transform.position = new Vector3 (newPosX -0.5f, newPosY- 0.5f + cameraVerticalOffset, -10f);

        float horizontal = width + 1;
        float vertical = (height/2) + 1;

        Camera.main.orthographicSize = horizontal > vertical ? horizontal + cameraSizeOffset : vertical;
    }

    private void SetupBoard()
    {
        for(int x=0; x<width;x++)
        {
            for(int y=0; y<height;y++)
            {
                var o = Instantiate(tileObject, new Vector3(x,y,-5), Quaternion.identity);
                o.transform.parent = transform;
                Tiles[x,y] = o.GetComponent<Tile>();
                Tiles[x,y]?.Setup(x, y, this);  
            }

        }
    }


    public void TileDown(Tile tile_)
    {
        startTile = tile_;

    }

    public void TileOver(Tile tile_)
    {
        endTile = tile_;
    }

    public void TileUp(Tile tile_)
    {
        if(startTile != null && endTile != null && IsCloseTo(startTile, endTile))
        {
            SwapTiles();
        }
    }

    private void SwapTiles()
    {
        var StarPiece = Pieces[startTile.x, startTile.y];
        var EndPiece = Pieces[endTile.x, endTile.y];

        StarPiece.Move(endTile.x, endTile.y);
        EndPiece.Move(startTile.x, startTile.y);

        Pieces[startTile.x, startTile.y] = EndPiece;
        Pieces[endTile.x, endTile.y] = StarPiece;

    }

    public bool IsCloseTo(Tile start, Tile end)
    {
        if(Math.Abs((start.x - end.x)) == 1 && start.y == end.y)
        {
            return true;
        }

        if (Math.Abs((start.y - end.y)) == 1 && start.x == end.x)
        {
            return true;
        }

        return false;
    }
    //Obtencion de la pieza
    public Piece GetPiece(int x, int y)
    {
        if(x < 0 || x >= width || y < 0 || y >= height)
        {
            return null;
        }
        return Pieces[x, y];
    }
    //Caida de las piezas al realizar un match3
    private void FallDown()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(Pieces[x,y] == null)
                {
                    for (int pos = y+1; pos < height; pos++)
                    {
                        if(Pieces[x,pos] != null)
                        {
                            Pieces[x, pos].Move(x, y);
                            Pieces[x, y] = Pieces[x, pos];
                            Pieces[x, pos] = null;
                            break;
                        }
                    }
                }
            }
        }
    }

    private void spawnPiece()
    {
        // si se realiza un match 3 y hay hueco
        for (int i = 0; i < width; i++)
        {
            if (GetPiece(i, height-1) == null)
            {
                var selectedPiece = avalaiblePieces[UnityEngine.Random.Range(0,avalaiblePieces.Length)];
                var o = Instantiate(selectedPiece, new Vector3(i, height-1, -5), Quaternion.identity);
                o.transform.parent = transform;
                Pieces[i, height-1] = o.GetComponent<Piece>();
                Pieces[i, height-1]?.Setup(i, height-1, this);
            }
        }
    }

    //Funcion para que no existan coincidencias iniciales
    private void GenerateUniquePieceTypes()
    {
        pieceTypes = new int[width, height];

        // Genera tipos de piezas únicas para cada celda
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int type;
                do
                {
                    type = UnityEngine.Random.Range(0, avalaiblePieces.Length); // Genera un tipo de pieza aleatorio para la celda
                } while (HasAdjacentMatches(x, y, type)); // Verifica si hay coincidencias cerca
                pieceTypes[x, y] = type; // Asigna el tipo de pieza generado a la celda
            }
        }
    }

    private bool HasAdjacentMatches(int x, int y, int type)
    {
        // Verifica si hay coincidencias horizontal o verticalmente en la celda
        if ((x > 1 && pieceTypes[x - 1, y] == type && pieceTypes[x - 2, y] == type) ||
            (y > 1 && pieceTypes[x, y - 1] == type && pieceTypes[x, y - 2] == type))
        {
            return true;
        }
        return false;
    }
}
