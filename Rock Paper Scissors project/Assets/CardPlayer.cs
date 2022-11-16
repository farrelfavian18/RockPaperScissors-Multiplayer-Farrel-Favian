using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class CardPlayer : MonoBehaviour
{
    public Transform atkPosRef;
    public Card chosenCard;
    [SerializeField] private TMP_Text nameText;
    public HealthBar healthBar;
    public TMP_Text healthText;
    public float Health;
    public float MaxHealth;
    private Tweener animationTweener;
    public TMP_Text NickName { get => nameText; }

    public void Start()
    {
        Health = MaxHealth;
    }

    public Attack? AttackValue
    {
        get => chosenCard == null ? null : chosenCard.AttackValue;
    }

    public void Reset()
    {
        if (chosenCard != null)
        {
            chosenCard.Reset();
        }

        chosenCard = null;
    }

    public void SetChosenCard(Card newCard)
    {

        if (chosenCard != null)
        {
            chosenCard.Reset();
        }

        chosenCard = newCard;
        chosenCard.transform.DOScale(chosenCard.transform.localScale * 1.2f, 0.2f);
    }

    public void ChangeHealth(float amount)
    {
        Health += amount;
        Health = Mathf.Clamp(Health, 0, 100);

        // healthbar
        healthBar.UpdateBar(Health / MaxHealth);
        // text
        healthText.text = Health + "/" + MaxHealth;
    }

    public void AnimateAttack()
    {
        animationTweener = chosenCard.transform.DOMove(atkPosRef.position, 1);
    }

    public void AnimateDamage()
    {
        var image = chosenCard.GetComponent<Image>();
        animationTweener = image.DOColor(Color.red, 0.1f).SetLoops(3, LoopType.Yoyo).SetDelay(0.5f);
    }

    public void AnimateDraw()
    {
        animationTweener = chosenCard.transform
            .DOMove(chosenCard.OriginalPosition, 1)
            .SetEase(Ease.InBack)
            .SetDelay(0.2f);
    }
    public bool IsAnimating()
    {
        return animationTweener.IsActive();
    }

    public void isClickable(bool value)
    {
        Card[] cards = GetComponentsInChildren<Card>();
        foreach (var card in cards)
        {
            card.SetClickable(value);
        }
    }
}
