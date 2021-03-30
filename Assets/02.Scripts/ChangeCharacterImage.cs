using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCharacterImage : MonoBehaviour
{
    public Sprite[] charArray;
    private Image image;
    private int randomChar = 0;

    void Start()
    {
        image = GetComponent<Image>();
    }

    public void ChangeImage()
    {
        // 버튼 소리
        SoundManager.Instance.ClickButton();

        int _randomChar = Random.Range(0, 7);
        
        while(_randomChar == randomChar)
        {
            Debug.Log($"ChangeCharacterImage ::: {_randomChar}");
            _randomChar = Random.Range(0, 7);
        }

        randomChar = _randomChar;
        image.sprite = charArray[randomChar];
    }
}
