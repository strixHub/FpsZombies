using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using TMPro;

public class SaveManager : MonoBehaviour
{
    private static string LVL = "Level";
    private static string SOLUTION = "Solution";
    private static string SOLUTIONS = "Solutions";
    
    /*
        Save files will work as the following example, in plain text, so the users can share their learning decks easily.
        Each WordToLearn can have more than 1 solution. This is because for example, a japanese kanji can have more than 1 reading,
        so you can add more than once the word to the learning deck, with different solutions. If you add the same word and the same 
        solution, the game will not give an error but just save it one time. 
    
        {
            WordToLearn{
                Solutions:[{Solution:solution, Level:0},{Solution:solution2, level:1}]
            }    
        }
    */
    public GameObject content;
    void SaveData()
    {

        string path = Application.persistentDataPath+"/saves/SaveData.save";

        FileStream file;
        if(!Directory.Exists(Application.persistentDataPath+"/saves")){
            Directory.CreateDirectory(Application.persistentDataPath+"/saves");
            file = File.Create(path);
        }else{
            file = File.Open(path,FileMode.Open);
        }
        
        
        string jsonSave = "{}";
        string jsonAux = File.ReadAllText(path);
        JObject jobAux = null;
        try
        {
            jobAux = new JObject(jsonAux);   
        }
        catch (System.Exception)
        {
            //do nothing, job not correct
        }
        
        JObject jobj = new JObject();

        foreach (Transform g in content.GetComponentsInChildren<Transform>())
        {
            if(g.gameObject.activeSelf){

                GameObject toLearn = g.Find("ToLearn").gameObject;
                GameObject solution = g.Find("Solution").gameObject;
                string txtToLearn = toLearn.GetComponent<TextMeshPro>().text;
                string txtSolution = solution.GetComponent<TextMeshPro>().text;
                if(txtToLearn.Equals("")|| txtSolution.Equals("")){
                    continue;
                }
                string lvl = "0";
                if(jobAux != null){
                    //if jobAux exists, lets search the current word to save the lvñ progress
                    JToken tokenData = null;                    
                    if(jobAux.TryGetValue(txtToLearn,out tokenData)){
                        JObject data = tokenData.ToObject<JObject>();
                        JToken tokenSol = null;
                        if(data.TryGetValue(SOLUTIONS, out tokenSol)){
                            JArray solutArray = tokenSol.ToObject<JArray>();
                            foreach(JToken jt in solutArray.Children()){
                                if(jt.ToObject<JObject>().GetValue(SOLUTION).ToString().Equals(txtSolution)){
                                    //save the lvl progress
                                    lvl = jt.ToObject<JObject>().GetValue(LVL).ToString();
                                }
                            }
                        }
                    }
                }
                //find the word in the new jsonObject, if exists, add solution if not duplicated, if it doesnt, add new object
                JToken token = null;
                
                //create as new
                JObject obj = new JObject();
                JArray solArray = new JArray();
                JObject solObject = new JObject();
                if(jobj.TryGetValue(txtToLearn, out token)){
                    //already existing, get the array of solutions
                    JToken solutions = token.ToObject<JObject>().GetValue(SOLUTIONS);
                    solArray = solutions.ToObject<JArray>();
                }
                
                solObject.Add(SOLUTION, txtSolution);
                solObject.Add(LVL, lvl);
                solArray.Add(solObject);
                obj.Add(SOLUTIONS,solArray);
                jobj.Add(txtToLearn,obj);//
            }
        }
        
        StreamReader sr = new StreamReader(file);
        using (StreamWriter sw = new StreamWriter(file))
        {
            // discard the contents of the file by setting the length to 0
            file.SetLength(0); 

            // write the new content
            sw.Write(jsonSave);
        }
    }

    private JObject manageGameObject(Transform g){
        return null;
    }

}
