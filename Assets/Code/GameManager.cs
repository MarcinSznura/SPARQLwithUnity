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

        [SerializeField] string birthPlace = "";
        [SerializeField] string bornBefore = "";
        [SerializeField] string deathPlace = "";
        [SerializeField] string diedBefore = "";
        // 
        public void GetQuery()
        {
            QueryClient queryClient = new QueryClient("http://dbpedia.org/sparql");

            Table table = queryClient.Query("PREFIX : <http://dbpedia.org/resource/>" +
                                                 "PREFIX dbo: <http://dbpedia.org/ontology/>" +
                                                 "SELECT ?name ?birth ?death ?person WHERE {" +
                                                 "     ?person dbo:birthPlace :"+birthPlace+" ." +
                                                 "     ?person dbo:birthDate ?birth ." +
                                                 "     ?person foaf:name ?name ." +
                                                 "     ?person dbo:deathDate ?death ." +
                                                 "     FILTER (?birth < \"" + bornBefore + "\"^^xsd:date && ?death < \"" + diedBefore + "\"^^xsd:date) ." +
                                                 //
                                                 "} ORDER BY ?birth LIMIT 100");

            Debug.Log(table.GetOutput(OutputFormat.Table));

           queryResult.text = table.GetOutput(OutputFormat.Table);

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
    }
}
