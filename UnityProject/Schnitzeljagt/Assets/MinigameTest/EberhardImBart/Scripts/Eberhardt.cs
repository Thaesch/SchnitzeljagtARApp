﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eberhardt : MonoBehaviour {

    public float MaxMoveRange;
    public float MoveSpeed = 1;
    public float EbenenMultiplikator = 1.2f;

    public SpriteRenderer FadeScreen;
    public GameObject EndScreen;


    private float speed;
    private Rigidbody rb;
    private int ebene = 1;

    private bool gameOver;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();

        speed = MoveSpeed * EbenenMultiplikator * ebene;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.x < -MaxMoveRange)
            speed = MoveSpeed * EbenenMultiplikator * ebene;
        else if (transform.position.x > MaxMoveRange)
            speed = -MoveSpeed * EbenenMultiplikator * ebene;

        transform.position += Vector3.right * speed * Time.deltaTime;

        if (!gameOver && ebene == 2)
        {
            gameOver = true;
            StartCoroutine(EndGame());
        }
    }



    public void GoUpEbene()
    {
        ebene++;
        transform.position += Vector3.up * 0.6f;
        transform.position += Vector3.forward;
    }




    private IEnumerator EndGame()
    {
        for (int i = 0; i < 100; i++)
        {
            FadeScreen.color = new Color(0, 0, 0, FadeScreen.color.a + 1 / 150f);
            yield return null;
        }
        var text = Instantiate(EndScreen, Vector3.zero, Quaternion.identity).GetComponentInChildren<Text>();
        text.text = "Hurra du hast gewonnen! \n " + GameObject.Find("PointsText").GetComponent<Text>().text;

        GameObject.Find("PointsText").transform.parent.gameObject.SetActive(false);
    }


}
