using UnityEngine;
using UnityEngine.EventSystems; // Add this namespace for IPointerClickHandler
using UnityEngine.UI;

// Tile class that represents a single tile in the game
// A tile can be a bomb, flagged, revealed, or have a number of adjacent bombs
// A tile can be clicked on to reveal it, or right-clicked to flag it
// A tile can be revealed to show if it is a bomb or not
// A tile can be flagged to show that the player thinks it is a bomb
// A tile can have a number of adjacent bombs to show how many bombs are around it
public class Tile : MonoBehaviour  // Implement IPointerClickHandler for mouse input
{
    public bool isBomb = false;
    public bool isRevealed = false;
    public bool isFlagged = false;

    public int adjacentBombs = 0;
    //public Image image;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Tile clicked");
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // Handle left click (reveal tile)
            RevealTile();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            // Handle right click (flag tile)
            FlagTile();
        }
    }

    private void RevealTile()
    {
        // Logic to reveal the tile
        isRevealed = true;
        Debug.Log("Tile revealed");
    }

    private void FlagTile()
    {
        // Logic to flag the tile
        isFlagged = !isFlagged;
        Debug.Log("Tile flagged");
    }
}
