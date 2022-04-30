using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendActivator : MonoBehaviour
{

    public GameObject leftFriend;
    public GameObject rightFriend;
    public int numFriends = 0;

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
            }
            else
            {
                rightFriend.GetComponent<BestFriend>().ActivateFriend();
            }
        }
    }
}
