using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private int mSpeed = 5;
    private int mGravity = 5;
    private int mState;
    int n = 0;
    private object STATE = {
        UP: n++,
        DOWN: n++,
        LEFT: n++,
    };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (this.mState)
        {
            case this.STATE.UP:
                this._Move("jump");
            break;

            case this.STATE.LEFT:
                this._Move("left");
            break;

            case this.STATE.RIGHT:
                this._Move("right");
            break;

            case this.STATE.NORMAL:
            break;

        }
        //Touch Handler
        this.TouchHandler();
        //
    }

    void TouchHandler()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            this._SetState(this.STATE.RIGHT);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            this._Move(this.STATE.RIGHT);
        if (Input.GetKeyDown(KeyCode.UpArrow))
            this._Move(this.STATE.UP);
    }

    //private function
    void _Move(string d)
    {
        Vector3 pos = this.transform.position;
        switch (d)
        {
            case "left":
                this.transform.position = new Vector3(pos.x - this.mSpeed*Time.deltaTime, pos.y, pos.z);
            break;

            case "jump":
                this.transform.position = new Vector3(pos.x, pos.y - this.mSpeed*Time.deltaTime, pos.z);
            break;

            case "right":
                this.transform.position = new Vector3(pos.x + this.mSpeed*Time.deltaTime, pos.y, pos.z);
            break;

            default:
            break;
        }
    }

    void _SetState(int s)
    {
        this.mState = s;
    }
}
