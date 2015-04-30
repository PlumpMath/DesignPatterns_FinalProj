using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chest : MonoBehaviour 
{
    Animation animation;
    public enum State
    { 
        open,
        close        
    }
    public State state;
    public GameObject parts;//mesh
    Color _defaultColors; //to restore chest color
    Renderer _renderer;    
    public float maxDistance = 2;    
    static GUI_Manager _gui;//such horrible practice to have every chest reference this... T_T
    GameObject _player;//reference to player
    public bool inUse = false;
    public List<Item> loot = new List<Item>();
    bool _used = false; //if chest has been populated

	// Use this for initialization
	void Start () 
    {
        animation = GetComponent<Animation>();
        state = State.close;
        _renderer = GetComponentInChildren<Renderer>();
        _defaultColors = _renderer.material.GetColor("_Color");
        _gui = FindObjectOfType<GUI_Manager>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (_player != null && inUse)
        {
            if (Vector3.Distance(transform.position, _player.transform.position) > maxDistance)
            {
                Close();
            }
        }
	}

    public void OnMouseEnter()
    {
        Debug.Log("enter");
        Highlight(true);
    }
    public void OnMouseExit()
    {
        Debug.Log("exit");
        Highlight(false);
    }
    public void OnMouseUp()
    {
        Debug.Log("up");

        GameObject go = GameObject.FindGameObjectWithTag("Player");

        if (go == null) { Debug.Log("GAME OBJECT IS NULL!?!?!?!??"); return; }

        if (Vector3.Distance(transform.position, go.transform.position) > maxDistance && !inUse)
            return;
        
        if (!animation.isPlaying)
        {          
            if (state == State.close)
            {
                //make sure there isn't any current open chests
                if (_gui.currentChest != null)
                    _gui.currentChest.Close();
                Open();
            }
            else //if (state == State.open)
            {
                Close();
            }
        }
       
    }

    public void PopulateChest(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            loot.Add(ItemGenerator.CreateItem());
            //loot[i].Name = "I: " + Random.Range(0, 100);
        }
        _used = true;
    }

    void Open()
    {
        inUse = true;
        _player = GameObject.FindWithTag("Player");
        animation.Play("open");

        if (!_used)
        {
            PopulateChest(5);
        }
        

        state = State.open;
        _gui.currentChest = this;
        _gui.DisplayLoot();
    }
    void Close()
    {
        inUse = false;
        _player = null;
        animation.Play("close");
        state = State.close;
        _gui.ClearWindow();
    }



    void Highlight(bool glow)
    {
        if (glow)
        {
            _renderer.material.SetColor("_Color", Color.yellow);
        }
        else
        {
            _renderer.material.SetColor("_Color", _defaultColors);
        }
    }
}
