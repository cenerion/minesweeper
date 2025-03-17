using UnityEngine;
using TMPro;

public class GameEndScreen : MonoBehaviour
{
    void Start(){}
    void Update(){}

    public TextMeshProUGUI textEndGame;

    public void showEndGame(bool win){
        gameObject.SetActive(true);
        if(win){
            textEndGame.text = "Wygrałeś!";
            textEndGame.color = Color.green;
        } else {
            textEndGame.text = "Przegrałeś!";
            textEndGame.color = Color.red;
        }
    }

    public void hideEndGame(){
        gameObject.SetActive(false);
    }
}
