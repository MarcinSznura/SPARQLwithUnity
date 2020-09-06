using System;
using SPARQLNET;
using SPARQLNET.Enums;
using SPARQLNET.Objects;
using SPARQLNET.Misc;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

namespace SPARQLNETClient
{
    class GameManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI queryResult = null;
        [SerializeField] TextMeshProUGUI nameText = null;
        [SerializeField] TextMeshProUGUI birthText = null;
        [SerializeField] TextMeshProUGUI deathText = null;
        [SerializeField] TextMeshProUGUI personText = null;

        [SerializeField] string birthPlace = "";
        [SerializeField] string bornBefore = "";
        [SerializeField] string deathPlace = "";
        [SerializeField] string diedBefore = "";

        public void GetQuery()
        {
            QueryClient queryClient = new QueryClient("http://dbpedia.org/sparql");

            string _query = "PREFIX : <http://dbpedia.org/resource/>" +
                                                 "PREFIX dbo: <http://dbpedia.org/ontology/>" +
                                                 "SELECT ?name ?birth ?death ?person WHERE {";

            if (birthPlace == "")
            {
                _query += "     ?person dbo:birthPlace ?birth .";
            }
            else
            {
                _query += "     ?person dbo:birthPlace :" + birthPlace + " .";
            }

            if (deathPlace != "")
            {
                _query += "     ?person dbo:deathPlace :" + deathPlace + " .";
            }

            _query += "     ?person dbo:birthDate ?birth ." +
                                                 "     ?person foaf:name ?name ." +
                                                 "     ?person dbo:deathDate ?death .";

            if (bornBefore.Length == 0 && diedBefore.Length == 0)
            {
            }
            else if (bornBefore.Length > 0 && diedBefore.Length > 0)
            {
                _query += "     FILTER (?birth < \"" + bornBefore + "\"^^xsd:date && ?death < \"" + diedBefore + "\"^^xsd:date) . ";
            }
            else if (diedBefore == "")
            {
                _query += "     FILTER (?death < \"" + bornBefore + "\"^^xsd:date) .";
            }
            else
            {
                _query += "     FILTER (?birth < \"" + diedBefore + "\"^^xsd:date) .";
            }

            _query += "} ORDER BY ?birth";

           Debug.Log(_query + "");

            Table table = queryClient.Query(_query);

            Debug.Log(table.GetOutput(OutputFormat.Table));

            //queryResult.text = table.GetOutput(OutputFormat.Table);

            string _name = "";
            string _birth = "";
            string _death = "";
            string _person = "";

            for (int i=0; i < table.Rows.Count;i++)
            {
                string pom = "";

                if (table.Rows[i].Data[0].Length > 30)
                {
                   pom = table.Rows[i].Data[0].Substring(0, 30) + '\n';
                }
                else
                {
                    pom = table.Rows[i].Data[0] + '\n';
                }
                _name += pom;

                if (table.Rows[i].Data[1].Length > 50)
                {
                    pom = table.Rows[i].Data[1].Substring(0, 50) + '\n';
                }
                else
                {
                    pom = table.Rows[i].Data[1] + '\n';
                }
                _birth += pom ;

                if (table.Rows[i].Data[2].Length > 30)
                {
                    pom = table.Rows[i].Data[2].Substring(0, 30) + '\n';
                }
                else
                {
                    pom = table.Rows[i].Data[2] + '\n';
                }
                _death += pom;

                if (table.Rows[i].Data[3].Length > 150)
                {
                    pom = table.Rows[i].Data[3].Substring(0, 150) + '\n';
                }
                else
                {
                    pom = table.Rows[i].Data[3] + '\n';
                }
                _person += pom;
            }

            nameText.text = _name;
            birthText.text = _birth;
            deathText.text = _death;
            personText.text = _person;

            //Debug.Log("columns 1: " + table.Columns[1]);
            // Debug.Log(table.Rows[0].Data[0]);
            //Debug.Log(table.Rows[0].Data[1]);
            //Debug.Log(table.Rows[0].Data[2]);
            //Debug.Log(table.Rows[0].Data[3]);

            //Debug.Log(table.Rows[1].Data[3]);

            /*
            string asd = table.Columns[1];

            for (int i = 0; i<table.Rows.Count;i++)
            {
               var test = Instantiate(row);
            }
            */
        }

        public void SetBirthPlace(string _data)
        {
            birthPlace = _data;
        }

        public void SetDeathPlace(string _data)
        {
            deathPlace = _data;
        }

        public void SetBornBefore(string _data)
        {
            bornBefore = _data;

        }

        public void SetDiedBefore(string _data)
        {
            diedBefore = _data;
        }
    }



}
