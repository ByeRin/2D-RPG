using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WalkTheMap : MonoBehaviour
{
    public Image playerIcon;
    public GameObject currentLocation;
    public GameObject selectedLocation;
    public List<GameObject> path;
    bool walking;

    void Start()
    {
        walking = false;
    }

    void FindNextPath()
    {
        //try the next path
        GameObject find = currentLocation;
        int tries = 0;
        while (find != null && find.GetComponent<MapPath>().nextLocation != null && tries < 10)
        {
            find = find.GetComponent<MapPath>().nextLocation;
            tries++;
            if (find != null && find == selectedLocation)
            {
                AddPathStepsToQueue(true);
                return;
            }
        }

        //try the previous path
        find = currentLocation;
        tries = 0;
        while (find != null && find.GetComponent<MapPath>().previousLocation != null && tries < 10)
        {
            find = find.GetComponent<MapPath>().previousLocation;
            tries++;
            if (find != null && find == selectedLocation)
            {
                AddPathStepsToQueue(false);
                return;
            }
        }

        //problem, we didn't find that location in any path
        Debug.Log("Not Found");
    }

    void AddPathStepsToQueue(bool next)
    {
        if (next)
        {
            while (currentLocation.GetComponent<MapPath>().nextLocation != selectedLocation)
            {
                path.AddRange(currentLocation.GetComponent<MapPath>().nextPath);
                currentLocation = currentLocation.GetComponent<MapPath>().nextLocation;
                path.Add(currentLocation);
            }
            path.AddRange(currentLocation.GetComponent<MapPath>().nextPath);
            path.Add(selectedLocation);
        }
        else
        {
            while (currentLocation.GetComponent<MapPath>().previousLocation != selectedLocation)
            {
                path.AddRange(currentLocation.GetComponent<MapPath>().previousPath);
                currentLocation = currentLocation.GetComponent<MapPath>().previousLocation;
                path.Add(currentLocation);
            }
            path.AddRange(currentLocation.GetComponent<MapPath>().previousPath);
            path.Add(selectedLocation);
        }
    }

    public void WalkToLocation(Button current)
    {
        //wait for walking to be done
        if (!walking)
        {
            selectedLocation = EventSystem.current.currentSelectedGameObject;
            FindNextPath();
        }
    }

    void Update()
    {
        if (selectedLocation != currentLocation)
        {
            //start walking
            if (path.Count > 0)
            {
                walking = true;
                GameObject location = path[0];
                if (playerIcon.transform.position.x != location.transform.position.x &&
                    playerIcon.transform.position.y != location.transform.position.y)
                {
                    playerIcon.transform.position = Vector3.MoveTowards(playerIcon.transform.position,
                        location.transform.position, Time.deltaTime * 20f);
                }
                else
                    path.RemoveAt(0);
            }
            else if (path.Count == 0)
            {
                walking = false;
                if (playerIcon.transform.position.x != selectedLocation.transform.position.x &&
                    playerIcon.transform.position.y != selectedLocation.transform.position.y)
                {
                    playerIcon.transform.position = Vector3.MoveTowards(playerIcon.transform.position,
                        selectedLocation.transform.position, Time.deltaTime * 2f);
                }
                else
                    currentLocation = selectedLocation;
            }
        }
    }
}
