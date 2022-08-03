using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class Minigame : MonoBehaviour
{
    public List<Button> buttons;
    public List<Button> shuffledButtons;
    public AudioSource winSound;
    public AudioSource loseSound;
    public GameObject door;
    public GameObject explosion;
    public Color32 emptyColor = Color.white;
    public Color32 pressedColor = Color.green;
    public Color32 winColor = Color.cyan;
    public Color32 loseColor = Color.red;
    public float timeToRestart = 2;
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        Restart();
    }

    public void Restart()
    {
        count = 0;
        shuffledButtons = buttons.OrderBy(a => Random.Range(0, 1000)).ToList();

        for (int i = 1; i < 11; i++)
        {
            shuffledButtons[i - 1].GetComponentInChildren<TMP_Text>().text = i.ToString();
            shuffledButtons[i - 1].interactable = true;
            shuffledButtons[i - 1].image.color = emptyColor;
        }

    }

    public void PressButton(Button boton)
    {
        if(int.Parse(boton.GetComponentInChildren<TMP_Text>().text)-1 == count)
        {
            count++;
            boton.interactable = false;
            boton.image.color = pressedColor;

            if (count == 10) StartCoroutine(OpenDoor(true));
        }
        else
        {
            StartCoroutine(OpenDoor(false));
        }
    }

    public IEnumerator OpenDoor(bool result)
    {
        if(!result)
        {
            foreach (var boton in shuffledButtons)
            {
                boton.image.color = loseColor;
                boton.interactable = false;
            }
            loseSound.Play();
            yield return new WaitForSecondsRealtime(timeToRestart);
            Restart();
        }

        else
        {
            foreach (var boton in shuffledButtons)
            {
                boton.image.color = winColor;
                boton.interactable = false;
            }
            winSound.Play();
            yield return new WaitForSecondsRealtime(timeToRestart);
            Instantiate(explosion, door.transform.position, Quaternion.identity);
            Time.timeScale = 1;
            GameManager.instance.currentSong.volume = 1;
            Destroy(door);
        }

    }
}
