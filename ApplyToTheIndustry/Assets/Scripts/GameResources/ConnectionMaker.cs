using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionMaker : MonoBehaviour
{
    public List<Company> companyName;
    //public int chanceMin;
    //public int chanceMax; //Add score to the skills

    string PickCompanyName()
    {
        string output = companyName[Random.Range(0, companyName.Count - 1)].compName;

        return output;
    }

    public Connections CreateConnection()
    {
        string result = PickCompanyName();
        //int chance = Random.Range(chanceMin, chanceMax);

        Connections con = new Connections(result);
        Debug.Log(result);

        return con;
    }

    public void AddConnectionToPool()
    {
        ResourceManager rm = ServiceLocator.Instance.GetService<ResourceManager>();

        rm.connectionList.Add(CreateConnection());
    }
}
