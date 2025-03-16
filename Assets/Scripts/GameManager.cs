using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public GameObject gridPanel;
	public GameObject tilePrefab;
	public int size = 8;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setTiles(size);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void setTiles(int size){
    	if(gridPanel != null){
    		gridPanel.GetComponent<GridLayoutGroup>().constraintCount = size;
    	}
    
    	for( int i = 0; i < size*size; ++i) {
    		GameObject newtile = Instantiate(tilePrefab, new Vector3(0,0,0), Quaternion.identity, gridPanel.transform);
    	}
    }
}
