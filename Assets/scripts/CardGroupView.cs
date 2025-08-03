using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;

public class CardGroupView : MonoBehaviour
{
    [SerializeField] private RectTransform _selfTransform;

    public List<CardView> cards;

    public GameObject labelObject;
    public TMP_Text label;
    public float groupWidth;
    internal float cardOverlapArea=0.65f;
    public void Add(CardView card)
    {
        card.transform.SetParent(this.transform);
        card.group = this;
        cards.Add(card);
        Rearrange();

    }

    public void Remove(CardView card)
    {
        cards.Remove(card);
        Rearrange();
    }

    void Rearrange()
    {
        float segment = CardController.Instance.cardWidth * (1-cardOverlapArea);
        groupWidth = (cards.Count-1) * segment;
        groupWidth += CardController.Instance.cardWidth; 

        float leftEnd = (-groupWidth / 2) + CardController.Instance.cardWidth / 2;

        _selfTransform.sizeDelta = new Vector2(groupWidth, _selfTransform.sizeDelta.y);

        foreach (var item in cards)
        {
            item.transform.localPosition = new Vector2(leftEnd,0);
            leftEnd += segment;
        }


    }
}
