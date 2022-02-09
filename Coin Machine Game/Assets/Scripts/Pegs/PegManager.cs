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

    public List<GameObject> removedPegs = new List<GameObject>();

    public int numPegsToRemove;

    public float regularPegValueModifier = 1;
    public float goldPegValueModifier = 1.5f;
    public float diamondPegValueModifier = 2;

    public Material regularPegMaterial;
    public Material goldPegMaterial;
    public Material diamondPegMaterial;

    // Start is called before the first frame update
    void Start()
    {
        CompilePegsAndRows();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyItemValues()
    {
        ChangePegAttributes();
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
    }

    GameObject SelectRandomPeg(string typeOfPeg)
    {
        GameObject chosenPeg;

        chosenPeg = allPegs[Random.Range(0, allPegs.Count)];

        return chosenPeg;
    }

    public void RemovePegs(int numPegsToRemove)
    {
        GameObject pegToRemove;

        for (int i = 0; i > numPegsToRemove; ++i)
        {
            pegToRemove = SelectRandomPeg("unmodified");

            removedPegs.Add(pegToRemove);

            pegToRemove.SetActive(false);
        }
    }

    public void ReplacePegs()
    {

    }

    void ChangePegAttributes()
    {
        GameObject pegToChange;

        pegToChange = SelectRandomPeg("unmodified");



        //pegToChange.GetComponent<Peg>().ModifyPeg(chosenPegValue, chosenMaterial);
    }
}
