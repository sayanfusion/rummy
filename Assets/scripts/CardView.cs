
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{

    [SerializeField] private Transform _selfTransform;
    [SerializeField] private Image _selfImage;
    [SerializeField] private Button _selfButton;
    internal string rank;
    internal string suit;
    [SerializeField]internal bool selected;

    internal CardGroupView group;
    internal void Initiate(string _rank, string _suit, Sprite sprite)
    {
        rank = _rank;
        suit = _suit;
        _selfImage.sprite = sprite;
        _selfTransform.localPosition = Vector3.zero;
        _selfButton.onClick.RemoveAllListeners();
        _selfButton.onClick.AddListener(() =>
        {
            Select();
        });
    }

    public void Select()
    {
        if (selected) return;
        Debug.Log("called");
        _selfTransform.localPosition += new Vector3(0, 50, 0);
        selected = true;
        CardController.Instance.OnSelect(this);
    }

}
