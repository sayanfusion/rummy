using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class ResourceManager : MonoBehaviour
{

    [Header("List of named sprites")]
    [SerializeField] private Sprite[] _diamondSprites;
    [SerializeField] private Sprite[] _heartSprites;
    [SerializeField] private Sprite[] _clubSprites;
    [SerializeField] private Sprite[] _spadeSprites;
    public Dictionary<string, Sprite> spriteDict;


    public static ResourceManager Instance { get; private set; }



    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        spriteDict = new Dictionary<string, Sprite>();

        SetspritDict(_diamondSprites, "D");
        SetspritDict(_heartSprites, "H");
        SetspritDict(_clubSprites, "C");
        SetspritDict(_spadeSprites, "S");
    }

    void SetspritDict(Sprite[] arr, string suit)
    { 
        for (int i = 0; i < arr.Length; i++)
        {
            if (!spriteDict.ContainsValue(arr[i]))
            {
                string rank = "";
                if (i == 0) rank = "A";
                else if (i == 10) rank = "J";
                else if (i == 11) rank = "Q";
                else if (i == 12) rank = "K";
                else rank = (i+1).ToString();
                spriteDict.Add($"{suit}_{rank}", arr[i]);
            }
            else
            {
                Debug.LogWarning($"Duplicate sprite: {arr[i].name}");
            }
        }

        
    }

}
