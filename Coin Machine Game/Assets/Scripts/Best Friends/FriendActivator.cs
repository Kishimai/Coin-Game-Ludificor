using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendActivator : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject leftFriend;
    public GameObject rightFriend;
    public int numFriends = 0;
    public float coinDropSpeed = 8f;
    public float minimumDropSpeed = 1f;
    public float timeUntilItem = 600f;
    public bool dropItems = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateFriend()
    {
        numFriends += 1;
        if (numFriends <= 2)
        {
            if (numFriends == 1)
            {
                leftFriend.GetComponent<BestFriend>().ActivateFriend();
                gameManager.GetComponent<ItemInventory>().AddFriendItems();
            }
            else
            {
                rightFriend.GetComponent<BestFriend>().ActivateFriend();
            }
        }
    }

    public void FasterFriends(float cooldownDecrease)
    {
        if (coinDropSpeed > minimumDropSpeed)
        {
            coinDropSpeed -= cooldownDecrease;
        }
    }

    public void FriendsWithBenefits()
    {
        dropItems = true;
    }

    public void MoreBenefits(float cooldownDecrease)
    {
        if (timeUntilItem > 5)
        {
            timeUntilItem -= cooldownDecrease;
        }
    }
}
