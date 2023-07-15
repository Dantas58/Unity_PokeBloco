using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] Pokemon _base;
    [SerializeField] int level;
    [SerializeField] bool isPlayerUnit;

    public PokemonLevels Pokemon { get; set; }

    Image image;
    Vector3 originalPos;
    Color originalColor;

    private void Awake()
    {
        image = GetComponent<Image>();
        originalPos = image.transform.localPosition;
        originalColor = image.color;
    }

    public void SetUp()
    {
        Pokemon = new PokemonLevels(_base, level);

        if (isPlayerUnit)
        {
            image.sprite = Pokemon.Base.BackSprite;
        }
        else
        {
            image.sprite = Pokemon.Base.FrontSprite;
        }

        image.color = originalColor;
        PlayEnterAnimation(); 
    }

    public void PlayEnterAnimation()
    {
        if (isPlayerUnit)
            image.transform.localPosition = new Vector3(-500f, originalPos.y);
        else
            image.transform.localPosition = new Vector3(500f, originalPos.y);

        image.transform.DOLocalMoveX(originalPos.x, 1f);
    }

    public void PlayAttackAnimation()
    {
        var sequence = DOTween.Sequence();

        if (isPlayerUnit)
            sequence.Append(image.transform.DOLocalMoveX(originalPos.x + 50f, 0.25f));
        else
            sequence.Append(image.transform.DOLocalMove(new Vector3(originalPos.x - 50f, originalPos.y - 10f), 0.25f)); 
            //sequence.Append(image.transform.DOLocalMoveX(originalPos.x - 50f, 0.25f));

        sequence.Append(image.transform.DOLocalMove(new Vector3(originalPos.x, originalPos.y), 0.25f));
    }

    public void PlayHitAnimation()
    {
        var sequence = DOTween.Sequence();

        for (int i = 0; i < 3; i++)
        {
            //sequence.Append(image.DOColor(Color.gray, 0.1f));
            //sequence.Append(image.DOColor(alphaChange, 0.12f));
            //sequence.Append(image.DOColor(originalColor, 0.12f));
            sequence.Append(image.DOFade(0, 0.12f));
            sequence.Append(image.DOFade(1, 0.12f));
        }
    }

    public void PlayFaintAnimation()
    {
        var sequence = DOTween.Sequence();

        sequence.Append(image.transform.DOLocalMoveY(originalPos.y - 10f, 0.5f));
        sequence.Join(image.DOFade(0f, 0.5f));
    }
}
