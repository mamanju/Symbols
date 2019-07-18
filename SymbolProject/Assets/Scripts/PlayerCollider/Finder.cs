using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finder : MonoBehaviour
{
    private Renderer m_renderer = null;
    private List<GameObject> m_enemy = new List<GameObject>();
    public List<GameObject> M_enemy
    {
        get { return m_enemy; }
    }

    private List<GameObject> m_tellain = new List<GameObject>();
    public List<GameObject> M_tellain
    {
        get { return m_tellain; }
    }

    private void Awake()
    {
        m_renderer = GetComponentInChildren<Renderer>();

        var searching = GetComponentInChildren<SearchingBehavior>();
        searching.onFound += OnFound;
        searching.onLost += OnList;
    }

    private void OnFound( GameObject i_foundObject )
    {
        //攻撃可能
        if (LayerMask.LayerToName(i_foundObject.layer) == "Enemy")
        {
            m_enemy.Add(i_foundObject);
            Debug.Log("敵発見！");
        }
        //ギミック
        if (LayerMask.LayerToName(i_foundObject.layer) == "Gimmick")
        {
            m_tellain.Add(i_foundObject);
            Debug.Log("ギミック発見！");
        }
    }

    public void OnList( GameObject i_lostObject)
    {
        if (LayerMask.LayerToName(i_lostObject.layer) == "Enemy")
        {
            m_enemy.Remove(i_lostObject);
        }
        if (LayerMask.LayerToName(i_lostObject.layer) == "Gimmick")
        {
            m_tellain.Remove(i_lostObject);
        }
    }
}// class Finder
