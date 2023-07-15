using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] HPBar hpBar;

    PokemonLevels _pokemon;
    public void SetData(PokemonLevels pokemon)
    {
        _pokemon = pokemon;

        nameText.text = pokemon.Base.Name;
        levelText.text = "Lv " + pokemon.Level;
        hpBar.SetHP((float)pokemon.CurrentHP / pokemon.MaxHp);
    }

    public IEnumerator UpdateHP()
    {
        yield return hpBar.SetHPSmooth((float)_pokemon.CurrentHP / _pokemon.MaxHp, (float)_pokemon.MaxHp / _pokemon.MaxHp); 
    }
}
