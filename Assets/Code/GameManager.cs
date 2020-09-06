using System;
using SPARQLNET;
using SPARQLNET.Enums;
using SPARQLNET.Objects;
using SPARQLNET.Misc;
using UnityEngine;
using TMPro;

namespace SPARQLNETClient
{
    class GameManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI queryResult = null;

        [SerializeField] string birthPlace = "";
        [SerializeField] string birthDate = "";
        [SerializeField] string name = "";
        [SerializeField] string deathDate = "";

        public void GetQuery()
        {
            //Set the endpoint
            QueryClient queryClient = new QueryClient("http://dbpedia.org/sparql");

            //Create a query that finds people who were born in Berlin before 1900
            Table table = queryClient.Query("PREFIX : <http://dbpedia.org/resource/>" +
                                                "PREFIX dbo: <http://dbpedia.org/ontology/>" +
                                                "SELECT ?name ?birth ?death ?person WHERE {" +
                                                "     ?person dbo:birthPlace :"+birthPlace+" ." +
                                                "     ?person dbo:birthDate ?birth ." +
                                                "     ?person foaf:name ?name ." +
                                                "     ?person dbo:deathDate ?death ." +
                                                "     FILTER (?birth < \"1900-01-01\"^^xsd:date) ." +
                                                "} ORDER BY ?birth LIMIT 10");

            Debug.Log(table.GetOutput(OutputFormat.Table));

            queryResult.text = table.GetOutput(OutputFormat.Table);

        }
    }
}
