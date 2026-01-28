using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] TextMeshProUGUI header;

    [SerializeField] string[] wordList;
    [SerializeField] AudioClip[] comms;

    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();        
    }

    private void Start()
    {
        PlayComms();
    }

    public void PrintMessage(string message)
    {
        messageText.text = message;
        header.text = wordList[Random.Range(0, wordList.Length)];
    }

    void PlayComms()
    {
        if (source == null || comms == null || comms.Length == 0)
            return;

        source.clip = comms[Random.Range(0, comms.Length)];
        source.Play();
    }
}
