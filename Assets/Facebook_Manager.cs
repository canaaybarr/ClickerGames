using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine;

public class Facebook_Manager : MonoBehaviour
{
    public static Facebook_Manager Instance;
    
    
    
    void Awake ()
    {
        if (Instance != null) 
        {
            Destroy(this.gameObject);
            
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            if (FB.IsInitialized) {
                FB.ActivateApp();
            } else {
                //Handle FB.Init
                FB.Init( () => {
                    FB.ActivateApp();
                });
            }
        }
        
    }
    
    public void LogAchievedLevelEvent (string Level_Name,string Level_Type,int Currency) {
        Dictionary<string,object> Params = new Dictionary<string, object>();
        Params[AppEventParameterName.Level] = Level_Name; // LVL NAME
        Params[AppEventParameterName.ContentType] = Level_Type; // LVL_TYPE : TUTORIAL // BOSS // STANDARD
        Params["virtual_currency"] = Currency; //  TOTAL USER CURRENCY 
        FB.LogAppEvent(
            AppEventName.AchievedLevel,
            null
            ,
            Params
        );
    }
    
    public void LogRevivePanel (string Level_Name, string Level_Type, int Currency, int Score, int Time_Spent, string Obstacle) {
        var Params = new Dictionary<string, object>();
        Params[AppEventParameterName.Level] = Level_Name; // LVL NAME
        Params[AppEventParameterName.ContentType] = Level_Type; // LVL_TYPE : TUTORIAL // BOSS // STANDARD
        //Params["virtual_currency"] = Currency; //  TOTAL USER CURRENCY 
        //Params["score"] = Score; //  SCORE
        //Params["time_spent"] = Time_Spent; //  TIME SPENT 
        Params["obstacle"] = Obstacle; //  OBSTACLE 
        FB.LogAppEvent(
            "Level_End",
            null,
            Params
        );
    }
    
    public void LogGameOver(string Level_Name, string Level_Type, int Currency, int Score, int Time_Spent, string Obstacle){
        var Params = new Dictionary<string, object>();
        Params[AppEventParameterName.Level] = Level_Name; // LVL NAME
        Params[AppEventParameterName.ContentType] = Level_Type; // LVL_TYPE : TUTORIAL // BOSS // STANDARD
        Params["virtual_currency"] = Currency; //  TOTAL USER CURRENCY 
        Params["score"] = Score; //  SCORE
        Params["time_spent"] = Time_Spent; //  TIME SPENT 
        Params["obstacle"] = Obstacle; //  OBSTACLE 

        FB.LogAppEvent(
            "Game_Over",
            null,
            Params
        );
    }
}
