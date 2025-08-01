using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;

public class CardGroupView : MonoBehaviour
{
    [SerializeField] private RectTransform _selfTransform;

    public List<CardView> _cards;

    public GameObject labelObject;
    public TMP_Text label;
    public float groupWidth;
    internal float cardOverlapArea=0.65f;
    public void Add(CardView card)
    {
        card.transform.SetParent(this.transform);
        card.group = this;
        _cards.Add(card);
        Rearrange();

    }

    public void Remove(CardView card)
    {
        _cards.Remove(card);
        Rearrange();
    }

    void Rearrange()
    {
        float segment = 168 * (1-cardOverlapArea);
        groupWidth = (_cards.Count-1) * segment;
        groupWidth += 168;

        float leftEnd = (-groupWidth / 2) + 168 / 2;

        _selfTransform.sizeDelta = new Vector2(groupWidth, _selfTransform.sizeDelta.y);

        foreach (var item in _cards)
        {
            item.transform.localPosition = new Vector2(leftEnd,0);
            leftEnd += segment;
        }


    }
}
