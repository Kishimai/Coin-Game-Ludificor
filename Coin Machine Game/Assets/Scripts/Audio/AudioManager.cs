using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject comboSound;
    public GameObject goldSound;
    public GameObject diamondSound;
    public GameObject tremorSound;
    public GameObject bombSound;
    public GameObject coinSound;
    public GameObject popSound;
    public GameObject pegCollide;
    public GameObject capsuleCollide;
    public GameObject button;
    public GameObject shopButton;
    public GameObject openCapsule;
    public GameObject openCloseButton;
    public GameObject denied;
    public GameObject getItem;
    public GameObject upgradeCoin;
    public GameObject bulldozeSound;
    public GameObject styroSound;
    public GameObject gwompSound;

    public GameObject powerSurgeClick;
    public GameObject powerSurgeDrone;
    private GameObject surgeCopy;

    public GameObject coinBlitz;

    public GameObject palladium;

    public void PlayAudioClip(string clipName, int index = 0)
    {
        GameObject audioSource;
        switch (clipName)
        {
            case "combo":
                audioSource = Instantiate(comboSound, playerCamera.transform.position, Quaternion.identity);
                audioSource.GetComponent<ComboPing>().PlayAudio(index);
                break;

            case "gold":
                audioSource = Instantiate(goldSound, playerCamera.transform.position, Quaternion.identity);
                audioSource.GetComponent<GoldPing>().PlayAudio();
                break;

            case "diamond":
                audioSource = Instantiate(diamondSound, playerCamera.transform.position, Quaternion.identity);
                audioSource.GetComponent<DiamondPing>().PlayAudio();
                break;

            case "coin":
                audioSource = Instantiate(coinSound, playerCamera.transform.position, Quaternion.identity);
                audioSource.GetComponent<CoinSound>().PlayAudio();
                break;

            case "pop":
                audioSource = Instantiate(popSound, playerCamera.transform.position, Quaternion.identity);
                audioSource.GetComponent<PopSound>().PlayAudio();
                break;

            case "peg":
                audioSource = Instantiate(pegCollide, playerCamera.transform.position, Quaternion.identity);
                audioSource.GetComponent<PegCollide>().PlayAudio();
                break;

            case "item_collide":
                audioSource = Instantiate(capsuleCollide, playerCamera.transform.position, Quaternion.identity);
                audioSource.GetComponent<CapsuleCollide>().PlayAudio();
                break;

            case "button":
                audioSource = Instantiate(button, playerCamera.transform.position, Quaternion.identity);
                audioSource.GetComponent<ButtonInteract>().PlayAudio();
                break;

            case "shop":
                audioSource = Instantiate(shopButton, playerCamera.transform.position, Quaternion.identity);
                audioSource.GetComponent<ShopInteract>().PlayAudio();
                break;

            case "open_capsule":
                audioSource = Instantiate(openCapsule, playerCamera.transform.position, Quaternion.identity);
                audioSource.GetComponent<CapsuleOpen>().PlayAudio();
                break;

            case "denied":
                audioSource = Instantiate(denied, playerCamera.transform.position, Quaternion.identity);
                audioSource.GetComponent<Denied>().PlayAudio();
                break;

            case "get_item":
                audioSource = Instantiate(getItem, playerCamera.transform.position, Quaternion.identity);
                audioSource.GetComponent<GetItemSound>().PlayAudio();
                break;

            case "upgrade_coin":
                audioSource = Instantiate(upgradeCoin, playerCamera.transform.position, Quaternion.identity);
                audioSource.GetComponent<UpgradeCoinSound>().PlayAudio();
                break;

            case "surge_click":
                audioSource = Instantiate(powerSurgeClick, playerCamera.transform.position, Quaternion.identity);
                audioSource.GetComponent<PowerSurgeClick>().PlayAudio();
                break;

            case "surge_drone":
                audioSource = Instantiate(powerSurgeDrone, playerCamera.transform.position, Quaternion.identity);
                surgeCopy = audioSource;
                surgeCopy.GetComponent<PowerSurgeDrone>().PlayAudio();
                break;

            case "tremor":
                break;

            case "bomb":
                break;

            case "bulldoze":
                audioSource = Instantiate(bulldozeSound, playerCamera.transform.position, Quaternion.identity);
                audioSource.GetComponent<BulldozeSound>().PlayAudio();
                break;

            case "blitz":
                audioSource = Instantiate(coinBlitz, playerCamera.transform.position, Quaternion.identity);
                audioSource.GetComponent<CoinBlitzAudio>().PlayAudio();
                break;

            case "palladium":
                audioSource = Instantiate(palladium, playerCamera.transform.position, Quaternion.identity);
                audioSource.GetComponent<PalladiumSound>().PlayAudio();
                break;

            case "styro":
                audioSource = Instantiate(styroSound, playerCamera.transform.position, Quaternion.identity);
                audioSource.GetComponent<StyroSound>().PlayAudio();
                break;

            case "gwomp":
                audioSource = Instantiate(gwompSound, playerCamera.transform.position, Quaternion.identity);
                audioSource.GetComponent<GwompSound>().PlayAudio();
                break;
        }
    }

    public void StopAudioClip(string clipName)
    {
        switch (clipName)
        {
            case "surge_drone":
                surgeCopy.GetComponent<PowerSurgeDrone>().DestroySelf();
                break;
        }
    }

    public void ButtonSound()
    {
        GameObject audioSource;

        audioSource = Instantiate(button, playerCamera.transform.position, Quaternion.identity);
        audioSource.GetComponent<ButtonInteract>().PlayAudio();
    }
    
    public void ShopSound()
    {
        GameObject audioSource;

        audioSource = Instantiate(upgradeCoin, playerCamera.transform.position, Quaternion.identity);
        audioSource.GetComponent<UpgradeCoinSound>().PlayAudio();
    }

    public void OpenSound()
    {
        GameObject audioSource;

        audioSource = Instantiate(openCapsule, playerCamera.transform.position, Quaternion.identity);
        audioSource.GetComponent<CapsuleOpen>().PlayAudio();
    }

    public void ShopOpenClose()
    {
        GameObject audioSource;

        audioSource = Instantiate(openCloseButton, playerCamera.transform.position, Quaternion.identity);
        audioSource.GetComponent<ShopOpenClose>().PlayAudio();
    }

    public void GetItem()
    {
        GameObject audioSource;

        audioSource = Instantiate(getItem, playerCamera.transform.position, Quaternion.identity);
        audioSource.GetComponent<GetItemSound>().PlayAudio();
    }
}
