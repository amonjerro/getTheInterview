using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionMaker : MonoBehaviour
{
    public List<Company> companyName;
    public int chanceMin;
    public int chanceMax; //Add score to the skills

    string PickCompanyName()
    {
        string output = companyName[Random.Range(0, companyName.Count - 1)].compName;

        return output;
    }

    public Connections CreateConnection()
    {
        string result = PickCompanyName();
        int chance = Random.Range(chanceMin, chanceMax);

        Connections con = new Connections(result, chance);
        Debug.Log(result);
        Debug.Log(chance);

        return con;
    }

    public void AddConnectionToPool()
    {
        Connections connection = CreateConnection();
        ResourceManager rm = ServiceLocator.Instance.GetService<ResourceManager>();
        Grader grader = ServiceLocator.Instance.GetService<Grader>();

        UIGeneralManager uigm = ServiceLocator.Instance.GetService<UIGeneralManager>();
        uigm.UpdatePopUp("You have made a connection with a person from " + connection.companyName);
        uigm.ShowPopUp();
        rm.connectionList.Add(connection);
        grader.AddConnectionFeedback();
    }
}
