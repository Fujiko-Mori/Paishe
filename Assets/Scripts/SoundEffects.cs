using UnityEngine;

public enum SoundEff { fishFound, common, rare, legendary };

public class SoundEffects : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip FishFoundClip;
    [SerializeField] AudioClip CommonFishClip;
    [SerializeField] AudioClip RareFishClip;
    [SerializeField] AudioClip LegendaryFishClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound(SoundEff soundEff)
    {
        if (soundEff == SoundEff.common)
        {
            audioSource.PlayOneShot(CommonFishClip,1.0f);
        } else if (soundEff == SoundEff.rare)
        {
            audioSource.PlayOneShot(RareFishClip, 1.0f);
        }
        else if (soundEff == SoundEff.legendary)
        {
            audioSource.PlayOneShot(LegendaryFishClip, 1.0f);
        }
        else if (soundEff == SoundEff.fishFound)
        {
            audioSource.PlayOneShot(FishFoundClip, 1.0f);
        }
    }
}
