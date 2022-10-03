using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject_Script : MonoBehaviour
{

    [Header("ENTER INFO HERE")]
    [SerializeField] string actionText;
    [SerializeField] string answerText;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite answerSprite;

    [Header("General Settings")]
    [SerializeField] GameObject UIObject;
    [SerializeField] KeyCode InteractionKey;
    Text InteractText;
    Text KeyText;
    Image UIElement;
    bool collidingWithMe;

    void Start()
    {
        UIElement = UIObject.transform.GetChild(0).GetComponent<Image>();
        InteractText = UIObject.transform.GetChild(1).GetComponent<Text>();
        KeyText = UIObject.transform.GetChild(2).GetComponent<Text>();
    }
    void Update()
    {
        InputCheck();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.transform.tag == "Fly")
        {
            collidingWithMe = true;
            InteractText.text = actionText;
            UIElement.sprite = defaultSprite;
            KeyText.text = InteractionKey.ToString();
            KeyText.gameObject.SetActive(true);
            UIObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider col)
    {
        collidingWithMe = false;
        UIObject.SetActive(false);
        InteractText.text = actionText;
        KeyText.gameObject.SetActive(false);
        UIElement.sprite = defaultSprite;
    }

    void InputCheck()
    {
        if(Input.GetKey(InteractionKey) && UIObject.activeSelf == true && collidingWithMe == true)
        {
            InteractText.text = answerText;
            UIElement.sprite = answerSprite;
            KeyText.gameObject.SetActive(false);
        }
    }
}
