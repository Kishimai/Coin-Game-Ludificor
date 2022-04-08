using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{

    private UI_Manager uiManager;
    public double currentCoin;
    public double shmoney;
    private string coinString;

    public GameObject addedShmoney;

    public Sprite zero, one, two, three, four, five, six, seven, eight, nine;
    public Sprite gZero, gOne, gTwo, gThree, gFour, gFive, gSix, gSeven, gEight, gNine;
    public Sprite shmoneySprite;

    // Sprite images are put in here
    public List<Sprite> moneyCount = new List<Sprite>();
    // Gameobjects which hold sprite images
    public List<GameObject> digitPlaces = new List<GameObject>();

    public List<Sprite> newMoneyCount = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("game_manager").GetComponent<UI_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        currentCoin = System.Math.Floor(uiManager._currentCoin);

        // Prevents loops from running constantly if player isnt getting money
        if (shmoney != currentCoin)
        {

            double x = shmoney - currentCoin;

            newMoneyCount.Clear();

            int newShmoney = (int)x;
            newShmoney = Mathf.Abs(newShmoney);
            string newString = newShmoney.ToString();
            
            int index;
            Vector3 location = Vector3.zero;

            // Old unchanged code at bottom of script
            location = digitPlaces[0].transform.position;
            location.y = location.y - 32;

            GameObject newMoney = Instantiate(addedShmoney, location, Quaternion.identity);

            newMoney.transform.SetParent(gameObject.transform);

            newMoney.GetComponent<AddedShmoney>().SetEndPos(new Vector3(location.x, location.y + 32, location.z));

            foreach (char digit in newString)
            {
                newMoneyCount.Add(CompareNumberToSpriteGreen(digit));
            }

            int maxPlaceG = newMoneyCount.Count;

            if (maxPlaceG > digitPlaces.Count)
            {
                maxPlaceG = digitPlaces.Count - 1;
            }

            int locationG = 0;

            for (int i = 0; i < maxPlaceG; ++i)
            {
                newMoney.GetComponent<AddedShmoney>().digitPlaces[i].SetActive(true);
                newMoney.GetComponent<AddedShmoney>().digitPlaces[i].GetComponent<Image>().sprite = newMoneyCount[i];
                locationG = i;
            }



            // Set to digit position of last active digit
            //newMoney.GetComponent<RectTransform>().pivot = new Vector2();

            // Sets shmoney to currentCoin
            shmoney = currentCoin;

            shmoney = System.Math.Floor(shmoney);

            // Sets coinstring to shmoney
            coinString = shmoney.ToString();

            // Emptys list
            moneyCount.Clear();

            foreach (char number in coinString)
            {
                // check number and compare it in switch to get correct sprite image
                // append each image to list
                moneyCount.Add(CompareNumberToSprite(number));
            }

            // Determines max place
            int maxPlace = moneyCount.Count;
            int lastPosition = 0;

            // Sets maxPlace to limit of digitPlaces
            if (maxPlace > digitPlaces.Count)
            {
                maxPlace = digitPlaces.Count - 1;
            }
            
            // Activates and sets number to digit places
            for (int i = 0; i < maxPlace; ++i)
            {
                // Gets last position of number of coins, to disable unused digits in menu
                lastPosition = i + 1;
                digitPlaces[i].SetActive(true);
                digitPlaces[i].GetComponent<Image>().sprite = moneyCount[i];
            }

            // Deactivates unused digit places
            for (int j = lastPosition; j < digitPlaces.Count; ++j)
            {
                digitPlaces[j].SetActive(false);
            }

            // use list of potential image objects
            // for now have max of 16 objects (number can be 16 digits long)

            // foreach image in image_list, set image to sprite from number_list
            // for every image object after number_list, set inactive

        }
    }

    private Sprite CompareNumberToSprite(char number)
    {
        switch (number)
        {
            case '0':
                return zero;

            case '1':
                return one;

            case '2':
                return two;

            case '3':
                return three;

            case '4':
                return four;

            case '5':
                return five;

            case '6':
                return six;

            case '7':
                return seven;

            case '8':
                return eight;

            case '9':
                return nine;

            default:
                Debug.LogError("Shmoney number isnt 0-9");
                return zero;
        }
    }

    private Sprite CompareNumberToSpriteGreen(char number)
    {
        switch (number)
        {
            case '0':
                return gZero;

            case '1':
                return gOne;

            case '2':
                return gTwo;

            case '3':
                return gThree;

            case '4':
                return gFour;

            case '5':
                return gFive;

            case '6':
                return gSix;

            case '7':
                return gSeven;

            case '8':
                return gEight;

            case '9':
                return gNine;

            default:
                Debug.LogError("New Shmoney number isnt 0-9");
                return gZero;
        }
    }

    private void OLDCODE()
    {
        /*
        GameObject placeholder = null;

        for (int i = 0; i < digitPlaces.Count; ++i)
        {
            if (digitPlaces[i].activeSelf)
            {
                placeholder = digitPlaces[i];
            }

            // Looks for last digit
            if (!digitPlaces[i].activeSelf)
            {
                // Gets location of last digit
                location = placeholder.transform.position;
                // Sets location to digit location minus offset position
                location.y = location.y - 32;
                break;
            }
            // If all digits are filled
            else if (i == digitPlaces.Count - 1)
            {
                location = digitPlaces[i].transform.position;
                location.y = location.y - 32;
            }
        }
        */
    }

}


