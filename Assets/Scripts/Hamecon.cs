using UnityEngine;
using System;

public class Hamecon : MonoBehaviour
{
    [SerializeField] private float cooldown;
    private float timeUntilNext;
    private float chance;
    private bool FISH;
    //Le max du range pour la proba d'avoir un FISS !
    private float maxProba;

    //EVENTS

    public event Action OnFishEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxProba = 10; //Set à la main, on va dire 10 pour tout
        //chance = 0; //Sert à rien, garde fou on sait jamais
        timeUntilNext = Time.time + cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < timeUntilNext && FISH)
        {
            Destroy(this);
        }

        if (FISH)
        {
            
            //Yas une prise, on l'envoie à la canne à peche avec l'event Action
        }
        //Appelle en boucle pour voir si y'a un poisson
        else { FISH = Fishing(); }

    }
    //Permet de dire aux autres scripts l'état de FISH
    bool getFISH() { return FISH; }
    public void setChance(float c) 
    {
        chance = c; 
    }

    //Fonction aléatoire
    //Après chaque cooldown, lance un "dé" et regarde si y'a un poisson
    bool Fishing()
    {
        //Astuce pour les cooldowns eheh
        if (Time.time < timeUntilNext) { return FISH; }
        timeUntilNext = Time.time + cooldown;
        //On teste si y'a un poiscalle

        //Je précise pour éviter l'imprecision entre
        //System.Random et UnityEngine.Random
        float randNumber = UnityEngine.Random.Range(0f, maxProba);
        if (randNumber < chance)
        {
            FISH = true;
            //La poiscaille à été trouvé, on le lance dans l'update d'hamecon !
            OnFishEvent?.Invoke();
        }
        else 
        { 
            //Rien d'attrapé !
        }
        return FISH;
    }
}
