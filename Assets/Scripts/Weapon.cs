using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Tipo { MAGIC, PHYSICAL }

[CreateAssetMenu]
public class Weapon : ScriptableObject
{
    public string nombre;
    public Tipo tipo;
    public int fuerza;
    public Sprite sprite;

    public void SetStats(Tipo newTipo, int newFuerza)
    {
        tipo = newTipo;
        fuerza = newFuerza;
    }
}
