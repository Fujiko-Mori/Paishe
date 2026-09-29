using UnityEngine;
using UnityEngine.InputSystem;
//PTDR, TKI ?
using static UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor;

public class Fishing_Rod : MonoBehaviour
{
    //Maybe grosse confusion
    //Le TYPE "Hamecon" est le script de l'hameçon
    //L'object "Hamecon" est le gameobject où on va mettre l'hameçon
    //Attention !

    //Aussi Fishing_Rod.isFish = Hamecon.FISH
    //Elles représente la meme chose, juste dans 2 scripts différents
    [SerializeField] private GameObject Hamecon;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float chance;
    private GameObject LE_hamecon;
    public InputAction fishingAction;
    private bool isFishing;
    private bool isFish;
    private Hamecon hamecon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fishingAction.Enable();
        hamecon = Hamecon.GetComponent<Hamecon>();
        hamecon.OnFishEvent += fishFound;
        isFish = false;
        isFishing = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Lance l'hameçon dans l'eau
        //Verifie si la touche est pressé et qu'on est pas déjà en paishe
        if(fishingAction.IsPressed() && !isFishing)
        {
            Fishing();
        }
        //Capture le poisson
        //Vérifie si la touche est pressé et qu'on a une touche
        //NE RETIRE PAS L'HAMECON
        else if (fishingAction.IsPressed() && isFish)
        {
            getDatFish();
        }
        //Retire l'hamecon
        //Verifie si on est entrain de pecher et que la touche est appuyé.
        else if (fishingAction.IsPressed() && isFishing) 
        {
            getHameconBack();
        }
    }

    //Instantie l'hameçon
    //Puis lui donne les caract de la canne à pêche
    //(permet de gérer plusieurs niveaux de canne à pêche au cas où)
    //Puis le balance avec un addforce
    void Fishing()
    {
        isFishing = true;
        LE_hamecon = (GameObject)Instantiate(Hamecon, spawnPoint.transform.position, Quaternion.identity);
        LE_hamecon.GetComponent<Hamecon>().setChance(chance);
        LE_hamecon.GetComponent<Rigidbody2D>().AddForce(new Vector2 (67f, 69f));
    }

    //KILL THE HAMECON !!!!!!!
    //On pourrait faire une animation de retrait du hamecon de l'eau
    //Mais eh... ON va voir ça à la fin
    void getHameconBack()
    {
        Destroy(LE_hamecon);
        isFishing = false;
    }
    //Le debug log est inutile, permet de set isFish -> true
    //En vrai, le getFISH dans hamecon fait aussi ça
    // On garde ? on laisse au cas où I guess
    void fishFound() { isFish = true; Debug.Log("FISH TROUVER AVEC L'EVENT !"); }

    //Changer ce truc pour la capture de poiscaille
    void getDatFish()
    {
        Debug.Log("Fais les events lié à la capture d'un pichon");
        isFish = false;
    }
}
