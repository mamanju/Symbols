﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finder : MonoBehaviour
{
    private Renderer m_renderer = null;
    private List<GameObject> m_targets = new List<GameObject>();

    private void Awake()
    {
        m_renderer = GetComponentInChildren<Renderer>();

        var searching = GetComponentInChildren<SearchingBehavior>();
        searching.onFound += OnFound;
        searching.onLost += OnList;
    }

    private void OnFound( GameObject i_foundObject )
    {
        m_targets.Add(i_foundObject);
        Debug.Log("敵発見！");
        //攻撃可能
        this.GetComponent<PlayerCtrl>().AttackFlag = true;
    }

    private void OnList( GameObject i_lostObject)
    {
        m_targets.Remove(i_lostObject);
        if(m_targets.Count == 0)
        {
            Debug.Log("敵がいないぞ");
            this.GetComponent<PlayerCtrl>().AttackFlag = false;
        }
    }
}// class Finder