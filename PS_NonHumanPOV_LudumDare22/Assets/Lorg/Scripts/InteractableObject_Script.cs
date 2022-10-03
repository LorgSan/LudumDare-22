using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject_Script : MonoBehaviour
{

    [Header("ENTER INFO HERE")]
    [SerializeField] string default_Text;
    [SerializeField] string interaction_Text;
    [SerializeField] Sprite original_Sprite;
    [SerializeField] Sprite interaction_Sprite;

    [Header("General Settings")]
    [SerializeField] GameObject UIObject;
    [SerializeField] Text InteractText;
    [SerializeField] Text KeyText;
    [SerializeField] Image UIElement;
    [SerializeField] KeyCode InteractionKey;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.transform.tag == "Fly")
        {
            Debug.Log("collision happened");
            UIObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider col)
    {
        UIObject.SetActive(false);
    }
    void Update()
    {
        InputCheck();
    }

    void InputCheck()
    {
        if(Input.GetKey(InteractionKey))
        {
            InteractText.text = interaction_Text;
            UIElement.sprite = interaction_Sprite;
            KeyText.gameObject.SetActive(false);
        }
    }
}
