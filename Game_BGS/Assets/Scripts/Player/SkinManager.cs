using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    private Animator originalAnimator;

    [System.Serializable] 
    public class Skins {
        public AnimatorOverrideController animOverride;
        public string skinName;
    }

    [SerializeField] private List<Skins> skins = new List<Skins>();


    // Start is called before the first frame update
    void Start()
    {
        originalAnimator = GetComponent<Animator>();     
    }

    void ChangeSkin(string _skinName) {
        AnimatorOverrideController _over = skins.Find(x => x.skinName == _skinName).animOverride;
        originalAnimator.runtimeAnimatorController = _over;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)) {
            ChangeSkin("Blue");
        }   
    }
}
