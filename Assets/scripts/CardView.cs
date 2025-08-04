
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardView : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Transform _selfTransform;
    [SerializeField] internal Image selfImage;
    [SerializeField] private Button _selfButton;
    internal int rank;
    internal string suit;
    internal string cardName;
    internal bool selected;
    internal bool isDragging;
    internal CardGroupView group;
    internal Transform parent;
    internal Vector2 pos;
    internal int currSib;
    internal int point;

    internal void Initiate(string _rank, string _suit, Sprite sprite)
    {
        cardName = $"{_suit}_{_rank}";
        SetRankAndPoint(_rank);
        suit = _suit;
        selfImage.sprite = sprite;
        _selfTransform.localScale = Vector3.one;
        _selfTransform.localPosition = Vector3.zero;
        _selfButton.onClick.RemoveAllListeners();
        _selfButton.onClick.AddListener(() =>
        {
            Select();
        });
        
    }

    void SetRankAndPoint(string _rank) {

        if (_rank == "A") { rank = 1; point = 10; }
        else if (_rank == "Q") { rank = 12; point = 10; }
        else if (_rank == "K") { rank = 13; point = 10; }
        else if (_rank == "J") { rank = 11; point = 10; }
        else { int.TryParse(_rank, out rank); point = rank; }  
    }

    public void Select()
    {
        if (isDragging) return;
        Debug.Log("called select method");
        if (selected)
        {
            DeSelect();
            return;
        }
        _selfTransform.localPosition += transform.up * 50;
        selected = true;
        CardController.Instance.OnSelect(this);
    }

    public void DeSelect()
    {
        if (selected)
        {
            _selfTransform.localPosition -= transform.up * 50;
            selected = false;
            CardController.Instance.OnDeselect(this);
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (selected) return;
        isDragging = true;
        CardController.Instance.draggingCard = this;
        pos = transform.localPosition; 
        selfImage.raycastTarget = false; // Disable raycast target to allow drop events
        transform.localScale = new Vector2(0.9f,1);


    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;
        transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0f) / CardController.Instance.canvas.scaleFactor;
        GameObject targetGO = eventData.pointerCurrentRaycast.gameObject;

        if (targetGO != null && targetGO != gameObject) // avoid self
        {
            CardView card = targetGO.GetComponent<CardView>();
            CardGroupView cardGroupView = targetGO.GetComponentInParent<CardGroupView>();
            if (card != null && cardGroupView != null)
            {
                currSib=cardGroupView.cards.IndexOf(card)+1;
                transform.SetParent(cardGroupView.transform);
                transform.SetSiblingIndex(card.transform.GetSiblingIndex() + 1);
                Debug.Log("sibling index"+currSib);

                if (targetGO.transform.parent != null)
                {
                    Debug.Log("Parent of that image: " + targetGO.transform.parent.name);
                }
            }
        }



    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDragging) return;
        isDragging = false;
        transform.localPosition = pos;
        selfImage.raycastTarget = true;
        transform.SetParent(parent);
        transform.localScale = Vector2.one;

    }

    public void OnDrop(PointerEventData eventData)
    {
        if (this.isDragging) return;
        if (CardController.Instance.draggingCard == null) return;
        CardController.Instance.draggingCard.group.Remove(CardController.Instance.draggingCard);
        this.group.Add(CardController.Instance.draggingCard, CardController.Instance.draggingCard.currSib);
        CardController.Instance.draggingCard.isDragging = false;
        CardController.Instance.draggingCard.selfImage.raycastTarget = true;
        CardController.Instance.draggingCard = null;
        CardController.Instance.ReArrangeCardHolder();

    }
}
