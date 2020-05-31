using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PartyManager : MonoBehaviour
{
    public List<GameObject> partyList = new List<GameObject> { };

    void Start()
    {
        AddNewUnit("Undead King", 3);
    }
   
    public GameObject ChoosePartyMember()
    {
        foreach (var partyMember in partyList)
        {
            if (partyMember.activeInHierarchy == false)
            {
                //if hidden and available, set it to active and return it to the player who calls this method.
                partyMember.SetActive(true);
                return partyMember;
            }
            
        }
            return null;
    }

    public void AddNewUnit(string name, int level = 1)
    {
        GameObject unitToAdd = GameObject.Find(name);
        GameObject newUnit = Instantiate(unitToAdd, this.transform.position, unitToAdd.transform.rotation);
        newUnit.transform.parent = this.transform;

        Unit newUnitStats = newUnit.GetComponent<Unit>();
        newUnitStats.currentLevel = level;

        Debug.Log("Adding Unit");
        newUnit.SetActive(false);
        partyList.Add(newUnit);
    }
}
