using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    private const int maxSize = 15;
    private const int minSize = 5;


	public GameObject fieldMap;
    public GameObject textMapSize;

	public int size = 5;


    void Start()
    {
        Screen.SetResolution(640, 480, false);
        updateSizeText();
    }

    // Update is called once per frame
    void Update(){

    }

    public void startGame(){
        fieldMap.GetComponent<Field>().setupField(size);
    }


    private void updateSizeText() {
        if(textMapSize != null){
            textMapSize.GetComponent<TextMeshProUGUI>().text = "Wielkość: " + size.ToString();
        }
    }

    public void sizeInc(){
        if(size < maxSize){
            size += 1;
            updateSizeText();
        }
    }

    public void sizeDec(){
        if(size > minSize){
            size -= 1;
            updateSizeText();
        }
    }
}
