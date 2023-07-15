using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonLevels
{

    public Pokemon Base { get; set; }
    public int Level { get; set; }

    public int CurrentHP { get; set; }

    public List<Move> Moves {  get; set; }

    public PokemonLevels(Pokemon pBase, int pLevel)
    {
        Base = pBase;
        Level = pLevel;
        CurrentHP = MaxHp;

        // Generate Moves
        Moves = new List<Move>();
        foreach (var move in Base.LearnableMoveSet)
        {
            if(move.Level <= Level)
            {
                Moves.Add(new Move(move.Base));
            }

            if(Moves.Count >= 4) break; 
        }
    }

    public int Attack
    {
        get { return Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5; }
    }

    public int SpAttack
    {
        get { return Mathf.FloorToInt((Base.SpAttack * Level) / 100) + 5; }
    }

    public int Defense
    {
        get { return Mathf.FloorToInt((Base.Defense * Level) / 100) + 5; }
    }

    public int SpDefense
    {
        get { return Mathf.FloorToInt((Base.SpDefense * Level) / 100) +5; }
    }

    public int MaxHp
    {
        get { return Mathf.FloorToInt((Base.MaxHp * Level) / 100) + 10; }
    }

    public int Speed
    {
        get { return Mathf.FloorToInt((Base.Speed * Level) / 100) + 5; }
    }

    public DamageDetails TakeDamage(Move move, PokemonLevels attacker)
    {
        float critical = 1f;
        if (Random.value * 100f <= 6.25f)
            critical = 2f;

        float type = Pokemon.TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type1) * Pokemon.TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type2);

        var damageDetails = new DamageDetails
        {
            TypeEffectiveness = type,
            Critical = critical,
            Fainted = false
        };

        float attack;
        float defense;
        float stab = 1f;

        if (move.Base.Type == attacker.Base.Type1 || move.Base.Type == attacker.Base.Type2)
            stab = 1.5f;

        if (move.Base.IsSpecial)
        {
            attack = attacker.SpAttack;
            defense = this.Base.SpDefense;
        }
        else
        {
            attack = attacker.Attack;
            defense = this.Base.Defense;
        }


        float firstStage = ((2 * attacker.Level) / 5f) + 2;
        float secondStage = ((firstStage * (move.Base.Power * attack)) / 50f) / defense;
        float thirdStage = (secondStage + 2) * critical * Random.Range(0.85f, 1f);
        int damage = Mathf.FloorToInt(thirdStage * stab * type);

        CurrentHP -= damage;
        if(CurrentHP <= 0)
        {
            CurrentHP = 0;
            damageDetails.Fainted = true;
        }

        return damageDetails;
    }

    public Move GetRandomMove()
    {
        int r = Random.Range(0, Moves.Count);
        return Moves[r];
    }

}

public class DamageDetails
{
    public bool Fainted { get; set; }
    public float Critical { get; set; }
    public float TypeEffectiveness { get; set; }

}
