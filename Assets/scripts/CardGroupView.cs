using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEditor.Progress;

public class CardGroupView : MonoBehaviour
{
    [SerializeField] private RectTransform _selfTransform;

    public List<CardView> cards;

    public GameObject labelObject;
    public TMP_Text label;
    public float groupWidth;
    internal float cardOverlapArea=0.35f;
    internal int grpPoint;
    public void Add(CardView card, int sibIndex=-1)
    {
        card.transform.SetParent(this.transform);
        card.parent=this.transform;
        card.group = this;
        card.transform.localScale = Vector3.one;
        if(sibIndex<0) cards.Insert(0, card);
        else if(sibIndex >= 0 && sibIndex< cards.Count) cards.Insert(sibIndex, card);
        else cards.Add(card);
        Rearrange();
        CheckTotalPoint();


    }

    public void Remove(CardView card)
    {
        cards.Remove(card);
        Rearrange();
        CheckTotalPoint();
    }

    void CheckTotalPoint()
    {
        labelObject.SetActive(true);
        List<CardView> temp_cards = cards.OrderBy(c => c.suit).ToList();
        temp_cards= temp_cards.OrderBy(c => c.rank).ToList();

        bool isSet=IsSet(temp_cards);
        bool isSeq= IsSequence(temp_cards);
        if (isSet)
        {
            label.text = " SET (0)";
            grpPoint = 0;

        }
        else if (isSeq)
        {
            grpPoint = 0;
            label.text = " SEQUENCE (0)";
        }
        else {
            int p = 0;
            foreach (var item in temp_cards)
            {
                p += item.point;
            }
            grpPoint =p;
            label.text = $"INVALID ({grpPoint})";
        }
        CardController.Instance.UpdateScore();

    }

    bool IsSequence(List<CardView> cards)
    {
        if (cards.Count < 3) return false;

        cards = cards.OrderBy(c => c.rank).ToList();
        string suit = cards[0].suit;

        for (int i = 1; i < cards.Count; i++)
        {
            if (cards[i].suit != suit || cards[i].rank != cards[i - 1].rank + 1)
                return false;
            //score -= 10;

        }
        //UpdateScore();
        return true;
    }
    bool IsSet(List<CardView> cards)
    {
        if (cards.Count < 3 || cards.Count > 4) return false;

        int rank = cards[0].rank;
        HashSet<string> suits = new HashSet<string>();

        foreach (var card in cards)
        {

            if (card.rank != rank || !suits.Add(card.suit))
                return false;
        }

        return true;
    }
    void Rearrange()
    {
        float segment = CardController.Instance.cardWidth * (1-cardOverlapArea);
        groupWidth = (cards.Count-1) * segment;
        groupWidth += CardController.Instance.cardWidth; 

        float leftEnd = (-groupWidth / 2) + CardController.Instance.cardWidth / 2;

        _selfTransform.sizeDelta = new Vector2(groupWidth, _selfTransform.sizeDelta.y);

        for (int i = 0; i < cards.Count; i++) {
            cards[i].transform.localPosition = new Vector2(leftEnd, 0);
            leftEnd += segment;
            cards[i].currSib = i;
            cards[i].transform.SetSiblingIndex(i);
        }
        //foreach (var item in cards)
        //{
        //    item.transform.localPosition = new Vector2(leftEnd,0);
        //    leftEnd += segment;
        //}


    }
}
