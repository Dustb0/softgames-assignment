using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct CardStack
{
    public Transform Stack;
    public TextMeshPro Label;
}

public class CardAnimator : MonoBehaviour
{
    [Header("Card Setup")]
    public CardStack StartStack = new CardStack();
    public CardStack EndStack = new CardStack();
    public GameObject PlayingCardPrefab;
    public int CardCount = 144;

    [Header("Animation")]
    public Vector3 cardOffset = new Vector3(0.001f, 0.001f, 0);
    public float moveInterval = 1f;       
    public float moveDuration = 0.5f;

    public UnityEvent OnFinishAnimation = new UnityEvent();

    private List<GameObject> cards = new List<GameObject>();

    // The card that's being moved next
    private int currentCardIndex;

    // The amount of time until we're moving a card again
    private float waitUntilNextMove;

    // The amount of time left to finish the move animation
    private float currentMoveTime;

    // Start position of the card that's being moved (used for lerping)
    private Vector3 currentStartPosition;

    void Start()
    {
        // Instantiate card prefabs
        for (int i = 0; i < CardCount; i++)
        {
            var card = Instantiate(PlayingCardPrefab);
            card.transform.position = StartStack.Stack.position;
            cards.Add(card);
        }

        cards[currentCardIndex].transform.position += cardOffset;

        UpdateAllCounters();
        currentCardIndex = 0;
        waitUntilNextMove = moveInterval;
    }

    private void UpdateAllCounters()
    {
        StartStack.Label.text = (CardCount - currentCardIndex).ToString();
        EndStack.Label.text = currentCardIndex.ToString();
    }

    private void OnDestroy()
    {
        // Clean up instantiated cards
        foreach (var item in cards)
        {
            Destroy(item);
        }
        cards.Clear();
    }

    void Update()
    {
       // Are we waiting until the next move?
       if (waitUntilNextMove > 0)
       {
            waitUntilNextMove -= Time.deltaTime;

            if (waitUntilNextMove <= 0)
            {
                currentMoveTime = moveDuration;
                currentStartPosition = cards[currentCardIndex].transform.position;
                
                if (currentCardIndex < CardCount - 1) cards[currentCardIndex + 1].transform.position += cardOffset;
            }
       }
       else if (currentMoveTime > 0)
       {
            // We're currently animating a card moving
            currentMoveTime -= Time.deltaTime;

            var normalizedTime = 1 - (currentMoveTime / moveDuration);

            cards[currentCardIndex].transform.position = Vector3.Lerp(currentStartPosition, EndStack.Stack.position, normalizedTime);
            cards[currentCardIndex].transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(0f, 360f, normalizedTime));

            if (currentMoveTime <= 0)
            {
                ++currentCardIndex;
                waitUntilNextMove = moveInterval - moveDuration;
                UpdateAllCounters();

                if (currentCardIndex >= CardCount)
                {
                    Debug.Log("Finished Animation");
                    OnFinishAnimation.Invoke();
                    enabled = false;
                }
            }
        }
    }
}
