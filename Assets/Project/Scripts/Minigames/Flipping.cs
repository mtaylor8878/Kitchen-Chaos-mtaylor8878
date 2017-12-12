﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flipping : Minigame {

    public GameObject pattyPrefab;
    public GameObject burntPrefab;
    public GameObject friesPrefab;
    private bool beingUsed;
    public List<Transform> cooking;
    public List<GameObject> timers;
    public List<bool> occupied;
    public List<bool> flipped;

    public Transform slot1;
    public GameObject timer1;
    public Transform slot2;
    public GameObject timer2;
    public Transform slot3;
    public GameObject timer3;
    public Transform slot4;
    public GameObject timer4;
    public Transform slot5;
    public GameObject timer5;
    public Transform slot6;
    public GameObject timer6;

    private List<float> time;

    // Use this for initialization
    new void Start () {
        cooking = new List<Transform>();
        timers = new List<GameObject>();
        occupied = new List<bool>();
        flipped = new List<bool>();
        time = new List<float>();

        cooking.Add(slot1);
        timers.Add(timer1);
        occupied.Add(false);
        flipped.Add(false);
        time.Add(15.0f);

        cooking.Add(slot2);
        timers.Add(timer2);
        occupied.Add(false);
        flipped.Add(false);
        time.Add(15.0f);

        cooking.Add(slot3);
        timers.Add(timer3);
        occupied.Add(false);
        flipped.Add(false);
        time.Add(15.0f);

        cooking.Add(slot4);
        timers.Add(timer4);
        occupied.Add(false);
        flipped.Add(false);
        time.Add(15.0f);

        cooking.Add(slot5);
        timers.Add(timer5);
        occupied.Add(false);
        flipped.Add(false);
        time.Add(15.0f);

        cooking.Add(slot6);
        timers.Add(timer6);
        occupied.Add(false);
        flipped.Add(false);
        time.Add(15.0f);

        for (int i = 0; i < 6; i++)
        {
            timers[i].GetComponent<TextMesh>().color = Color.green;
            timers[i].GetComponent<TextMesh>().text = "Open";
        }
        base.Start();
    }

    new void Update()
    {
        base.Update();

        for (int i = 0; i < 6; i++)
        { 
            if (occupied[i])
            {
                time[i] -= Time.deltaTime;
                timers[i].GetComponent<TextMesh>().text = Mathf.Round(time[i]).ToString();

                if (time[i] < 10f)
                {
                    timers[i].GetComponent<TextMesh>().color = Color.red;
                }

                if (time[i] < 0)
                {
                    GameObject burnt;
                    Destroy(cooking[i].GetChild(1).gameObject);
                    burnt = Instantiate(burntPrefab, cooking[i]);
                    burnt.transform.localPosition = Vector3.zero;
                    burnt.transform.parent = GameObject.Find("GameController").transform;
                    burnt.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.None;
                    burnt.GetComponentInChildren<Rigidbody>().detectCollisions = true;
                    burnt.GetComponentInChildren<Rigidbody>().useGravity = true;
                    float angle = Random.Range(0, 360);
                    burnt.GetComponentInChildren<Rigidbody>().AddForce((new Vector3(Mathf.Cos(angle), 1, Mathf.Sin(angle))) * 250f);
                    occupied[i] = false;
                    flipped[i] = false;
                    timers[i].GetComponent<TextMesh>().text = "Burnt";
                    time[i] = 15.0f;
                }
            }
        }
    }

    public override void complete()
    {

    }

    protected override void activeUpdate()
    {
        if (controller.GetButtonDown("LeftHand") || controller.GetButtonDown("RightHand"))
        {
            for (int i = 0; i < 6; i++)
            {
                if (time[i] < 10f && !flipped[i] && cooking[i].GetChild(1).gameObject.name.Contains("UncookedPatty"))
                {
                    flipped[i] = true;
                    time[i] = 15.0f;
                    timers[i].GetComponent<TextMesh>().text = time[i].ToString();
                    break;
                }

                if (time[i] < 10f && flipped[i] && cooking[i].GetChild(1).gameObject.name.Contains("UncookedPatty"))
                {
                    GameObject done;
                    Destroy(cooking[i].GetChild(1).gameObject);
                    done = Instantiate(pattyPrefab, cooking[i]);
                    done.transform.localPosition = Vector3.zero;
                    done.transform.parent = GameObject.Find("GameController").transform;
                    done.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.None;
                    done.GetComponentInChildren<Rigidbody>().detectCollisions = true;
                    done.GetComponentInChildren<Rigidbody>().useGravity = true;
                    float angle = Random.Range(0, 360);
                    done.GetComponentInChildren<Rigidbody>().AddForce((new Vector3(Mathf.Cos(angle), 1, Mathf.Sin(angle))) * 250f);
                    occupied[i] = false;
                    flipped[i] = false;
                    timers[i].GetComponent<TextMesh>().color = Color.green;
                    timers[i].GetComponent<TextMesh>().text = "Open";
                    time[i] = 15.0f;
                    break;
                }

                if (time[i] < 10f && cooking[i].GetChild(1).gameObject.name.Contains("CutPotato"))
                {
                    GameObject done;
                    Destroy(cooking[i].GetChild(1).gameObject);
                    done = Instantiate(friesPrefab, cooking[i]);
                    done.transform.localPosition = Vector3.zero;
                    done.transform.parent = GameObject.Find("GameController").transform;
                    done.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.None;
                    done.GetComponentInChildren<Rigidbody>().detectCollisions = true;
                    done.GetComponentInChildren<Rigidbody>().useGravity = true;
                    float angle = Random.Range(0, 360);
                    done.GetComponentInChildren<Rigidbody>().AddForce((new Vector3(Mathf.Cos(angle), 1, Mathf.Sin(angle))) * 250f);
                    occupied[i] = false;
                    flipped[i] = false;
                    timers[i].GetComponent<TextMesh>().color = Color.green;
                    timers[i].GetComponent<TextMesh>().text = "Open";
                    time[i] = 15.0f;
                }
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Uncooked")
        {
            for (int i = 0; i < 6; i++)
            {
                if (!occupied[i])
                {
                    collision.gameObject.transform.parent = cooking[i];
                    collision.gameObject.transform.localPosition = Vector3.zero;
                    collision.gameObject.transform.rotation = Quaternion.identity;
                    collision.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    collision.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
                    collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    occupied[i] = true;
                    timers[i].GetComponent<TextMesh>().color = Color.green;
                    timers[i].GetComponent<TextMesh>().text = time[i].ToString();
                    break;
                }
            }
        }
    }

    public override void handleItem(Player p, bool leftHand)
    {
        GameObject hand = leftHand ? p.lHand : p.rHand;
        int i;

        if (hand == null)
            return;

        for (i = 0; i < 6; i++)
        {
            if (!occupied[i])
            {
                hand.gameObject.transform.parent = cooking[i];
                hand.gameObject.transform.localPosition = Vector3.zero;
                hand.gameObject.transform.rotation = Quaternion.identity;
                hand.gameObject.GetComponent<Rigidbody>().useGravity = false;
                hand.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
                hand.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                occupied[i] = true;
                timers[i].GetComponent<TextMesh>().color = Color.green;
                timers[i].GetComponent<TextMesh>().text = time[i].ToString();
                break;
            }
        }

        if (i == 6)
            return;

        if (leftHand)
            p.lHand = null;
        else
            p.rHand = null;
        
    }

    public override void interact(GameObject caller, bool leftHand)
    {
        Player p = caller.GetComponent<Player>();

        if (p == null)
            return;

        GameObject hand = leftHand ? p.lHand : p.rHand;

        //If you aren't holding anything
        if (hand == null)
        {
            //check the opposite hand you interacted with for an ingredient
            if (leftHand)
                handleItem(p, false);
            else
                handleItem(p, true);

            enter(caller);
        }
        else //If you are holding something
        {
            //If it's an ingredient take it into the station
            if (hand.tag == "Uncooked")
            {
                handleItem(p, leftHand);
            }
        }
    }
}
