using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    private const int maxSize = 15;
    private const int minSize = 5;


	public GameObject fieldMap;
    public GameEndScreen gameEndScreen;
    public TextMeshProUGUI textMapSize;
    public TextMeshProUGUI textFlagsLeftCount;
    public TextMeshProUGUI textTotalMinesCount;

	public int size = 5;


    void Start()
    {
        Screen.SetResolution(640, 480, false);
        updateSizeText();
        gameEndScreen.hideEndGame();
    }

    void Update(){}

    public void startGame(){
        fieldMap.GetComponent<Field>().setupField(size);
        gameEndScreen.hideEndGame();
    }

    public void endGame(bool win){
        gameEndScreen.showEndGame(win);
    }


    public void setMinesCountText(int mines) {
        textTotalMinesCount.text = "Ilość min: " + mines.ToString();
    }

    public void setFlagsCountText(int flags) {
        textFlagsLeftCount.text = "Pozostało flag: " + flags.ToString();
    }

    private void updateSizeText() {
        if(textMapSize != null){
            textMapSize.text = "Wielkość: " + size.ToString();
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
