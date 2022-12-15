using System.Collections;
using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using UnityEngine;

public abstract class GameManager<T> : Singleton<T> where T : MonoBehaviour
{
    //STATE

    #region STATE ENUM

    public enum STATE
    {
        Play,
        Pause,
        Lose,
        Win
    }

    #endregion

    public STATE state = STATE.Pause;

    public virtual void Start()
    {
        EX_UIManager.Instance.SetLevelNumber();
    }

    public virtual void Update()
    {
        switch (state)
        {
            case STATE.Play:

                #region PLAY STATE

                //TODO:PLAY STATE CODES------------------------------------------------------------------------

                //TODO:PLAY STATE CODES------------------------------------------------------------------------

                #endregion

                break;
            case STATE.Pause:

                #region PAUSE STATE

                //TODO:PAUSE STATE CODES------------------------------------------------------------------------

                //TODO:PAUSE STATE CODES------------------------------------------------------------------------

                #endregion

                break;
            case STATE.Lose:

                #region DEAD STATE

                //TODO:DEAD STATE CODES------------------------------------------------------------------------

                //TODO:DEAD STATE CODES------------------------------------------------------------------------

                #endregion

                break;
        }
    }

    public virtual void Perfect()
    {


    }



    public Vector2 GetMousePosition()
    {
        Vector2 pos;

        pos = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);

        return pos;
    }

    public virtual void Win()
    {
        EX_UIManager.Instance.ConfettiEnable();

        PlayerPrefs.SetInt("level", EX_LevelManager.Instance.currentLevelNumber + 1);

        state = STATE.Win;

        MMVibrationManager.Haptic(HapticTypes.Success);

        EX_UIManager.Instance.ShowLevelCompletePanel(3);
    }

    public virtual void Lose()
    {
        state = STATE.Lose;

        MMVibrationManager.Haptic(HapticTypes.Failure);

        EX_UIManager.Instance.ShowGameOverPanel();
    }

    #region USEFUL METHODS

    public float ConvertAngle(float _angle)
    {
        float angle = _angle;
        angle = (angle > 180) ? angle - 360 : angle;
        return angle;
    }

    public string ReturnMoneyText(float Money, string format)
    {
        if (Money > 999999999999)
        {
            return "$" + (Money / 1000000000000f).ToString(format) + "T";
        }
        else if (Money > 999999999)
        {
            return "$" + (Money / 1000000000f).ToString(format) + "B";
        }
        else if (Money > 999999)
        {
            return "$" + (Money / 1000000f).ToString(format) + "M";
        }
        else if (Money > 999)
        {
            return "$" + (Money / 1000f).ToString(format) + "K";
        }
        else
        {
            return "$" + ((int) Money).ToString();
        }
    }

    public string ReturnMoneyText(float Money)
    {
        if (Money > 999999999999)
        {
            return "$" + (Money / 1000000000000f).ToString("F1") + "T";
        }
        else if (Money > 999999999)
        {
            return "$" + (Money / 1000000000f).ToString("F1") + "B";
        }
        else if (Money > 999999)
        {
            return "$" + (Money / 1000000f).ToString("F1") + "M";
        }
        else if (Money > 999)
        {
            return "$" + (Money / 1000f).ToString("F1") + "K";
        }
        else
        {
            return "$" + ((int) Money).ToString();
        }
    }

    public float ClampAngle(float angle, float min, float max)
    {
        angle = Mathf.Repeat(angle, 360);
        min = Mathf.Repeat(min, 360);
        max = Mathf.Repeat(max, 360);
        bool inverse = false;
        var tmin = min;
        var tangle = angle;
        if (min > 180)
        {
            inverse = !inverse;
            tmin -= 180;
        }

        if (angle > 180)
        {
            inverse = !inverse;
            tangle -= 180;
        }

        var result = !inverse ? tangle > tmin : tangle < tmin;
        if (!result)
            angle = min;
        inverse = false;
        tangle = angle;
        var tmax = max;
        if (angle > 180)
        {
            inverse = !inverse;
            tangle -= 180;
        }

        if (max > 180)
        {
            inverse = !inverse;
            tmax -= 180;
        }

        result = !inverse ? tangle < tmax : tangle > tmax;
        if (!result)
            angle = max;
        return angle;
    }

    #endregion
}
