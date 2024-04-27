using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    private Animator originalAnimator;
    [SerializeField] private RuntimeAnimatorController Base;

    [System.Serializable] 
    public class Skins {
        public AnimatorOverrideController animOverride;
        public string skinName;
    }

    [SerializeField] private List<Skins> skins = new List<Skins>();

    void Start()
    {
        originalAnimator = GetComponent<Animator>();  
    }

    public void ChangeSkin(string _skinName) {
        if(_skinName == "Base") {
            originalAnimator.runtimeAnimatorController = Base;
        }
        else {
            AnimatorOverrideController _over = skins.Find(x => x.skinName == _skinName).animOverride;
            originalAnimator.runtimeAnimatorController = _over;
        }
        
    }
}
