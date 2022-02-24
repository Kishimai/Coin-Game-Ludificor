using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotLight : MonoBehaviour
{
    public Material currentMaterial;

    public Material offMaterial;
    public Material idleMaterial;
    public Material flashMaterial;
    public Material frenzyMaterial;
    public Material scrollMaterial;
    public Material idleScroll;

    private bool frenzy = false;
    private bool flash = false;
    private bool scroll = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void IdleAppearance()
    {
        frenzy = false;
        flash = false;

        currentMaterial = idleMaterial;

        GetComponent<Renderer>().material = currentMaterial;
    }

    public void Flash()
    {

        if (flash)
        {
            flash = false;
            currentMaterial = offMaterial;
        }
        else
        {
            flash = true;
            currentMaterial = flashMaterial;
        }

        GetComponent<Renderer>().material = currentMaterial;

    }

    public void Frenzy()
    {

        if (frenzy)
        {
            frenzy = false;
            currentMaterial = offMaterial;
        }
        else
        {
            frenzy = true;
            currentMaterial = frenzyMaterial;
        }

        GetComponent<Renderer>().material = currentMaterial;

    }

    public void Scroll()
    {

        if (scroll)
        {
            scroll = false;
            currentMaterial = idleScroll;
        }
        else
        {
            scroll = true;
            currentMaterial = scrollMaterial;
        }

        GetComponent<Renderer>().material = currentMaterial;

    }

    public void TurnOff()
    {
        GetComponent<Renderer>().material = offMaterial;
    }

    public void IdleScroll()
    {
        GetComponent<Renderer>().material = idleScroll;
    }

}
