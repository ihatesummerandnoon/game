using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRow : MonoBehaviour
{
    // TODO: public GameObject body;
    public GameObject image;
    public int numImage;
    Boolean initPlayer = false;
    public GameObject player;
    public Unit[] units;

    private Board board;
    private int numReady = 0;
    public void Start()
    {
    }

    public void initialize()
    {
        units = new Unit[numImage];
        StartCoroutine(delayedCreateImages());
    }

    private IEnumerator delayedCreateImages()
    {
        //ToDo: yield return new WaitForSeconds(1.0f);
        yield return null;  // wait one frame
        //System.Random random = new System.Random();
        for (int i = 0; i < numImage; i++)
        {
            // TODO: GameObject img = Instantiate(image, new Vector3(0, 0, -1), Quaternion.identity, body.transform);

            GameObject unit = Instantiate(image, new Vector3(0, 0, -1), Quaternion.identity, this.transform);
            Unit unitObject = unit.GetComponent<Unit>();
            unitObject.setParent(this);
            unitObject.initialize();
            unitObject.setFrame(board);
            if (i == numImage / 2 && initPlayer)
            {
                unitObject.createPlayer();
            }
            units[i] = (unitObject);
            
            //img.GetComponent<Unit>().initialize(random);

        }
        // yield break;  // if this is the last statement, you do not have to put yield break here.
    }

    public void initializePlayer()
    {
        initPlayer = true;
    }

    public void buildGrid(Unit[,] grid, int index)
    {
        //Debug.Log(rows + " " + cols);
        for(int i  = 0; i < numImage; i++)
        {
            grid[index, i] = units[i];
        }
    }

    public int numUnits()
    {
        return numImage;
    }

    public void setParent(Board frame)
    {
        this.board = frame;
    }

    public void ReportNumReady(Unit temp)
    {
        board.CheckNumReady(temp);
    }
    public void DestroyMove()
    {
        for(int i = 0; i < units.Length; i++)
        {
            units[i].DestroyMove();
        }
    }
}