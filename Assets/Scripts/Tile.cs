using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems; // Add this namespace for IPointerClickHandler
using UnityEngine.UI;

// Tile class that represents a single tile in the game
// A tile can be a bomb, flagged, revealed, or have a number of adjacent bombs
// A tile can be clicked on to reveal it, or right-clicked to flag it
// A tile can be revealed to show if it is a bomb or not
// A tile can be flagged to show that the player thinks it is a bomb
// A tile can have a number of adjacent bombs to show how many bombs are around it
public class Tile : MonoBehaviour, IPointerClickHandler// for mouse input
{
    public bool isRevealed = false;
    public bool isFlagged = false;
    public int adjacentBombs = 0;
    public bool isBomb
    {
        get => adjacentBombs == -1;
    }
    public int posX;
    public int posY;
    public int idx;

    Image image;

    public Sprite tile_unreveald;
    public Sprite tile_bomb;
    public Sprite tile_flagged;
    public Sprite tile_n0;
    public Sprite tile_n1;
    public Sprite tile_n2;
    public Sprite tile_n3;
    public Sprite tile_n4;
    public Sprite tile_n5;
    public Sprite tile_n6;
    public Sprite tile_n7;
    public Sprite tile_n8;

    void Start(){
        image = gameObject.GetComponent<Image>();
        image.sprite = tile_unreveald;
    }
    void Update(){}


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if(!isFlagged){
                var field = gameObject.transform.parent.gameObject.GetComponent<Field>();
                field.revealTiles(posX, posY, idx);
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            FlagTile();
        }
    }

    public void RevealTail(){
        if(isFlagged){
            return;
        }

        isRevealed = true;

        if(isBomb){
            image.sprite = tile_bomb;
            return;
        }
        
        switch (adjacentBombs)
        {
            case 0:
                image.sprite = tile_n0;
                break;
            case 1:
                image.sprite = tile_n1;
                break;
            case 2:
                image.sprite = tile_n2;
                break;
            case 3:
                image.sprite = tile_n3;
                break;
            case 4:
                image.sprite = tile_n4;
                break;
            case 5:
                image.sprite = tile_n5;
                break;
            case 6:
                image.sprite = tile_n6;
                break;
            case 7:
                image.sprite = tile_n7;
                break;
            case 8:
                image.sprite = tile_n8;
                break;
        }
    }

    private void FlagTile()
    {
        if(isRevealed)
            return;
        
        isFlagged = !isFlagged;
        if(isFlagged){
            image.sprite = tile_flagged;
        }
        else{
            image.sprite = tile_unreveald;
        }
    }
}
