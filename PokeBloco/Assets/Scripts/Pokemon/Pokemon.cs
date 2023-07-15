using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Beast", menuName = "Beast/Create new beast")]
public class Pokemon : ScriptableObject
{

    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;

    [SerializeField] PokemonType type1;
    [SerializeField] PokemonType type2;

    [SerializeField] int maxHp;
    [SerializeField] int attack;
    [SerializeField] int spAttack;
    [SerializeField] int defense;
    [SerializeField] int spDefense;
    [SerializeField] int speed;

    [SerializeField] List<LearnableMoves> learnableMoves;

    public string Name
    {
        get { return name; }
    }
    public string Description
    {
        get { return description; }
    }

    public int MaxHp
    {
        get { return maxHp; }
    }

    public int Attack
    {
        get { return attack; }
    }

    public int SpAttack
    {
        get { return spAttack; }
    }

    public int Defense
    {
        get { return defense; }
    }

    public int SpDefense
    {
        get { return spDefense; }
    }

    public int Speed
    {
        get { return speed; }
    }

    public Sprite FrontSprite { get { return frontSprite; } }
    public Sprite BackSprite { get { return backSprite; } }
    public PokemonType Type1 { get { return type1; } }
    public PokemonType Type2 { get { return type2; } }

    public List<LearnableMoves> LearnableMoveSet { get { return learnableMoves; } }


    [System.Serializable]
    public class LearnableMoves
    {
        [SerializeField] MoveBase moveBase;
        [SerializeField] int level;

        public MoveBase Base { get { return moveBase; } }
        public int Level { get { return level;} }
    }

    public enum PokemonType
    {
        Peneiro,
        Reizinho,
        Saqueador,
        Psiquico,
        Javardao,
        Bebado,
        Cego,
        Retardado,
        Nerd,
        None
    }

    public class TypeChart
    {
        static float[][] chart =
        {
            //                 PEN  REI SAQ PSI JAV BEB CEG RET NER NON                 
            /*PEN*/new float[] {1f, 0.5f, 2f, 1f, 1f, 1f, 1f, 1f, 0.5f, 1f},
            /*REI*/new float[] {1f, 2f, 1f, 2f, 1f, 1f, 1f, 0f, 1f, 1f},
            /*SAQ*/new float[] {0.5f, 1f, 1f, 2f, 2f, 1f, 1f, 1f, 0.5f, 1f},
            /*PSI*/new float[] {1f, 0.5f, 0f, 2f, 1f, 2f, 2f, 2f, 1f, 1f},
            /*JAV*/new float[] {1f, 0.5f, 0.5f, 1f, 1f, 2f, 2f, 0.5f, 1f, 1f},
            /*BEB*/new float[] {1f, 1f, 1f, 0.5f, 2f, 2f, 0f, 0.5f, 2f, 1f},
            /*CEG*/new float[] {1f, 1f, 1f, 0.5f, 1f, 0f, 0.5f, 1f, 2f, 1f},
            /*RET*/new float[] {1f, 2f, 1f, 2f, 0.5f, 0.5f, 1f, 0.5f, 1f, 1f},
            /*NER*/new float[] {2f, 0.5f, 2f, 1f, 0.5f, 0.5f, 0.5f, 1f, 1f, 1f},
            /*NON*/new float[] {1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f}
        };

        public static float GetEffectiveness(PokemonType attackType, PokemonType defenseType)
        {
            if (attackType == PokemonType.None || defenseType == PokemonType.None)
                return 1;

            int row = (int) attackType;
            int col = (int) defenseType;
            
            return chart[row][col];
        }
    }
}