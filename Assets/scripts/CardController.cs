using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class CardController : MonoBehaviour
{
    public static CardController Instance;
    [SerializeField] CardView cardPrefab;
    [SerializeField] CardGroupView cardGroupPrefab;
    [SerializeField] RectTransform cardHolder;
    [SerializeField] List<CardGroupView> allCardGroups;
    [SerializeField] List<CardView> selectedCards;
    [SerializeField] Button grpBtn;
    [SerializeField] Button dealBtn;

    [Header("card picking")]
    [SerializeField] GameObject cardPickPopup;
    [SerializeField] Image cardPick;
    [SerializeField] Button acceptbtn;
    [SerializeField] Button returnbtn;
    [SerializeField] Button returnFromHandBtn;
    [SerializeField] string currentPickingCard;
    [SerializeField] Image openDeckImage;
    [SerializeField]
    List<string> allCards = new List<string>
    {
    "H_A", "H_2", "H_3", "H_4", "H_5", "H_6", "H_7", "H_8", "H_9", "H_10", "H_J", "H_Q", "H_K",
    "D_A", "D_2", "D_3", "D_4", "D_5", "D_6", "D_7", "D_8", "D_9", "D_10", "D_J", "D_Q", "D_K",
    "C_A", "C_2", "C_3", "C_4", "C_5", "C_6", "C_7", "C_8", "C_9", "C_10", "C_J", "C_Q", "C_K",
    "S_A", "S_2", "S_3", "S_4", "S_5", "S_6", "S_7", "S_8", "S_9", "S_10", "S_J", "S_Q", "S_K"
    };
    [SerializeField] List<string> closedDeck;
    [SerializeField] List<string> openDeck;
    public bool unbalanceDeck;
    public TMP_Text scoreText;
    public Canvas canvas;
    public float cardWidth = 168;
    internal CardView draggingCard;
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
        dealBtn.onClick.AddListener(() => ChooseFromCloseDeck());
        acceptbtn.onClick.AddListener(() => OnAcceptCard());
        returnbtn.onClick.AddListener(() => OnReturnCard());
        returnFromHandBtn.onClick.AddListener(() => OnReturnFromHand());
    }

    void RandomSpawnCard()
    {
        CardGroupView cardGroup = Instantiate(cardGroupPrefab, cardHolder);
        cardGroup.transform.localPosition = Vector3.zero;
        cardGroup.name = "0";
        cardGroup.cardOverlapArea = 0.5f;
        allCardGroups.Insert(0, cardGroup);
        for (int i = 0; i < 10; i++)
        {
            int rndomIndex = Random.Range(0, allCards.Count);
            string cardName = allCards[rndomIndex];
            allCards.RemoveAt(rndomIndex);
            string rank = cardName.Split('_')[1];
            string suit = cardName.Split('_')[0];
            ResourceManager.Instance.spriteDict.TryGetValue(cardName, out Sprite sprite);
            if (sprite != null)
            {
                CardView card = Instantiate(cardPrefab);
                card.Initiate(rank, suit, sprite);
                card.transform.name= cardName;
                cardGroup.Add(card,i);
                card.parent=cardGroup.transform;
            }
            else
            {

                Debug.Log("sprite = null");
                Debug.Log(cardName);
                Sprite sp = ResourceManager.Instance.spriteDict[cardName];
                Debug.Log(sp.name);

            }

        }
        closedDeck = new List<string>(allCards);
        ShuffleList(closedDeck);
        allCards.Clear();

    }

    public void OnSelect(CardView card)
    {
        if (unbalanceDeck == true)
        {
            List<CardView> cards = new List<CardView>(selectedCards);
            if (cards.Count > 0)
            {
                foreach (var item in cards)
                {
                    item.DeSelect();
                }
            }
            cards.Clear();
            selectedCards.Add(card);
            return;
        }
        selectedCards.Add(card);
        if (selectedCards.Count > 1)
        {
            grpBtn.gameObject.SetActive(true);
        }
        else
        {
            grpBtn.gameObject.SetActive(false);
        }
    }

    public void OnDeselect(CardView card)
    {
        selectedCards.Remove(card);
        if (selectedCards.Count > 1)
        {
            grpBtn.gameObject.SetActive(true);
        }
        else
        {
            grpBtn.gameObject.SetActive(false);
        }
    }

    void ChooseFromCloseDeck()
    {
        cardPickPopup.SetActive(true);
        if (closedDeck.Count == 0)
        {
            Debug.Log("No more cards to deal");
            return;
        }
        int rndomIndex = Random.Range(0, closedDeck.Count);
        string cardName = closedDeck[rndomIndex];
        closedDeck.RemoveAt(rndomIndex);
        currentPickingCard = cardName;

        ResourceManager.Instance.spriteDict.TryGetValue(cardName, out Sprite sprite);
        if (sprite != null)
        {
            cardPick.sprite = sprite;
        }
    }

    void OnAcceptCard()
    {
        if (string.IsNullOrEmpty(currentPickingCard))
        {
            Debug.Log("No card selected");
            return;
        }
        string rank = currentPickingCard.Split('_')[1];
        string suit = currentPickingCard.Split('_')[0];
        CardView card = Instantiate(cardPrefab);
        card.Initiate(rank, suit, cardPick.sprite);
        allCardGroups[^1].Add(card);
        cardPickPopup.SetActive(false);
        ReArrangeCardHolder();
        unbalanceDeck = true;
        currentPickingCard = string.Empty;
        returnFromHandBtn.gameObject.SetActive(true);
    }

    void OnReturnFromHand()
    {
        selectedCards[0].group.Remove(selectedCards[0]);
        selectedCards[0].gameObject.SetActive(false);
        selectedCards[0].selected = false;
        ReArrangeCardHolder();
        Sprite sp = ResourceManager.Instance.spriteDict[selectedCards[0].cardName];
        openDeck.Insert(0, selectedCards[0].cardName);
        checkNRefillClosedDeck(openDeck);
        openDeckImage.sprite = sp; ;
        Destroy(selectedCards[0].gameObject);
        selectedCards.Clear();
        unbalanceDeck = false;
        returnFromHandBtn.gameObject.SetActive(false);
    }
    void OnReturnCard()
    {
        if (string.IsNullOrEmpty(currentPickingCard))
        {
            Debug.Log("No card selected");
            return;
        }
        openDeck.Insert(0, currentPickingCard);
        checkNRefillClosedDeck(openDeck);
        currentPickingCard = string.Empty;
        openDeckImage.sprite = ResourceManager.Instance.spriteDict[openDeck[0]];
        cardPickPopup.SetActive(false);
    }
    void MakeGroup()
    {
        if (selectedCards.Count == 1) return;
        CardGroupView cardGroup = Instantiate(cardGroupPrefab, cardHolder);
        cardGroup.transform.localPosition = Vector3.zero;
        cardGroup.name = $"{allCardGroups.Count}";
        allCardGroups.Insert(0, cardGroup);

        for (int i = 0; i < selectedCards.Count; i++)
        {
            selectedCards[i].group.Remove(selectedCards[i]);
            cardGroup.Add(selectedCards[i],i);
            selectedCards[i].selected = false;
        }

        selectedCards.Clear();
        ReArrangeCardHolder();
        grpBtn.gameObject.SetActive(false);

    }

    internal void ReArrangeCardHolder()
    {
        float width = 0;
        for (int i = allCardGroups.Count - 1; i >= 0; i--)
        {
            if (allCardGroups[i].cards.Count > 0)
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
            leftEnd += item.groupWidth / 2 + (30);

        }
    }



    public static void ShuffleList(List<string> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            string value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    internal void UpdateScore()
    {
        int score = 0;

        foreach (var item in allCardGroups)
        {
            score += item.grpPoint;
        }

        if (score <= 0)
        {

            scoreText.text = "YOU WON";
            return;

        }

        scoreText.text = score.ToString();
    }
    void checkNRefillClosedDeck(List<string> cards)
    {
        if (closedDeck.Count > 0) return;
        closedDeck.Clear();
        closedDeck = new List<string>(cards);
    }

}
