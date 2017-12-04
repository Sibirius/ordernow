using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendOverlay : MonoBehaviour {

    private Text nameLabel;
    private Transform cameraTransform;
    private RectTransform satisfactionMeter;
    private RawImage requirement;

    private Friend friend;

    private bool alarmPlayed = false;


    // Use this for initialization
    void Start () {
        nameLabel = GetComponentInChildren<Text>();
        cameraTransform = GameObject.FindObjectOfType<Camera>().transform;
        satisfactionMeter = transform.Find("Satisfaction") as RectTransform;
        requirement = transform.Find("Requirement").GetComponent<RawImage>();

    }

    // Update is called once per frame
    void Update () {

        requirement.texture = friend.requirement.texture;

        transform.rotation = Quaternion.LookRotation(transform.position - cameraTransform.position);
        if (friend != null && satisfactionMeter != null)
        {
            satisfactionMeter.sizeDelta = new Vector2(50, friend.satisfaction * 4);

            if (friend.satisfaction < 20)
            {
                if (!alarmPlayed)
                {
                    GetComponent<AudioSource>().Play();
                    alarmPlayed = true;
                }
                satisfactionMeter.GetComponent<Image>().color = new Color(0.7f, 0, 0);
            }
            else if (friend.satisfaction < 60)
            {
                alarmPlayed = false;
                satisfactionMeter.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0);
            }
            else
            {
                alarmPlayed = false;
                satisfactionMeter.GetComponent<Image>().color = new Color(0, 0.7f, 0);

            }

        }
    }

    public void setFriend(Friend friend)
    {
        this.friend = friend;
    }
}
