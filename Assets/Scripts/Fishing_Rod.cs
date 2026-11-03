using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
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
    [SerializeField] private Score score;
    private GameObject LE_hamecon;
    public InputAction fishingAction;
    private bool isFishing;
    private bool isFish;
    private Hamecon hamecon;
    //Problème stupide, solution stupide
    [SerializeField] private float cooldown;
    private float untilNextFishing;
    //Barre de force
    private Vector2 forceHamecon;
    [SerializeField] private Slider NRJslider;
    private bool isCharging;
    private float timeCharging;
    //Time des 4 types de prises
    private float timingFish;
    [SerializeField] private float legendaryFish;
    [SerializeField] private float rareFish;
    [SerializeField] private float commonFish;
    [SerializeField] private float escapeFish;

    //Audio
    [SerializeField] private SoundEffects soundEffects;
    //Screen Shake
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private float screenShakeDuration;
    [SerializeField] private float screenShakeAmountCommon;
    [SerializeField] private float screenShakeAmountRare;
    [SerializeField] private float screenShakeAmountLegendary;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fishingAction.Enable();
        isFish = false;
        isFishing = false;
        untilNextFishing = 0;
        forceHamecon = new Vector2(1f, 0.3f);
        timeCharging = 0f;
        //Init des fish
        /*
        legendaryFish = 0.1f;
        rareFish = 0.4f;
        commonFish = 1f;
        escapeFish = 2f
        */
        timingFish = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        //Lance l'hameçon dans l'eau
        //Verifie si la touche est pressé et qu'on est pas déjà en paishe
        //Charge up
        if(fishingAction.IsPressed() && !isFishing && Time.time > untilNextFishing)
        {
            timeCharging += Time.deltaTime;
            NRJslider.value = math.clamp(math.log(1+timeCharging),0, 1);
        }
        //RELEASED !!
        if (fishingAction.WasReleasedThisFrame() && !isFishing && Time.time > untilNextFishing)
        {
            forceHamecon = new Vector2(400f * NRJslider.value, 120f * NRJslider.value);
            Fishing();
            untilNextFishing = Time.time + cooldown;    
            timeCharging = 0f;
        }
        //Capture le poisson PUIS retire l'hamecon
        //Vérifie si la touche est pressé et qu'on a une touche
        else if (fishingAction.WasPressedThisFrame() && isFish)
        {
            getDatFish();
        }
        //Retire l'hamecon
        //Verifie si on est entrain de pecher et que la touche est appuyé.
        else if (fishingAction.WasPressedThisFrame() && isFishing) 
        {
            getHameconBack();
        }
        //Marche, mais moche, on pourra faire un truc plus clean avec fishFound
        /*
        if (Time.time < timingFish + escapeFish && isFishing)
        {
            Debug.Log("The Fish has escaped");
            getHameconBack();
        }
        */
    }

    //Instantie l'hameçon
    //Puis lui donne les caract de la canne à pêche
    //(permet de gérer plusieurs niveaux de canne à pêche au cas où)
    //Puis le balance avec un addforce
    void Fishing()
    {
        isFishing = true;
        LE_hamecon = (GameObject)Instantiate(Hamecon, spawnPoint.transform.position, Quaternion.identity);
        Hamecon scriptHamecon = LE_hamecon.GetComponent<Hamecon>();
        scriptHamecon.setChance(chance);
        scriptHamecon.OnFishEvent += fishFound;
        LE_hamecon.GetComponent<Hamecon>().setChance(chance);
        //Possible de changer le vector en fonction de la force si on veut rajouter un input dessus ????
        LE_hamecon.GetComponent<Rigidbody2D>().AddForce(forceHamecon);
    }

    //KILL THE HAMECON !!!!!!!
    //On pourrait faire une animation de retrait du hamecon de l'eau
    //Mais eh... ON va voir ça à la fin
    void getHameconBack()
    {
        if (LE_hamecon != null)
        {
            Destroy(LE_hamecon);
            isFishing = false;
            StopCoroutine("KillHamecon");
            untilNextFishing = Time.time + cooldown;
            NRJslider.value = 0;
        }
    }

    //La coroutine qui va retirer l'hamecon automatiquement quand la prise commence

    IEnumerator KillHamecon()
    {
        //Le () => est bizarre, merci la doc unity I guess ?
        yield return new WaitUntil(() => Time.time > timingFish + escapeFish);
        if (isFishing) { getHameconBack(); }
    }

    //Permet de set isFish -> true
    //En vrai, le getFISH dans hamecon fait aussi ça
    // On garde ? on laisse au cas où I guess
    //"FISH TROUVER AVEC L'EVENT !
    void fishFound() 
    {
        isFish = true; 
        Debug.Log("Fish trouvé");
        soundEffects.playSound(SoundEff.fishFound);
        timingFish = Time.time;
        StartCoroutine("KillHamecon");
    }


    //Changer ce truc pour la capture de poiscaille
    //Fait les debug, reste plus qu'à lier au score
    void getDatFish()
    {
        //Fais les events lié à la capture d'un pichon
        isFish = false;
        //Astuce pour les cooldowns eheh
        if (Time.time < timingFish + legendaryFish * (2.5f+NRJslider.value)) 
        {
            score.changeScore(CatchType.legendary);
            soundEffects.playSound(SoundEff.legendary);
            cameraShake.screenShake(screenShakeDuration, screenShakeAmountLegendary);
            Debug.Log("Legendary Fish !!"); 
        }
        else if (Time.time < timingFish + rareFish * (1.5f+NRJslider.value))
        {
            score.changeScore(CatchType.rare);
            soundEffects.playSound(SoundEff.rare);
            cameraShake.screenShake(screenShakeDuration, screenShakeAmountRare);
            Debug.Log("Rare fish !");
        }
        else if (Time.time < timingFish + commonFish * (0.7f+NRJslider.value))
        {
            score.changeScore(CatchType.common);
            soundEffects.playSound(SoundEff.common);
            cameraShake.screenShake(screenShakeDuration, screenShakeAmountCommon);
            Debug.Log("common fish...");
        }
        getHameconBack();
    }
}
