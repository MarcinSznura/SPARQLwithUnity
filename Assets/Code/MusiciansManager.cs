using UnityEngine;
using SPARQLNET;
using SPARQLNET.Enums;
using SPARQLNET.Objects;
using SPARQLNET.Misc;
using TMPro;
using RestSharp.Extensions;

namespace SPARQLNETClient
{
    public class MusiciansManager : MonoBehaviour
    {
        [Header("Anwser GameObject")]
        [SerializeField] GameObject anwserPrefab = null;
        [SerializeField] Transform parent = null;

        [Header("Musicians database")]
        [SerializeField] TextMeshProUGUI queryForMusicians = null;

        [Header("Filters")]
        [SerializeField] string creator = "";
        [SerializeField] string title = "";

        public void GetMusician()
        {
            int _childToRemove = parent.childCount;
            if (_childToRemove > 0)
            {
                for (int i = 0; i< _childToRemove; i++)
                {
                    Destroy(parent.GetChild(_childToRemove - (i+1)).gameObject);
                }
            }

            QueryClient _queryClient = new QueryClient("http://sparql.europeana.eu/");
            Table _table = _queryClient.Query(queryForMusicians.text);

            Debug.Log(_table.GetOutput(OutputFormat.Table));

            for (int i = 0; i < _table.Rows.Count; i++)
            {
                if(_table.Rows[i].Data[0].Contains(title) && _table.Rows[i].Data[1].Contains(creator))
                {
                    Debug.Log("HIT!");
                    string _check = "";
                    GameObject _go = Instantiate(anwserPrefab, parent);

                    if (_table.Rows[i].Data[0] != null)
                    {
                        if (_table.Rows[i].Data[0].Length > 40)
                        {
                            _check = _table.Rows[i].Data[0].Substring(0, 40) + '\n';
                        }
                        else
                        {
                            _check = _table.Rows[i].Data[0] + '\n';
                        }

                        _go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _check;
                    }
                    else
                    {
                        Debug.LogWarning("row 0 is null");
                    }

                    if (_table.Rows[i].Data[1] != null)
                    {
                        if (_table.Rows[i].Data[1].Length > 50)
                        {
                            _check = _table.Rows[i].Data[1].Substring(0, 50) + '\n';
                        }
                        else
                        {
                            _check = _table.Rows[i].Data[1] + '\n';
                        }

                        _go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _check;
                    }
                    else
                    {
                        Debug.LogWarning("row 1 is null");
                    }

                    if (_table.Rows[i].Data[2] != null)
                    {
                        if (_table.Rows[i].Data[2].Length > 80)
                        {
                            _check = _table.Rows[i].Data[2].Substring(0, 80) + '\n';
                        }
                        else
                        {
                            _check = _table.Rows[i].Data[2] + '\n';
                        }

                        _go.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = _check;
                    }
                    else
                    {
                        Debug.LogWarning("row 2 is null");
                    }

                    if (_table.Rows[i].Data[3] != null)
                    {
                        if (_table.Rows[i].Data[3].Length > 10)
                        {
                            _check = _table.Rows[i].Data[3].Substring(0, 10) + '\n';
                        }
                        else
                        {
                            _check = _table.Rows[i].Data[3] + '\n';
                        }

                        _go.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = _check;
                    }
                    else
                    {
                        Debug.LogWarning("row 3 is null");
                    }
                }
            }
        }

        public void OpenLinkOnButton(TextMeshProUGUI _url)
        {
            string _urlString = _url.text.Replace("\"","");
            Application.OpenURL(_urlString);
            Debug.Log("I am opening: " + _urlString);
        }

        public void SetTitle(string _data)
        {
            title = _data;
        }

        public void SetCreator(string _data)
        {
            creator = _data;
        }
    }
}
