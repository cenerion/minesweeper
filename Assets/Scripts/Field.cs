using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor.Rendering.Universal;
using System;
using Unity.VisualScripting;
using System.Linq;

public class Field : MonoBehaviour
{
	public GameObject tilePrefab;
    private List<Tile> tiles = new List<Tile>();
    private int size;


    void Start(){}
    void Update(){}

    public void setupField(int size){
        this.size = size;
        var minesCount = calculateMineCount(size);

        var gen = new MinesweeperMapGenerator(size, size, minesCount);
        var map = gen.GenerateBoard();

        foreach (var tile in tiles) {
            Destroy(tile.gameObject);
        }
        tiles.Clear();


    	if(gameObject != null){
            var rect = gameObject.GetComponent<RectTransform>().rect;
            var layout = gameObject.GetComponent<GridLayoutGroup>();
    		layout.constraintCount = size;

            var a = Math.Min(rect.width / size, rect.height / size);
            layout.cellSize = new Vector2(a, a);
    	}

    
    	for( int i = 0; i < size*size; ++i) {

    		GameObject tile = Instantiate(tilePrefab, new Vector3(0,0,0), Quaternion.identity, gameObject.transform);
            var data = tile.GetComponent<Tile>();
            data.posX = i % size;
            data.posY = i / size;
            data.idx = i;

            data.adjacentBombs = map[data.posX, data.posY];

            tiles.Add(data);
    	}
    }
    
    int calculateMineCount(double x){
        // 37x - 200y = 325
        return (int) (((37.0*(x*x))-325.0)/200.0);
    }



    public void revealTiles(int x, int y, int idx){
        var tile = tiles[idx];

        if(tile.isBomb){
            //TODO the end
        }

        var list = new Queue<int>();
        list.Enqueue(idx);

        while(list.Count != 0){
            var t = tiles[list.Dequeue()];

            if(t.adjacentBombs == 0){
                if(t.posX > 0 && (!tiles[t.idx-1].isRevealed) && (!list.Contains(t.idx-1))){
                    list.Enqueue(t.idx-1);
                }

                if(t.posX < size-1 && (!tiles[t.idx+1].isRevealed) && (!list.Contains(t.idx+1)))
                    list.Enqueue(t.idx+1);

                if(t.posY > 0 && (!tiles[t.idx-size].isRevealed) && (!list.Contains(t.idx-size))){
                        list.Enqueue(t.idx-size);
                }

                if(t.posY < size-1 && (!tiles[t.idx+size].isRevealed) && (!list.Contains(t.idx+size))){
                    list.Enqueue(t.idx+size);
                }

                if(t.posY > 0 && t.posX > 0 && (!tiles[t.idx-size-1].isRevealed) && (!list.Contains(t.idx-size-1)))
                    list.Enqueue(t.idx-size-1);

                if(t.posY > 0 && t.posX < size-1 && (!tiles[t.idx-size+1].isRevealed) && (!list.Contains(t.idx-size+1)))
                    list.Enqueue(t.idx-size+1);

                if(t.posY < size-1 && t.posX > 0 && (!tiles[t.idx+size-1].isRevealed) && (!list.Contains(t.idx+size-1)))
                    list.Enqueue(t.idx+size-1);

                if(t.posY < size-1 && t.posX < size-1 && (!tiles[t.idx+size+1].isRevealed) && (!list.Contains(t.idx+size+1)))
                    list.Enqueue(t.idx+size+1);
            }

            t.RevealTail();
        }
    }

    public void gameOver(){
        foreach (var tile in tiles)
        {
            //tile.RevealTail()
        }
    }





    //
    //
    //

    


    class MinesweeperMapGenerator{
        private int _rows;
        private int _cols;
        private int _totalMines;
        private int[,] board;
        //private System.Random _random;

        public MinesweeperMapGenerator(int rows, int cols, int totalMines)
        {
            _rows = rows;
            _cols = cols;
            _totalMines = totalMines;
            board = new int[rows, cols];
        // _random = new Random();
        }

        public int[,] GenerateBoard()
        {
            PlaceMines();
            CalculateAdjacentMines();
            return board;
        }

        private void PlaceMines()
        {
            List<Tuple<int, int>> availableCells = new List<Tuple<int, int>>();
            
            // Step 1: Fill availableCells with all possible positions
            for (int r = 0; r < _rows; r++)
            {
                for (int c = 0; c < _cols; c++)
                {
                    availableCells.Add(new Tuple<int, int>(r, c));
                }
            }

            // Step 2: Randomly place mines while ensuring constraints
            for (int i = 0; i < _totalMines; i++)
            {
                int index = UnityEngine.Random.Range(0, availableCells.Count);
                var selectedCell = availableCells[index];
                int row = selectedCell.Item1;
                int col = selectedCell.Item2;

                // Place the mine
                board[row, col] = -1; // -1 represents a mine

                // Remove the selected cell from available cells
                availableCells.RemoveAt(index);
            }

            // Step 3: Validate mine distribution
            ValidateMineDistribution();
        }

        private void ValidateMineDistribution()
        {
            // Make sure no small region (3x3) has too many mines (for example, max 4 mines in 3x3)
            bool valid = false;

            while (!valid)
            {
                valid = true;

                for (int r = 0; r < _rows - 2; r++)
                {
                    for (int c = 0; c < _cols - 2; c++)
                    {
                        int mineCount = CountMinesInRegion(r, c, 3, 3);
                        if (mineCount > 4)
                        {
                            // Too many mines in this 3x3 region, so remove and re-distribute
                            board = new int[_rows, _cols]; // Clear the board
                            PlaceMines(); // Recreate the board
                            valid = false;
                            break;
                        }
                    }

                    if (!valid) break;
                }
            }
        }

        private int CountMinesInRegion(int row, int col, int height, int width)
        {
            int count = 0;
            for (int r = row; r < row + height; r++)
            {
                for (int c = col; c < col + width; c++)
                {
                    if (r < _rows && c < _cols && board[r, c] == -1)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private void CalculateAdjacentMines()
        {
            // Step 2: Calculate adjacent mines for each cell
            for (int r = 0; r < _rows; r++)
            {
                for (int c = 0; c < _cols; c++)
                {
                    if (board[r, c] == -1) continue; // Skip mines

                    // Count mines in neighboring cells
                    int mineCount = 0;
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            int nr = r + i;
                            int nc = c + j;

                            if (nr >= 0 && nr < _rows && nc >= 0 && nc < _cols)
                            {
                                if (board[nr, nc] == -1) mineCount++;
                            }
                        }
                    }

                    board[r, c] = mineCount;
                }
            }
        }
    }


}
