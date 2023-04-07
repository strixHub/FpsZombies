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
    public static GameObject content;
    public static List<GameObject> clonedObj;
    
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
    private void Start() {
        clonedObj = new List<GameObject>();
    }

    public static void SaveData()
    {

        string path = Application.persistentDataPath+"/saves/SaveData.save";

        FileStream file = null;
        if(!Directory.Exists(Application.persistentDataPath+"/saves")){
            Directory.CreateDirectory(Application.persistentDataPath+"/saves");
            file = File.Create(path);
            file.Close();
        }
        Debug.Log(path);
        
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

        //"ListUnitBase"

        foreach (Transform tr in content.GetComponentsInChildren<Transform>(false))
        {
            if(tr.name.Equals("Content") || tr.Find("ToLearn") == null) continue;
            GameObject g = tr.gameObject;
            if(g.activeSelf){
                string txtToLearn = tr.Find("ToLearn").gameObject.GetComponentInChildren<TMP_InputField>().text;
                string txtSolution =  tr.Find("Solution").gameObject.GetComponentInChildren<TMP_InputField>().text;

                if(string.IsNullOrWhiteSpace(txtToLearn.Trim()) && 
                    !string.IsNullOrWhiteSpace(tr.Find("ToLearn").GetChild(0).GetChild(2).GetComponent<TMP_Text>().text.Trim()) &&
                    !tr.Find("ToLearn").GetChild(0).GetChild(2).GetComponent<TMP_Text>().text.Equals("Enter text...")){
                    txtToLearn = tr.Find("ToLearn").GetChild(0).GetChild(2).GetComponent<TMP_Text>().text;
                }
                if(string.IsNullOrWhiteSpace(txtSolution.Trim()) &&
                    !string.IsNullOrWhiteSpace(tr.Find("Solution").GetChild(0).GetChild(2).GetComponent<TMP_Text>().text.Trim()) &&
                    !tr.Find("Solution").GetChild(0).GetChild(2).GetComponent<TMP_Text>().text.Equals("Enter text...")){
                    txtSolution = tr.Find("Solution").GetChild(0).GetChild(2).GetComponent<TMP_Text>().text;
                }
                if(txtToLearn.Equals("") || txtSolution.Equals("")){
                    Debug.Log("Continue");
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
                bool exists = false;
                if(jobj.TryGetValue(txtToLearn, out token)){
                    //already existing, get the array of solutions
                    exists = true;
                    JToken solutions = token.ToObject<JObject>().GetValue(SOLUTIONS);
                    solArray = solutions.ToObject<JArray>();
                }
                
                solObject.Add(SOLUTION, txtSolution);
                solObject.Add(LVL, lvl);
                solArray.Add(solObject);
                string [] info = new string[]{txtSolution, lvl}; 

                if(GameMng.data.ContainsKey(txtToLearn)){
                        GameMng.data[txtToLearn].Add(info);
                    }else{
                        List<string[]> strList = new List<string[]>();
                        strList.Add(info);
                        GameMng.data.Add(txtToLearn,strList);
                    }

                obj.Add(SOLUTIONS,solArray);
                if(exists){
                    jobj.Remove(txtToLearn);
                }
                jobj.Add(txtToLearn,obj);//
                
            }
        }

        jsonSave = jobj.ToString();
        
        file = File.OpenWrite(path);
        using (StreamWriter sw = new StreamWriter(file))
        {
            // discard the contents of the file by setting the length to 0
            file.SetLength(0); 

            // write the new content
            sw.Write(jsonSave);
        }
    }


    public static void LoadData(GameObject cloneObj, GameObject cnt){

        if(clonedObj != null) {
            int i = clonedObj.Count;
            int j = 0;
            while (j<i){
                
                Destroy(clonedObj[j]);
                j++;
            }
        }
        clonedObj = new List<GameObject>();

        content = cnt;
        string path = Application.persistentDataPath+"/saves/SaveData.save";
        
        FileStream file = null;
        if(Directory.Exists(Application.persistentDataPath+"/saves")){
           
            string jsonAux = File.ReadAllText(path);
            JObject json = null;
            try
            {
                json = JObject.FromObject(JsonConvert.DeserializeObject(jsonAux));
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
                return;
            }

            foreach (JProperty property in json.Children())
            {
                JObject thisObj = property.Value.ToObject<JObject>();
                JArray solutionsArray = thisObj.GetValue(SOLUTIONS).ToObject<JArray>();
                foreach(JToken jt in solutionsArray.Children()){
                    JObject sol = jt.ToObject<JObject>();
                    string solStr = sol.GetValue(SOLUTION).ToString();
                    string lvlStr = sol.GetValue(LVL).ToString();
                    string [] info = new string[]{solStr, lvlStr}; 
                    if(GameMng.data.ContainsKey(property.Name)){
                        GameMng.data[property.Name].Add(info);
                    }else{
                        List<string[]> strList = new List<string[]>();
                        strList.Add(info);
                        GameMng.data.Add(property.Name,strList);
                    }

                    GameObject newObj = Instantiate(cloneObj, content.transform, true);    
                    newObj.transform.Find("ToLearn").GetChild(0).GetChild(1).GetComponent<TMP_Text>().SetText(property.Name);
                    
                    newObj.transform.Find("Solution").GetChild(0).GetChild(1).GetComponent<TMP_Text>().SetText(solStr);                    
                    newObj.SetActive(true);
                    clonedObj.Add(newObj);
                }
            }
        }
    }
}