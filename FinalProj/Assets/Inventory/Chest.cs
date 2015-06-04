//Written by Sam Arutyunyan for Design Patterns Project Spring 2015
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace inventory
{
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
        GameObject _player;//reference to player
        public bool inUse = false;
        public List<Item> loot = new List<Item>();
        bool _used = false; //if chest has been populated

        // Use this for initialization
        void Start()
        {
            animation = GetComponent<Animation>();
            state = State.close;
            _renderer = GetComponentInChildren<Renderer>();
            _defaultColors = _renderer.material.GetColor("_Color");            
        }

        // Update is called once per frame
        void Update()
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
            Highlight(true);
        }
        public void OnMouseExit()
        {
            Highlight(false);
        }
        public void OnMouseUp()
        {
            GameObject go = GameObject.FindGameObjectWithTag("Player");

            if (go == null) { Debug.Log("GAME OBJECT IS NULL!?!?!?!??"); return; }

            if (Vector3.Distance(transform.position, go.transform.position) > maxDistance && !inUse)
                return;

            if (!animation.isPlaying)
            {
                if (state == State.close)
                {
                    //make sure there isn't any current open chests
                    if (GUI_Manager.instance.currentChest != null)
                        GUI_Manager.instance.currentChest.Close();
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
            GUI_Manager.instance.currentChest = this;
            GUI_Manager.instance.DisplayLoot();
        }
        void Close()
        {
            inUse = false;
            _player = null;
            animation.Play("close");
            state = State.close;
            GUI_Manager.instance.ClearWindow();
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
}