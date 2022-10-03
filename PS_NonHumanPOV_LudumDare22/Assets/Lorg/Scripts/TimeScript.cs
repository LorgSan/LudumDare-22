using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScript : MonoBehaviour
{
    [SerializeField] float bombProbability = 0;
    [SerializeField] float stepIncrease = 0.2f;
    [SerializeField] int cycleNumbers;
    [SerializeField] float TimeBetweenCycles = 10f;
    [SerializeField] AudioClip tenSecPass;
    [SerializeField] AudioClip doomSound;
    [SerializeField] AudioClip doomAmbient;
    [SerializeField] AudioSource audioSource;
    
    struct RandomSelection {
    private int minValue;
    private int maxValue;
    public float probability;
    
    public RandomSelection(int minValue, int maxValue, float probability) {
        this.minValue = minValue;
        this.maxValue = maxValue;
        this.probability = probability;
    }
    
    public int GetValue() { return Random.Range(minValue, maxValue + 1); }
    }

    int Randomizer()
    {
        int random = GetRandomValue(
            new RandomSelection(0, 8, 1f - bombProbability),
            new RandomSelection(9, 9, bombProbability)
        );
        return random;
    }
    
    int GetRandomValue(params RandomSelection[] selections) {
        float rand = Random.value;
        float currentProb = 0;
        foreach (var selection in selections) {
            currentProb += selection.probability;
            if (rand <= currentProb)
                return selection.GetValue();
        }
    
        //will happen if the input's probabilities sums to less than 1
        //throw error here if that's appropriate
        return -1;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("BombTimer", 0f, TimeBetweenCycles);
    }

    void BombTimer()
    {
        cycleNumbers ++;
        if (cycleNumbers >= 15)
        {
            bombProbability = 1f;
        }

        int currentRandom = Randomizer();
        Debug.Log(currentRandom);
        if (currentRandom == 9)
        {
            Debug.Log("bomb went off");
            audioSource.PlayOneShot(doomSound);
            audioSource.clip = doomAmbient;
            audioSource.Play();
            BombOff_Script.bombWentOff = true;
            CancelInvoke();
        } else 
            if (cycleNumbers >= 3)
            {
                bombProbability += stepIncrease;
            }
            audioSource.PlayOneShot(tenSecPass);
    }
}
