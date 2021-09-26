﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ClickMovement : MonoBehaviour
{
    public Tilemap fogOfWar;
    public Tilemap world;
    public GameObject tile;


    public Vector3Int location;
    Vector3 novaPostionPlayer;
    Vector3 mousePosition;

    [SerializeField]
    int lances = 50;
    int lancesMax = 50;

    void Start()
    {

    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        location = world.WorldToCell(mousePosition);
        //novaPostionPlayer = world.CellToWorld(location)

        tile.transform.position = world.CellToWorld(location);

        if (Input.GetMouseButtonDown(0))
        {
            MovementClickMethod();
        }


    }

    public int vision = 1;
    void UpdateFogOfWar()
    {
        Vector3Int currentPlayerTile = fogOfWar.WorldToCell(transform.position);

        for(int x =-vision; x<= vision; x++)
        {
            for(int y=-vision; y<= vision; y++)
            {
                fogOfWar.SetTile(currentPlayerTile + new Vector3Int(x, y, 0), null);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D objeto)
    {
        if(objeto.gameObject.layer > gameObject.layer)
        {
            print("Player: "+ gameObject.layer+", Objeto:" + objeto.gameObject.layer );
            lances -= 2;
            gameObject.layer = objeto.gameObject.layer;
        }

        else if(objeto.gameObject.layer <= gameObject.layer)
        {
            gameObject.layer = objeto.gameObject.layer;
        }

        else if(objeto.gameObject.tag == "Pedra")
        {
            lances -= 2;
        }
    }

    void MovementClickMethod()
    {

        if (world.GetTile(location))
        {
            print(world.CellToWorld(location));
            novaPostionPlayer = Vector3.Lerp(transform.position, world.CellToWorld(location), 24);
            transform.position = novaPostionPlayer;
        }
        else print("não tem");
    }
    
    void OnMouseEnter()
    {
        if (world.GetTile(location)) { 
            tile.SetActive(true);
        }
    }

    void OnMouseExit()
    {
        if (!world.GetTile(location))
        {
            tile.SetActive(false);
        }
        
    }
}
