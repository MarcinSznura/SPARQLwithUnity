using SPARQLNET;
using SPARQLNET.Enums;
using SPARQLNET.Misc;
using SPARQLNET.Objects;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IdeasManager : MonoBehaviour
{
    [Header("Anwser GameObject")]
    [SerializeField] GameObject anwserPrefab = null;
    [SerializeField] Transform parent = null;

    [Header("Filters")]
    [SerializeField] string word = "";

    public void FindAnwsers()
    {
        int _childToRemove = parent.childCount;
        if (_childToRemove > 0)
        {
            for (int i = 0; i < _childToRemove; i++)
            {
                Destroy(parent.GetChild(_childToRemove - (i + 1)).gameObject);
            }
        }

        QueryClient _queryClient = new QueryClient("http://dbpedia.org/sparql");
        Table _table = _queryClient.Query("select distinct ?Concept where {[] a ?Concept} LIMIT 2000");
        Debug.Log(_table.GetOutput(OutputFormat.Table));
        //_resultField.text = _table.GetOutput(OutputFormat.Table);

        for (int i = 0; i < _table.Rows.Count; i++)
        {
            if (_table.Rows[i].Data[0].Contains(word))
            {
                GameObject _go = Instantiate(anwserPrefab, parent);

                _go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (1+i).ToString();
                _go.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = _table.Rows[i].Data[0];
            }
        }
    }

    public void SetWord(string _data)
    {
        word = _data;
    }

    public void OpenLinkOnButton(TextMeshProUGUI _url)
    {
        string _urlString = _url.text.Replace("\"", "");
        Application.OpenURL(_urlString);
        Debug.Log("I am opening: " + _urlString);
    }
}
