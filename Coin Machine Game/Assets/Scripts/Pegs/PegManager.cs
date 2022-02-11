using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegManager : MonoBehaviour
{
    // Potentially not useful with current event and item ideas (can be used later if entire rows need to be altered)
    private List<GameObject> pegRows = new List<GameObject>();

    public List<GameObject> allPegs = new List<GameObject>();

    public List<GameObject> unmodifiedPegs = new List<GameObject>();

    public List<GameObject> modifiedPegs = new List<GameObject>();

    public int numPegsToRemove;

    public float regularPegValueModifier = 0;
    private int goldPegValueModifier = 3;
    private int diamondPegValueModifier = 4;

    // Start is called before the first frame update
    void Start()
    {
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

    GameObject SelectUnmodifiedPeg()
    {
        GameObject chosenPeg;

        chosenPeg = unmodifiedPegs[Random.Range(0, allPegs.Count)];

        return chosenPeg;
    }

    GameObject SelectNonDisabledAndUnmodifiedPeg()
    {
        GameObject chosenPeg;

        List<GameObject> nonDisabledPegs = new List<GameObject>();

        foreach (GameObject peg in unmodifiedPegs)
        {
            if (peg.activeSelf)
            {
                nonDisabledPegs.Add(peg);
            }
        }

        chosenPeg = nonDisabledPegs[Random.Range(0, nonDisabledPegs.Count)];

        return chosenPeg;
    }

    public void RemovePegs(int numPegsToRemove)
    {
        GameObject pegToRemove;

        for (int i = 0; i < numPegsToRemove; ++i)
        {
            pegToRemove = SelectNonDisabledAndUnmodifiedPeg();

            pegToRemove.SetActive(false);
        }
    }

    public void ChangePegAttributes(string pegTyping)
    {
        // CONVERT DISABLED PEGS TO A GLOBAL OBJECT
        // MIGHT BE WHERE ISSUE IS LOCATED

        // pegMaterialName = "normal", "gold", "diamond"
        GameObject pegToChange;

        List<GameObject> disabledPegs = new List<GameObject>();

        foreach (GameObject peg in allPegs)
        {
            if (!peg.activeSelf)
            {
                disabledPegs.Add(peg);
            }
        }

        // If there are no disabled pegs, pick a peg from the unmodified list
        if (disabledPegs.Count == 0)
        {
            pegToChange = unmodifiedPegs[Random.Range(0, allPegs.Count)];
            
            if (pegTyping.Equals("gold"))
            {
                // Modifies peg with values: valueModifier, pegMaterial, isGolden, isDiamond
                pegToChange.GetComponent<Peg>().ModifyPeg(goldPegValueModifier, "gold", true, false);
            }
            else if (pegTyping.Equals("diamond"))
            {
                // Modifies peg with values: valueModifier, pegMaterial, isGolden, isDiamond
                pegToChange.GetComponent<Peg>().ModifyPeg(diamondPegValueModifier, "diamond", false, true);
            }
            else
            {
                // Probably not needed since there wont be items that harm the gameplay or make it harder for the player
                Debug.Log("Convert peg to normal version");
            }

            //pegToChange.GetComponent<Peg>().ModifyPeg(VALUEMODIFIER, PEGMATERIAL);
        }
        // If there are disabled pegs, pick a peg from the list of disabled ones
        else
        {
            pegToChange = disabledPegs[Random.Range(0, disabledPegs.Count)];
            pegToChange.SetActive(true);

            if (pegTyping.Equals("gold"))
            {
                // Modifies peg with values: valueModifier, pegMaterial, isGolden, isDiamond
                pegToChange.GetComponent<Peg>().ModifyPeg(goldPegValueModifier, "gold", true, false);
            }
            else if (pegTyping.Equals("diamond"))
            {
                // Modifies peg with values: valueModifier, pegMaterial, isGolden, isDiamond
                pegToChange.GetComponent<Peg>().ModifyPeg(diamondPegValueModifier, "diamond", false, true);
            }
            else
            {
                // Probably not needed since there wont be items that harm the gameplay or make it harder for the player
                Debug.Log("Convert peg to normal version");
            }

            // pegToChange.GetComponent<Peg>().ModifyPeg(VALUEMODIFIER, PEGMATERIAL);
        }

        unmodifiedPegs.Remove(pegToChange);

        //pegToChange.GetComponent<Peg>().ModifyPeg(chosenPegValue, chosenMaterial);
    }
}
