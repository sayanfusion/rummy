using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public static CardController Instance;
    [SerializeField] CardView cardPrefab;
    [SerializeField] CardGroupView cardGroupPrefab;
    [SerializeField] RectTransform cardHolder;
    [SerializeField] List<CardGroupView> allCardGroups;
    [SerializeField] List<CardView> selectedCards;
    [SerializeField] Button grpBtn;
    string[] allCards = new string[]
    {
    "H_A", "H_2", "H_3", "H_4", "H_5", "H_6", "H_7", "H_8", "H_9", "H_10", "H_J", "H_Q", "H_K",
    "D_A", "D_2", "D_3", "D_4", "D_5", "D_6", "D_7", "D_8", "D_9", "D_10", "D_J", "D_Q", "D_K",
    "C_A", "C_2", "C_3", "C_4", "C_5", "C_6", "C_7", "C_8", "C_9", "C_10", "C_J", "C_Q", "C_K",
    "S_A", "S_2", "S_3", "S_4", "S_5", "S_6", "S_7", "S_8", "S_9", "S_10", "S_J", "S_Q", "S_K"
    };
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        RandomSpawnCard();
        grpBtn.onClick.AddListener(() => MakeGroup());
    }

    void RandomSpawnCard()
    {
        List<string> allCardsTemp = new List<string>(allCards);
        CardGroupView cardGroup = Instantiate(cardGroupPrefab, cardHolder);
        cardGroup.transform.localPosition = Vector3.zero;
        cardGroup.name = "0";
        cardGroup.cardOverlapArea = 0.5f;
        allCardGroups.Insert(0, cardGroup);
        for (int i = 0; i < 10; i++)
        {
            int rndomIndex = Random.Range(0, allCardsTemp.Count);
            string cardName = allCardsTemp[rndomIndex];
            allCardsTemp.RemoveAt(rndomIndex);
            string rank = cardName.Split('_')[1];
            string suit = cardName.Split('_')[0];
            ResourceManager.Instance.spriteDict.TryGetValue(cardName, out Sprite sprite);
            if (sprite != null)
            {
                CardView card = Instantiate(cardPrefab);
                card.Initiate(rank, suit, sprite);
                cardGroup.Add(card);
            }
            else
            {

                Debug.Log("sprite = null");
                Debug.Log(cardName);
                Sprite sp = ResourceManager.Instance.spriteDict[cardName];
                Debug.Log(sp.name);

            }

        }

    }

    public void OnSelect(CardView card)
    {
        selectedCards.Add(card);
    }

    void MakeGroup()
    {
        if (selectedCards.Count == 1) return;
        CardGroupView cardGroup = Instantiate(cardGroupPrefab, cardHolder);
        cardGroup.transform.localPosition = Vector3.zero;
        cardGroup.name = $"{allCardGroups.Count}";
        allCardGroups.Insert(0, cardGroup);
        foreach (var item in selectedCards)
        {
            item.group.Remove(item);
            cardGroup.Add(item);
            item.selected = false;

        }
        selectedCards.Clear();
        ReArrangeCardHolder();
    }

    void ReArrangeCardHolder()
    {
        float width = 0;
        for (int i = allCardGroups.Count - 1; i >= 0; i--)
        {
            if (allCardGroups[i]._cards.Count > 0)
            {
                width += allCardGroups[i].groupWidth;
            }
            else
            {
                Destroy(allCardGroups[i].gameObject);
                allCardGroups.RemoveAt(i);
            }
        }
        cardHolder.sizeDelta = new Vector2(width, cardHolder.sizeDelta.y);

        float leftEnd = (-width / 2);

        foreach (var item in allCardGroups)
        {
            leftEnd += item.groupWidth / 2;
            item.transform.localPosition = new Vector2(leftEnd, 0);
            leftEnd += item.groupWidth / 2 + (20);

        }
    }
}
