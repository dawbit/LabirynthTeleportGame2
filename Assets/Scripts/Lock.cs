using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lock : MonoBehaviour
{
    public Door[] doors;
    public KeyColor myColor;
    bool iCanOpen = false;
    bool locked = false;
    Animator key;

    // Start is called before the first frame update
    void Start()
    {
        key = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (iCanOpen && !locked)                                            //<-------
        {                                                                   //<-------
            GameManager.gameManager.SetUseInfo("Press E to open lock");     //<-------
        }                                                                   //<-------
        if (Input.GetKeyDown(KeyCode.E) && !locked && iCanOpen) 
        {
            key.SetBool("useKey", CheckTheKey());
        }
    }

    public void UseKey()
    {
        foreach(Door door in doors) 
        {
            door.OpenClose();
        }
    }

    public bool CheckTheKey() 
    {
        if (GameManager.gameManager.redKey > 0 && myColor == KeyColor.Red) 
        {
            GameManager.gameManager.redKey--;
            GameManager.gameManager.redKeyText.text = GameManager.gameManager.redKey.ToString();  //<-------
            locked = true;
            return true;
        }
        else if (GameManager.gameManager.greenKey > 0 && myColor == KeyColor.Green) 
        {
            GameManager.gameManager.greenKey--;
            GameManager.gameManager.greenKeyText.text = GameManager.gameManager.greenKey.ToString();  //<-------
            locked = true;
            return true;
        }
        else if (GameManager.gameManager.goldKey > 0 && myColor == KeyColor.Gold) 
        {
            GameManager.gameManager.goldKey--;
            GameManager.gameManager.goldKeyText.text = GameManager.gameManager.goldKey.ToString();  //<-------
            locked = true;
            return true;
        }
        else 
        {
            Debug.Log("Nie masz klucza");
            return false;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player") 
        {
            iCanOpen = true;
            Debug.Log("You Can Use Lock");
            GameManager.gameManager.SetUseInfo(""); //<-------
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.tag == "Player") 
        {
            iCanOpen = false;
            Debug.Log("You Can not Use Lock");
        }
    }
}
