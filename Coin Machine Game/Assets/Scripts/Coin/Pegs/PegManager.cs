using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PegManager : MonoBehaviour
{
    // Potentially not useful with current event and item ideas (can be used later if entire rows need to be altered)
    private List<GameObject> pegRows = new List<GameObject>();

    public List<GameObject> allPegs = new List<GameObject>();

    public List<GameObject> unmodifiedPegs = new List<GameObject>();

    public List<GameObject> modifiedPegs = new List<GameObject>();

    public List<GameObject> disabledPegs = new List<GameObject>();

    private GameObject eventManager;

    public bool allowPegEvent = false;
    public float regularPegValueModifier = 0;
    private float goldPegValueModifier = 0.5f;
    private float diamondPegValueModifier = 1f;
    // Not currently used
    private int comboValueModifier = 2;

    public AudioSource pegcombo;

    // Start is called before the first frame update
    void Start()
    {
        eventManager = GameObject.FindGameObjectWithTag("gameplay_event_system");
        CompilePegsAndRows();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CompilePegsAndRows()
    {
        // Finds all child objects of peg collection (in this case its the peg rows)
        foreach (Transform pegRow in transform)
        {
            pegRows.Add(pegRow.transform.gameObject);

            foreach (Transform peg in pegRow)
            {
                allPegs.Add(peg.transform.gameObject);
            }
        }

        foreach (GameObject peg in allPegs)
        {
            unmodifiedPegs.Add(peg);
        }
    }

    public void ChangePegAttributes(string pegTyping, GameObject playerSelectedPeg = null)
    {
        // if pegToChange is null, it knows to choose randomly, else, choose the peg within that variable

        GameObject selectedPeg = null;

        // Runs if no specific peg was passed, will tell SelectUnmodified to make a random choice
        if (playerSelectedPeg == null)
        {
            selectedPeg = SelectUnmodified(pegTyping);
        }
        // Runs if player selected a peg themselves
        else
        {
            // Makes sure to check if player is picking an unmodified peg
            if (unmodifiedPegs.Contains(playerSelectedPeg))
            {
                selectedPeg = SelectUnmodified("", playerSelectedPeg);
            }
            else if (disabledPegs.Contains(playerSelectedPeg))
            {
                selectedPeg = SelectUnmodified("", playerSelectedPeg);
            }
            else if (modifiedPegs.Contains(playerSelectedPeg))
            {
                //selectedPeg = SelectUnmodified;
            }
        }

        if (selectedPeg != null)
        {
            DeterminePegOutcome(pegTyping, selectedPeg);
        }
        else
        {
            Debug.Log("No peg was returned to selectedPeg, player tried to select modified peg OR something went wrong");
        }

    }

    private void DeterminePegOutcome(string pegTyping, GameObject selectedPeg)
    {
        if (pegTyping == "gold")
        {
            AddToModified(selectedPeg);
            selectedPeg.GetComponent<Peg>().ConvertToGilded(goldPegValueModifier);
        }
        else if (pegTyping == "diamond")
        {
            AddToModified(selectedPeg);
            selectedPeg.GetComponent<Peg>().ConvertToDiamond(diamondPegValueModifier);
        }
        else if (pegTyping == "combo")
        {
            AddToModified(selectedPeg);
            selectedPeg.GetComponent<Peg>().ConvertToCombo();
        }
        else if (pegTyping == "palladium")
        {
            AddToModified(selectedPeg);
            selectedPeg.GetComponent<Peg>().ConvertToPalladium();
        }
    }

    public void DisablePegs(int numToDisable)
    {
        GameObject pegToDisable;

        for (int i = 0; i < numToDisable; ++i)
        {
            // Calls SelectUnmodified without passing peg typing or object
            // If no peg typing is passed, it assumes it should be disabled

            if (unmodifiedPegs.Count > 0)
            {
                pegToDisable = SelectUnmodifiedToDisable();



                AddToDisabled(pegToDisable);


                // Player should be able to select a disabled peg, so do not set to inactive anymore
                // instead pass isDisabled to the peg's script so it turns off its own functionality but can still collide with the pointer
                // use a trigger collision box instead? coins shouldnt bounce off of disabled pegs.
                //pegToDisable.SetActive(false);
                pegToDisable.GetComponent<Peg>().ConvertToDisabled();
            }
            else
            {
                break;
            }
        }
    }

    private GameObject SelectUnmodified(string decidedType = "", GameObject highlightedPeg = null)
    {
        GameObject chosenPeg = null;

        // Makes sure there are pegs left to modify
        if (unmodifiedPegs.Count > 0 || disabledPegs.Count > 0)
        {
            // If no peg was highlighted or selected, choose randomly
            if (highlightedPeg == null)
            {
                // Checks list of disabled pegs to see if there are any, if not it will choose from unmodifiedPegs list
                if (disabledPegs.Count > 0)
                {
                    chosenPeg = disabledPegs[Random.Range(0, disabledPegs.Count)];
                }
                else if (unmodifiedPegs.Count > 0)
                {
                    chosenPeg = unmodifiedPegs[Random.Range(0, unmodifiedPegs.Count)];
                }
                else
                {
                    string pegType = "";
                    foreach (GameObject peg in modifiedPegs)
                    {
                        pegType = peg.GetComponent<Peg>().GetPegIdentity();
                        if (decidedType.Equals("gold"))
                        {
                            // Do nothing since this is a common tier
                        }
                        else if (decidedType.Equals("diamond"))
                        {
                            if (pegType.Equals("gold")) // Or gemstone
                            {
                                modifiedPegs.Remove(peg);
                                unmodifiedPegs.Add(peg);
                                chosenPeg = peg;
                            }
                        }
                        else if (decidedType.Equals("combo"))
                        {
                            if (pegType.Equals("gold")) // Or gemstone
                            {
                                modifiedPegs.Remove(peg);
                                unmodifiedPegs.Add(peg);
                                chosenPeg = peg;
                            }
                        }
                        else if (decidedType.Equals("palladium"))
                        {
                            if (pegType.Equals("gold")) // Or gemstone
                            {
                                modifiedPegs.Remove(peg);
                                unmodifiedPegs.Add(peg);
                                chosenPeg = peg;
                            }
                        }
                    }
                }
            }
            // If a peg was highlighted or selected, pick that selected peg
            else
            {
                // First checks to see if player selected a disabled peg
                foreach (GameObject peg in disabledPegs)
                {
                    if (peg.name == highlightedPeg.name)
                    {
                        chosenPeg = peg;
                        //break;
                    }
                }

                // If player did not select a disabled peg, it checks the list of unmodified pegs
                if (chosenPeg == null)
                {
                    // Look through the list of unmodified pegs and find the peg whos name matches the one that the player selected
                    foreach (GameObject peg in unmodifiedPegs)
                    {
                        // When that peg is found, pass it to chosenPeg
                        if (peg.name == highlightedPeg.name)
                        {
                            chosenPeg = peg;
                            //break;
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("No Pegs left in unmodifiedPegs");
        }

        return chosenPeg;
    }

    private GameObject SelectUnmodifiedToDisable()
    {
        GameObject chosenPeg;

        chosenPeg = unmodifiedPegs[Random.Range(0, unmodifiedPegs.Count)];

        return chosenPeg;
    }

    private void AddToModified(GameObject pegToMod)
    {
        if (disabledPegs.Contains(pegToMod))
        {
            disabledPegs.Remove(pegToMod);

            modifiedPegs.Add(pegToMod);
        }
        else
        {
            unmodifiedPegs.Remove(pegToMod);

            modifiedPegs.Add(pegToMod);
        }

    }

    private void AddToDisabled(GameObject pegToDis)
    {
        // Finds and removes peg from unmodified list
        if (unmodifiedPegs.Contains(pegToDis))
        {
            unmodifiedPegs.Remove(pegToDis);
            // Adds peg to disabled list
            disabledPegs.Add(pegToDis);
        }
    }

    public void RelocatePeg(GameObject movingPeg)
    {

        GameObject randomPeg = allPegs[Random.Range(0, allPegs.Count)];

        Vector3 movingPegPos = movingPeg.transform.position;
        Vector3 locationToMove = randomPeg.transform.position;

        movingPeg.transform.position = locationToMove;
        randomPeg.transform.position = movingPegPos;

        randomPeg.GetComponent<Peg>().ResetHitCounter();

    }

    public IEnumerator ComboEvent(float eventDuration)
    {
        float timeUntilEnd = eventDuration;

        allowPegEvent = true;

        foreach (GameObject peg in disabledPegs)
        {
            //peg.SetActive(true);
        }

        foreach (GameObject peg in allPegs)
        {
            peg.GetComponent<Peg>().ConvertToComboEventPeg();
        }

        while (timeUntilEnd > 0)
        {
            if (allowPegEvent)
            {
                timeUntilEnd -= Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            else
            {
                yield return null;
            }
        }

        foreach (GameObject peg in allPegs)
        {
            peg.GetComponent<Peg>().RevertToRecordedAttributes();
        }
        // Iterate again, this time running a method which returns peg's state to its recorded value

        foreach (GameObject peg in disabledPegs)
        {
            //peg.SetActive(false);
            //peg.GetComponent<Peg>.comboe
        }
    }

    public void PauseComboEvent()
    {
        allowPegEvent = false;

        // iterate over all pegs, reverting them to their recorded value

        foreach (GameObject peg in allPegs)
        {
            peg.GetComponent<Peg>().RevertToRecordedAttributes();
        }
    }

    public void ResumeComboEvent()
    {

        // iterate over all pegs, recording their now current value, then turning them to event value

        foreach (GameObject peg in allPegs)
        {
            peg.GetComponent<Peg>().ConvertToComboEventPeg();
        }

        allowPegEvent = true;
    }

}
